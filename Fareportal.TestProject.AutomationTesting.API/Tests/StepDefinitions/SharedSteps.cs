using System;
using System.Collections.Generic;
using Fareportal.TestProject.AutomationTesting.Common;
using Fareportal.TestProject.AutomationTesting.Common.DataManager;
using Fareportal.TestProject.AutomationTesting.Common.DataManager.DataReceiving.Responses;
using Fareportal.TestProject.AutomationTesting.Common.DataManager.Shared;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Fareportal.TestProject.AutomationTesting.API.Tests.StepDefinitions
{
    [Binding]
    internal class SharedSteps
    {
        private readonly Dictionary<string, string> _statusToTag = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            {"get", SolutionSharedSteps.CURRENT_GET_RESULT_CODE},
            {"post", SolutionSharedSteps.CURRENT_POST_RESULT_CODE},
            {"put", SolutionSharedSteps.CURRENT_PUT_RESULT_CODE},
            {"delete", SolutionSharedSteps.CURRENT_DELETE_RESULT_CODE}
        };

        [Then(@"Response result code for (GetNew|Get|Delete|Post|Put|Patch) operation is '(.*)'")]
        public void ThenResponseResultCodeForOperationsIs(string operation, int responsecode)
        {
            int storeResultCode = int.Parse(ScenarioContext.Current[_statusToTag[operation]].ToString());
            Assert.AreEqual(responsecode, storeResultCode,
                storeResultCode.ToString().StartsWith("2")
                    ? $"Result code returned for operatoin {operation} is: {storeResultCode}."
                    : $"Result code returned for operatoin {operation} is: {storeResultCode}. Error message: {DataManager.ErrorMessageStorage.GetMessage(operation)}");
        }

        [When(@"I call GET for '/users/{id}' with id provided")]
        public void WhenICallGETForUsersWithIdProvided()
        {
            string userId = ScenarioContext.Current.Get<string>(SolutionSharedSteps.CURRENT_ID);
            ReceivedUser receivedUser = DataManager.Receiver.CallGETById<ReceivedUser>(userId);
            ScenarioContext.Current[SolutionSharedSteps.CURRENT_USER] = receivedUser;
        }

        [Given(@"I have called GET for '/users'")]
        [When(@"I call GET for '/users'")]
        public void WhenICallGETForUsers()
        {
            List<ReceivedUser> receivedUsers = DataManager.Receiver.GetLatestRemoteObjectsList<ReceivedUser>();
            ScenarioContext.Current[SolutionSharedSteps.CURRENT_GET_RESULT_CODE] = RequestHelper.LastOperationResultCode;
            ScenarioContext.Current[SolutionSharedSteps.CURRENT_USER] = receivedUsers;
        }

        [When(@"I call GET for '/albums/{id}' with id provided")]
        public void WhenICallGETForAlbumsWithIdProvided()
        {
            string albumId = ScenarioContext.Current.Get<string>(SolutionSharedSteps.CURRENT_ID);
            ReceivedAlbum receivedAlbum = DataManager.Receiver.CallGETById<ReceivedAlbum>(albumId);
            ScenarioContext.Current[SolutionSharedSteps.CURRENT_ALBUM] = receivedAlbum;
        }
    }
}
