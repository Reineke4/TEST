using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NLog.Fluent;
using static System.Console;

namespace Fareportal.AutomationFramework.RestClient.Core
{
    public enum HttpAction
    {
        Get,
        Post,
        Put,
        Delete,
    }

    public class RestClient : IRestClient
    {
        public string RootApiUrl { get; protected set; }

        public HttpResponseMessage LastResponseMessage { get; protected set; }
        private static HttpClient _client;

        private readonly HttpClientHandler _httpClientHandler = new HttpClientHandler { AllowAutoRedirect = false };
        private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();

        public HttpClient HttpClient
        {
            get
            {
                if (_client == null)
                {
                    _client = new HttpClient(_httpClientHandler);
                }
                return _client;
            }
        }

        /// <summary>
        /// Initialize base params
        /// </summary>
        public virtual void SetConfig(IHasRestApiConfig config)
        {
            RootApiUrl = config.RootApiUrl;
        }

        /// <summary>
        /// Return the PostResponseId
        /// </summary>
        public virtual string PostResponseId
        {
            get
            {
                return !LastResponseMessage.IsSuccessStatusCode ? LastResponseMessage.GetAsStringSync() :
                    Deserializer.GetDeserializedObjectWithoutMissingMembers<PostResponseIdContainer>(LastResponseMessage.GetAsStringSync()).GetId();
            }
        }

        /// <summary>
        /// Return the last http action result code (HttpStatusCode)
        /// </summary>
        public virtual HttpStatusCode LastOperationResult
        {
            get { return LastResponseMessage.StatusCode; }
        }

        /// <summary>
        /// Return the last http action result code (int)
        /// </summary>
        public virtual int LastOperationResultCode => (int)LastOperationResult;

        /// <summary>
        /// Return the last HttpResponseMessage content (json)
        /// </summary>
        public virtual string LastErrorMessage { get; private set; }

        /// <summary>
        /// Create Http request for Get
        /// </summary>
        public virtual HttpResponseMessage Get(string url)
        {
            return PerformHttpAction(OptimizeUrl(url), HttpAction.Get).Result;
        }

        /// <summary>
        /// Create Http request for Put
        /// </summary>
        public virtual HttpResponseMessage Put(string url, object objectToPut)
        {
            StringContent content = new StringContent(objectToPut.ToJSON(), Encoding.UTF8, "application/json");
            return PerformHttpAction(OptimizeUrl(url), HttpAction.Put, content).Result;
        }

        /// <summary>
        /// Create Http request for Post
        /// </summary>
        public virtual HttpResponseMessage Post(string url, object objectToPost)
        {
            StringContent content = new StringContent(objectToPost.ToJSON(), Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = PerformHttpAction(OptimizeUrl(url), HttpAction.Post, content).Result;
            return responseMessage;
        }

        /// <summary>
        /// Create Http request for Delete
        /// </summary>
        public virtual HttpResponseMessage Delete(string url)
        {
            return PerformHttpAction(OptimizeUrl(url), HttpAction.Delete).Result;
        }

        /// <summary>
        /// Execute the Http actions: Get, Post, Put, Delete, Patch
        /// </summary>
        public virtual async Task<HttpResponseMessage> PerformHttpAction(string url, HttpAction action, HttpContent content = null)
        {
            HttpResponseMessage message;

            string contentObject = string.Empty;
            if (content != null)
                contentObject = content.ReadAsStringAsync().Result;

            switch (action)
            {
                case HttpAction.Get:
                    message = await HttpClient.GetAsync(url).ConfigureAwait(true);
                    break;

                case HttpAction.Post:
                    message = await HttpClient.PostAsync(url, content).ConfigureAwait(true);
                    break;

                case HttpAction.Put:
                    message = await HttpClient.PutAsync(url, content).ConfigureAwait(true);
                    break;

                case HttpAction.Delete:
                    message = await HttpClient.DeleteAsync(url).ConfigureAwait(true);
                    break;

                default:
                    throw new NotImplementedException("Unsupported HTTP operation:" + action);
            }

            CatchTheError(message, action, url, contentObject);
            WriteRequestIntoFile(message, action, url, contentObject);

            LastResponseMessage = message;
            LastErrorMessage = !IsPositiveStatusCode((int)LastResponseMessage.StatusCode) ? LastResponseMessage.Content.ReadAsStringAsync().Result : null;

            return message;
        }

        /// <summary>
        /// Catch and write the http request with error
        /// </summary>
        private void CatchTheError(HttpResponseMessage message, HttpAction action, string url, string contentObject)
        {
            var statusCode = (int)message.StatusCode;
            if (statusCode.ToString().StartsWith("4") || statusCode.ToString().StartsWith("5"))
            {
                WriteLine();
                WriteLine("----------------Negative, status code is received!-------------------");
                WriteLine($"Status code is {statusCode}, endpoing {action.ToString().ToUpper()} for {url}");
                WriteLine($"Request content: {contentObject}");
                WriteLine("Headers: key | value");
                foreach (var header in HttpClient.DefaultRequestHeaders)
                {
                    WriteLine($"{header.Key} : {header.Value.FirstOrDefault()}");
                }
                WriteLine($"Error message is: {message.Content.ReadAsStringAsync().Result}");
                WriteLine("------------------------Error message end!---------------------------");
                WriteLine();
            }
        }

        /// <summary>
        /// Method for tests logging and debugging
        /// </summary>
        private void WriteRequestIntoFile(HttpResponseMessage message, HttpAction action, string url, string contentObject)
        {
            var statusCode = (int)message.StatusCode;

            Log.Info();
            Log.Info("-----------------------Http request start!----------------------------");
            Log.Info($"Status code is {statusCode}, endpoing {action.ToString().ToUpper()} for {url}");
            Log.Info($"Request content: {contentObject}");
            Log.Info("Headers: key | value");
            foreach (var header in HttpClient.DefaultRequestHeaders)
            {
                Log.Info($"{header.Key} : {header.Value.FirstOrDefault()}");
            }
            Log.Info($"ReasonPhrase is: {message.ReasonPhrase}");
            Log.Info("-------------------------Http request end!----------------------------");
            Log.Info();
        }

        /// <summary>
        /// Check if provided status code belong to 200-th group
        /// </summary>
        private bool IsPositiveStatusCode(int statusCode)
        {
            return statusCode.ToString().StartsWith("2");
        }

        /// <summary>
        /// Concatenate base url with 'small' endpoint url
        /// </summary>
        protected virtual string OptimizeUrl(string url)
        {
            if (url.StartsWith("/"))
            {
                if (String.IsNullOrEmpty(RootApiUrl))
                    throw new InvalidOperationException("Root api url shouldn't be empty");

                return RootApiUrl + url;
            }
            return url;
        }
    }
}