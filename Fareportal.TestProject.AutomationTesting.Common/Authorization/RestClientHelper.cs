using Fareportal.AutomationFramework.RestClient.Core;
using Fareportal.TestProject.AutomationTesting.Common.PermanentTestData;

namespace Fareportal.TestProject.AutomationTesting.Common.Authorization
{
    public sealed class RestClientHelper
    {
        private static RestClientHelper _instance;
        public static RestClientHelper GetInstance => _instance ?? (_instance = new RestClientHelper());

        private RestClientHelper()
        {
        }

        /// <summary>
        /// Bind to TestProject config data and set the current config
        /// </summary>
        public void TestProjecReset()
        {
            RestClientService.ResetHttpClient();
            TestData data = new TestData();
            IHasRestApiConfig config = data.Clone();
            RestClientService.SetConfig(config);
        }
    }
}
