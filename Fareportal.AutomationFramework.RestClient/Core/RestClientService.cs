using System;
using Ninject;

namespace Fareportal.AutomationFramework.RestClient.Core
{
    public static class RestClientService
    {
        private static IRestClient _client;
        private static IHasRestApiConfig _config;

        /// <summary>
        /// Clean HttpClient headers and set null for client
        /// </summary>
        public static void ResetHttpClient()
        {
            if (_client == null) return;
            _client = null;
        }

        /// <summary>
        /// Set kernel base config
        /// </summary>
        public static void SetConfig(IHasRestApiConfig config)
        {
            _config = config;
        }

        /// <summary>
        /// Return the RestClient instance
        /// </summary>
        public static IRestClient RestClient
        {
            get
            {
                if (_client == null)
                {
                    if (_config == null)
                        throw new ApplicationException("Api configuration should be set");

                    _client = DependencyManager.Kernel.Get<IRestClient>();
                    _client.SetConfig(_config);
                }
                return _client;
            }
        }

        /// <summary>
        /// Set custom DI dependencies for IRestClient
        /// </summary>
        /// <typeparam name="TRestClient">ApplicationRestClient</typeparam>
        public static void BindToCustomRestClient<TRestClient>() where TRestClient : IRestClient
        {
            DependencyManager.Kernel.Rebind<IRestClient>().To<TRestClient>();
        }
    }
}
