using System.Collections.Generic;
using System.Linq;
using Fareportal.TestProject.AutomationTesting.Common;
using Fareportal.TestProject.AutomationTesting.Common.DataManager;
using Fareportal.TestProject.AutomationTesting.Common.DataManager.DataBuilding.Data;
using Fareportal.TestProject.AutomationTesting.Common.DataManager.DataReceiving.Responses;
using Fareportal.TestProject.AutomationTesting.Common.DataManager.Shared;
using Fareportal.TestProject.AutomationTesting.Common.Utils;
using Fareportal.TestProject.AutomationTesting.Common.Utils.Parameters;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Fareportal.TestProject.AutomationTesting.API.Tests.StepDefinitions
{
    [Binding]
    public class PostsSteps
    {
        [Given(@"I have generated the (.*) Post")]
        [Given(@"I have generated the (.*) Post again")]
        public void GivenIHaveGeneratedThePost(string state)
        {
            Post post = DataManager.Builder.GenerateNew<Post>(new Dictionary<string, string> {{ParamKeys.State, state}});
            ScenarioContext.Current[SolutionSharedSteps.OBJECT_SEND_DATA_PREPARED] = post;
        }

        [Given(@"I have posted the Post with (.*) data")]
        public void GivenIHavePostedThePostWithData(string state)
        {
            DataManager.Builder.BuildAndPost<Post>(new Dictionary<string, string> {{ParamKeys.State, state}});
            ScenarioContext.Current[SolutionSharedSteps.CURRENT_POST_RESULT_CODE] = RequestHelper.LastOperationResultCode;
            ScenarioContext.Current[SolutionSharedSteps.CURRENT_POST_RESPONSE_ID] = 
                DataManager.Builder.Storage.GetRecentId<Post>();
        }

        [Given(@"I have received the userId by Post title: (.*)")]
        public void GivenIHaveReceivedTheUserIdByPostTitle(string title)
        {
            List<ReceivedPost> receivedPosts =
                ScenarioContext.Current.Get<List<ReceivedPost>>(SolutionSharedSteps.CURRENT_POST);
            string userId = receivedPosts.Single(el => el.Title.IsEqualsTo(title)).UserId.ToString();
            ScenarioContext.Current[SolutionSharedSteps.CURRENT_ID] = userId;
        }

        [Given(@"I have called POST for '/posts' with data generated")]
        [When(@"I call POST for '/posts' with data generated")]
        public void GivenIHaveCalledPOSTForPostsWithDataGenerated()
        {
            Post post = ScenarioContext.Current.Get<Post>(SolutionSharedSteps.OBJECT_SEND_DATA_PREPARED);
            DataManager.Builder.Post(post);
            ScenarioContext.Current[SolutionSharedSteps.CURRENT_POST_RESULT_CODE] = RequestHelper.LastOperationResultCode;
            ScenarioContext.Current[SolutionSharedSteps.CURRENT_POST_RESPONSE_ID] = 
                DataManager.Builder.Storage.GetRecentId<Post>();
        }

        [When(@"I call PUT for '/posts/{id}' with data generated and id provided")]
        public void WhenICallPUTForWithDataGeneratedAndIdProvided()
        {
            string postId = ScenarioContext.Current.Get<string>(SolutionSharedSteps.CURRENT_POST_RESPONSE_ID);
            Post newPost = ScenarioContext.Current.Get<Post>(SolutionSharedSteps.OBJECT_SEND_DATA_PREPARED);
            DataManager.Builder.Put(postId, newPost);
            ScenarioContext.Current[SolutionSharedSteps.CURRENT_PUT_RESULT_CODE] = RequestHelper.LastOperationResultCode;
        }
        
        [When(@"I call Delete for '/posts/{id}' with id provided")]
        public void WhenICallDeleteForPostsWithIdProvided()
        {
            string postId = ScenarioContext.Current.Get<string>(SolutionSharedSteps.CURRENT_POST_RESPONSE_ID);
            DataManager.Destructor.Delete<Post>(postId);
            ScenarioContext.Current[SolutionSharedSteps.CURRENT_DELETE_RESULT_CODE] = RequestHelper.LastOperationResultCode;
        }

        [Given(@"I have called GET for '/posts'")]
        [When(@"I call GET for '/posts'")]
        public void WhenICallGETForPosts()
        {
            List<ReceivedPost> receivedPosts = DataManager.Receiver.GetLatestRemoteObjectsList<ReceivedPost>();
            ScenarioContext.Current[SolutionSharedSteps.CURRENT_GET_RESULT_CODE] = RequestHelper.LastOperationResultCode;
            ScenarioContext.Current[SolutionSharedSteps.CURRENT_POST] = receivedPosts;
        }

        [Then(@"I see that updated post matches to expected")]
        [Then(@"I see that received post matches to generated one")]
        public void ThenISeeThatReceivedPostMatchesToGeneratedOne()
        {
            Post expectedPost = ScenarioContext.Current.Get<Post>(SolutionSharedSteps.OBJECT_SEND_DATA_PREPARED);
            ReceivedPost actualPost = ScenarioContext.Current.Get<ReceivedPost>(SolutionSharedSteps.CURRENT_POST);

            Assert.AreEqual(expectedPost.UserId, actualPost.Id);
            Assert.AreEqual(expectedPost.Title, actualPost.Title);
            Assert.AreEqual(expectedPost.Body, actualPost.Body);
        }       
        
        [Then(@"I see that received user name is equal to: (.*)")]
        public void ThenISeeThatReceivedUserNameIsEqualTo(string userName)
        {
            ReceivedUser receivedUser = ScenarioContext.Current.Get<ReceivedUser>(SolutionSharedSteps.CURRENT_USER);
            Assert.AreEqual(userName, receivedUser.name);
        }
    }
}
