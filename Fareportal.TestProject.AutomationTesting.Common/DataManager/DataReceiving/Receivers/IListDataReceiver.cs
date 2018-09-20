using System.Collections.Generic;

namespace Fareportal.TestProject.AutomationTesting.Common.DataManager.DataReceiving.Receivers
{
    public interface IListDataReceiver
    {
        List<T> Receive<T>(Dictionary<string, string> parameters) where T : class, new();
    }
}
