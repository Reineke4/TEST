using System;
using System.Collections.Generic;

namespace Fareportal.TestProject.AutomationTesting.Common.DataManager.DataReceiving.Receivers
{
    internal class DefaultListReceiver : IListDataReceiver
    {
        /// <summary>
        /// Receive the list of existed objects
        /// </summary>
        public List<T> Receive<T>(Dictionary<string, string> parameters = null) where T : class, new()
        {
            if (parameters != null)
                throw new ArgumentException("No parameters should be defined for default listReceiver!\r\n" +
                                            "To receive list of entities with parameters use the custom list receiver.");

            return DataManager.Receiver.GETObjectList<T>();
        }
    }
}
