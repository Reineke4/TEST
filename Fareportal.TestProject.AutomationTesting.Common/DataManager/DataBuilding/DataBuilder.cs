using System.Collections.Generic;
using Fareportal.AutomationFramework.RestClient.Core;
using Fareportal.TestProject.AutomationTesting.Common.DataManager.Shared;

namespace Fareportal.TestProject.AutomationTesting.Common.DataManager.DataBuilding
{
    public sealed class DataBuilder
    {
        private static DataBuilder _instance;
        public static DataBuilder GetInstance => _instance ?? (_instance = new DataBuilder());
        public BuilderStorage Storage => BuilderStorage.GetInstance;

        private DataBuilder()
        {
        }

        /// <summary>
        /// Build and post the object by provided type and parameter
        /// </summary>
        /// <typeparam name="TData">Type to build and post</typeparam>
        /// <param name="parameters">Parameter for build</param>
        /// <returns>Built object</returns>
        public TData BuildAndPost<TData>(Dictionary<string, string> parameters = null) where TData : class, IDataGeneratable, new()
        {
            TData data = GenerateNew<TData>(parameters);
            Post(data);
            return data;
        }

        /// <summary>
        /// Execute POST request for object
        /// </summary>
        /// <param name="data">Type to post</param>
        public void Post(IDataGeneratable data)
        {
            RestClientService.RestClient.Post(UrlResolver.GetUrl(data), data);
            ObjectToPostId objectCreated = new ObjectToPostId(data, RestClientService.RestClient.PostResponseId);
            RequestHelper.HandleTheResponse("POST");
            Storage.SaveItem(data.GetType(), objectCreated);
        }

        /// <summary>
        /// Execute PUT request for object
        /// </summary>
        /// <param name="id">Posted object id</param>
        /// <param name="data">Type to put</param>
        public void Put(string id, IDataGeneratable data)
        {
            RestClientService.RestClient.Put($"{UrlResolver.GetUrl(data)}/{id}", data);
            ObjectToPostId objectCreated = new ObjectToPostId(data, id);

            RequestHelper.HandleTheResponse("PUT");
            Storage.UpdateItem(data.GetType(), objectCreated);
        }

        /// <summary>
        /// Create the instance of decired type
        /// </summary>
        /// <typeparam name="TData">Type to generate</typeparam>
        public TData GenerateNew<TData>(Dictionary<string, string> parameters = null) where TData : class, IDataGeneratable, new()
        {
            TData data = BuilderResolver.GetObjectBuilder<TData>().Create<TData>(parameters);
            return data;
        }
    }
}
