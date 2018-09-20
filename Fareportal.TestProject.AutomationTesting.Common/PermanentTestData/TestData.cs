using Fareportal.AutomationFramework.RestClient.Core;

namespace Fareportal.TestProject.AutomationTesting.Common.PermanentTestData
{
    internal class TestData: IHasRestApiConfig
    {
        private const string rootApiUrl = "https://jsonplaceholder.typicode.com";
        public string RootApiUrl
        {
            get => rootApiUrl;
            set { }
        }

        public IHasRestApiConfig Clone()
        {
            return (TestData) this.MemberwiseClone();
        }
    }
}
