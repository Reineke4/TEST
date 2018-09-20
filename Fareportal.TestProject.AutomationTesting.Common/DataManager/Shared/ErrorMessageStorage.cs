using System;
using Fareportal.AutomationFramework.RestClient.Core;

namespace Fareportal.TestProject.AutomationTesting.Common.DataManager.Shared
{
    public class ErrorMessageStorage
    {
        private string MessageForGet { get; set; }
        private string MessageForPost { get; set; }
        private string MessageForPut { get; set; }
        private string MessageForDelete { get; set; }

        private static ErrorMessageStorage _instance;
        public static ErrorMessageStorage GetInstance => _instance ?? (_instance = new ErrorMessageStorage());

        /// <summary>
        /// Get the error message by provided operation
        /// </summary>
        /// <param name="operation">GET, POST, PUT, DELETE, PATCH</param>
        /// <returns>HttpResponseMessage content (json)</returns>
        public string GetMessage(string operation)
        {
            switch (operation.ToUpper().Trim())
            {
                case "GET":
                    return MessageForGet;
                case "POST":
                    return MessageForPost;
                case "PUT":
                    return MessageForPut;
                case "DELETE":
                    return MessageForDelete;
                default:
                    throw new NotImplementedException($"No implementation for operation: {operation.ToUpper()}");
            }
        }

        /// <summary>
        /// Set the error message for provided operation if status code isn't successful
        /// </summary>
        /// <param name="operation">GET, POST, PUT, DELETE</param>
        public void SetMessage(string operation, int statusCode)
        {
            if (IsPositiveStatusCode(statusCode))
                return;
            switch (operation.ToUpper().Trim())
            {
                case "GET":
                    MessageForGet = (string)RestClientService.RestClient.LastErrorMessage.Clone();
                    break;
                case "POST":
                    MessageForPost = (string)RestClientService.RestClient.LastErrorMessage.Clone();
                    break;
                case "PUT":
                    MessageForPut = (string)RestClientService.RestClient.LastErrorMessage.Clone();
                    break;
                case "DELETE":
                    MessageForDelete = (string)RestClientService.RestClient.LastErrorMessage.Clone();
                    break;
                default:
                    throw new NotImplementedException($"No implementation for operation: {operation.ToUpper()}");
            }
        }

        /// <summary>
        /// Check if provided status code belong to 200-th group
        /// </summary>
        private bool IsPositiveStatusCode(int statusCode)
        {
            return statusCode.ToString().StartsWith("2");
        }
    }
}
