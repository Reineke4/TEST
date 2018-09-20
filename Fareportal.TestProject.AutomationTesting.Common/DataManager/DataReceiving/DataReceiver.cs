using System.Collections.Generic;
using System.Net.Http;
using Fareportal.AutomationFramework.RestClient.Core;
using Fareportal.TestProject.AutomationTesting.Common.DataManager.Shared;

namespace Fareportal.TestProject.AutomationTesting.Common.DataManager.DataReceiving
{
    public sealed class DataReceiver
    {
        private static DataReceiver _instance;
        public static DataReceiver GetInstance => _instance ?? (_instance = new DataReceiver());

        private DataReceiver()
        {
        }

        /// <summary>
        /// Execute GET for list objects by provided type
        /// </summary>
        /// <typeparam name="TData">Decired object type</typeparam>
        /// <returns>Instance of decired object type</returns>
        internal TData CallGET<TData>() where TData : class, new()
        {
            TData data = new TData();
            RestClientService.RestClient.Get(UrlResolver.GetUrl(data));
            RequestHelper.HandleTheResponse("GET");
            return Deserializer.GetDeserializedObject<TData>(RequestHelper.LastResponseMessage.GetAsStringSync());
        }

        /// <summary>
        /// Execute GET for list of objects by provided type
        /// </summary>
        /// <typeparam name="TData">Type of the single object</typeparam>
        /// <returns>List of decired types</returns>
        internal List<TData> GETObjectList<TData>() where TData : class, new()
        {
            TData data = new TData();
            RestClientService.RestClient.Get(UrlResolver.GetUrl(data));
            RequestHelper.HandleTheResponse("GET");
            return Deserializer.GetDeserializedObject<List<TData>>(RequestHelper.LastResponseMessage.GetAsStringSync());
        }

        /// <summary>
        /// Execute GET for single object by provided type and id
        /// </summary>
        /// <typeparam name="TData">Decired object type</typeparam>
        /// <param name="id">Item post response id</param>
        /// <returns>Instance of decired object type</returns>
        public TData CallGETById<TData>(string id) where TData : class, new()
        {
            TData data = new TData();
            RestClientService.RestClient.Get($"{UrlResolver.GetUrl(data)}/{id}");
            RequestHelper.HandleTheResponse("GET");
            return Deserializer.GetDeserializedObject<TData>(RequestHelper.LastResponseMessage.GetAsStringSync());
        }

        /// <summary>
        /// Execute GET by current base url + provided partial url
        /// </summary>
        public HttpResponseMessage CallGETWithPartialUrl(string url)
        {
            RestClientService.RestClient.Get(url);
            RequestHelper.HandleTheResponse("GET");
            return RequestHelper.LastResponseMessage;
        }

        /// <summary>
        /// Return the latest state of required list types from server
        /// </summary>
        public List<TData> GetLatestRemoteObjectsList<TData>(Dictionary<string, string> parameters = null) where TData : class, new()
        {
            List<TData> data = BuilderResolver.GetListObjectReceiver<TData>().Receive<TData>(parameters);
            return data;
        }
    }
}