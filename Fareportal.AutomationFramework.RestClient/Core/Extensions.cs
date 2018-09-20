using System.Net.Http;
using Newtonsoft.Json;

namespace Fareportal.AutomationFramework.RestClient.Core
{
    public static class Extensions
    {
        /// <summary>
        /// Represent object as json string
        /// </summary>
        public static string ToJSON(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// Represent HttpResponseMessage content as json string
        /// </summary>
        public static string GetAsStringSync(this HttpResponseMessage obj)
        {
            return obj.Content.ReadAsStringAsync().Result;
        }
    }
}
