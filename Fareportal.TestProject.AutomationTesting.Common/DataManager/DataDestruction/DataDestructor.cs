using Fareportal.AutomationFramework.RestClient.Core;
using Fareportal.TestProject.AutomationTesting.Common.DataManager.DataBuilding;
using Fareportal.TestProject.AutomationTesting.Common.DataManager.Shared;

namespace Fareportal.TestProject.AutomationTesting.Common.DataManager.DataDestruction
{
    public sealed class DataDestructor
    {
        private static DataDestructor _instance;

        private DataDestructor()
        {
        }

        public static DataDestructor GetInstance => _instance ?? (_instance = new DataDestructor());

        /// <summary>
        /// Execute DELETE request for type
        /// </summary>
        /// <typeparam name="TData">Type of object to delete</typeparam>
        /// <param name="id">Id or Id with additional url</param>
        public void Delete<TData>(string id) where TData : class, IDataGeneratable, new()
        {
            if (id.StartsWith("/"))
                id = id.Remove(0, 1);
            TData data = new TData();
            RestClientService.RestClient.Delete($"{UrlResolver.GetUrl(data)}/{id}");
            RequestHelper.HandleTheResponse("DELETE");
        }

        /// <summary>
        /// Execute DELETE request for type
        /// </summary>
        /// <param name="id">Id or Id with additional url</param>
        /// <param name="data">Created object</param>
        public void Delete(string id, IDataGeneratable data)
        {
            if (id.StartsWith("/"))
                id = id.Remove(0, 1);
            RestClientService.RestClient.Delete($"{UrlResolver.GetUrl(data)}/{id}");
            RequestHelper.HandleTheResponse("DELETE");
        }
    }
}