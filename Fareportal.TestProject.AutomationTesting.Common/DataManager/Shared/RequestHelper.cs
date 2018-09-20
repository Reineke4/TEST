using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Fareportal.AutomationFramework.RestClient.Core;

namespace Fareportal.TestProject.AutomationTesting.Common.DataManager.Shared
{
    public static class RequestHelper
    {
        public static HttpResponseMessage LastResponseMessage;
        public static int LastOperationResultCode;

        /// <summary>
        /// Save the received response message, code, error message
        /// </summary>
        /// <param name="operation">Request type, like: GET; POST; PUT; DELETE</param>
        internal static void HandleTheResponse(string operation)
        {
            LastResponseMessage = RestClientService.RestClient.LastResponseMessage;
            LastOperationResultCode = RestClientService.RestClient.LastOperationResultCode;
            DataManager.ErrorMessageStorage.SetMessage(operation, LastOperationResultCode);
        }
    }
}