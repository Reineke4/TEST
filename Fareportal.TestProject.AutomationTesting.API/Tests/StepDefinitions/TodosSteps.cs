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
    public class TodosSteps
    {
        private Dictionary<string, int> _usersIds = new Dictionary<string, int>();
        [Given(@"I have got the userId with name (.*)")]
        public void GivenIHaveGotTheUserIdWithName(string userName)
        {
            List<ReceivedUser> receivedUsers =
                ScenarioContext.Current.Get<List<ReceivedUser>>(SolutionSharedSteps.CURRENT_USER);
            int userId = receivedUsers.Single(el => el.name.IsEqualsTo(userName)).id;
            _usersIds.Add(userName, userId);
        }
        
        [When(@"I call GET for '/todos'")]
        public void WhenICallGETForTodos()
        {
            List<ReceivedTodo> receivedTodos = DataManager.Receiver.GetLatestRemoteObjectsList<ReceivedTodo>();
            ScenarioContext.Current[SolutionSharedSteps.CURRENT_GET_RESULT_CODE] = RequestHelper.LastOperationResultCode;
            ScenarioContext.Current[SolutionSharedSteps.CURRENT_TODO] = receivedTodos;
        }
        
        [Then(@"I see that (.*) has more than (.*) completed TODOs than (.*)")]
        public void ThenISeeThatFirstHasMoreThanCompletedTODOsThanSecond(string firstUserName, int todosCount, string secondUserName)
        {
            List<ReceivedTodo> receivedTodos =
                ScenarioContext.Current.Get<List<ReceivedTodo>>(SolutionSharedSteps.CURRENT_TODO);
            int firstUserId = _usersIds[firstUserName];
            int secondUserId = _usersIds[secondUserName];

            int firstUserCompletedTodoCount = receivedTodos.Where(el => el.UserId == firstUserId && el.Completed).Count();
            int secondUserCompletedTodoCount = receivedTodos.Where(el => el.UserId == secondUserId && el.Completed).Count();

            Assert.That((firstUserCompletedTodoCount - secondUserCompletedTodoCount) > todosCount,
                $"The difference between {firstUserName} and {secondUserName} should be more than {todosCount} of completed TODOs\r\n" +
                $"But was only {firstUserCompletedTodoCount - secondUserCompletedTodoCount}");

        }
    }
}
