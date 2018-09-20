using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Fareportal.AutomationFramework.RestClient.Core
{
    public interface IRestClient
    {
        HttpStatusCode LastOperationResult { get; }
        string PostResponseId { get; }
        HttpResponseMessage Get(string url);
        HttpResponseMessage Put(string url, object objectToPut);
        HttpResponseMessage Post(string url, object objectToPost);
        HttpResponseMessage Delete(string url);
        Task<HttpResponseMessage> PerformHttpAction(string url, HttpAction action, HttpContent content = null);
        HttpClient HttpClient { get; }
        HttpResponseMessage LastResponseMessage { get; }
        int LastOperationResultCode { get; }
        string LastErrorMessage { get; }
        void SetConfig(IHasRestApiConfig config);
    }
}
