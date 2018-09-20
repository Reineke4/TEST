using System;
using System.Collections.Generic;

namespace Fareportal.TestProject.AutomationTesting.Common.DataManager.DataReceiving.Receivers
{
    public class DefaultReceiver : IDataReceiver
    {
        /// <summary>
        /// Receive the existed object
        /// </summary>
        public T Receive<T>(Dictionary<string, string> parameters = null) where T : class, new()
        {
            if (parameters != null)
                throw new ArgumentException("No parameters should be defined for default receiver!\r\n" +
                                            "To receive entity with parameters use the custom receiver.");

            return DataManager.Receiver.CallGET<T>();
        }
    }
}
