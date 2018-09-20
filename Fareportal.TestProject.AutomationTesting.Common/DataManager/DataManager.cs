using Fareportal.TestProject.AutomationTesting.Common.DataManager.DataBuilding;
using Fareportal.TestProject.AutomationTesting.Common.DataManager.DataDestruction;
using Fareportal.TestProject.AutomationTesting.Common.DataManager.DataReceiving;
using Fareportal.TestProject.AutomationTesting.Common.DataManager.Shared;

namespace Fareportal.TestProject.AutomationTesting.Common.DataManager
{
    public static class DataManager
    {
        public static DataReceiver Receiver => DataReceiver.GetInstance;
        public static DataBuilder Builder => DataBuilder.GetInstance;
        public static DataDestructor Destructor => DataDestructor.GetInstance;
        public static ErrorMessageStorage ErrorMessageStorage => ErrorMessageStorage.GetInstance;
    }
}
