using System.Collections.Generic;
using System.Linq;
using Fareportal.TestProject.AutomationTesting.Common;
using Fareportal.TestProject.AutomationTesting.Common.DataManager;
using Fareportal.TestProject.AutomationTesting.Common.DataManager.DataReceiving.Responses;
using Fareportal.TestProject.AutomationTesting.Common.DataManager.Shared;
using Fareportal.TestProject.AutomationTesting.Common.Utils;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Fareportal.TestProject.AutomationTesting.API.Tests.StepDefinitions
{
    [Binding]
    public class CommentsSteps
    {
        [When(@"I call GET for '/comments'")]
        public void WhenICallGETForComments()
        {
            List<ReceivedComment> receivedComments = DataManager.Receiver.GetLatestRemoteObjectsList<ReceivedComment>();
            ScenarioContext.Current[SolutionSharedSteps.CURRENT_GET_RESULT_CODE] = RequestHelper.LastOperationResultCode;
            ScenarioContext.Current[SolutionSharedSteps.CURRENT_COMMENT] = receivedComments;
        }

        [Then(@"I see that user who left a comment with text: (.*), email is (.*)")]
        public void ThenISeeThatUserWhoLeftACommentWithTextEmailIs(string commentBody, string userEmail)
        {
            List<ReceivedComment> receivedComments =
                ScenarioContext.Current.Get<List<ReceivedComment>>(SolutionSharedSteps.CURRENT_COMMENT);
            ReceivedComment actualComment = receivedComments.Single(el => el.body.IsContains(commentBody));

            Assert.AreEqual(userEmail, actualComment.email);
        }
    }
}
