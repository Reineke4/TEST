using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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
    public class PhotosSteps
    {
        [Given(@"I have called GET for '/photos'")]
        public void GivenIHaveCalledGETForPhotos()
        {
            List<ReceivedPhoto> receivedPhotos = DataManager.Receiver.GetLatestRemoteObjectsList<ReceivedPhoto>();
            ScenarioContext.Current[SolutionSharedSteps.CURRENT_GET_RESULT_CODE] = RequestHelper.LastOperationResultCode;
            ScenarioContext.Current[SolutionSharedSteps.CURRENT_PHOTO] = receivedPhotos;
        }

        [Given(@"I have received the albumId by Photo title: (.*)")]
        public void GivenIHaveReceivedTheAlbumIdByPhotoTitle(string photoTitle)
        {
            List<ReceivedPhoto> receivedPhotos =
                ScenarioContext.Current.Get<List<ReceivedPhoto>>(SolutionSharedSteps.CURRENT_PHOTO);
            string albumId = receivedPhotos.Single(el => el.Title.IsEqualsTo(photoTitle)).AlbumId.ToString();
            ScenarioContext.Current[SolutionSharedSteps.CURRENT_ID] = albumId;
        }

        [Given(@"I have called GET for '/photos/{id}' with id of: (.*)")]
        public void WhenICallGETForPhotosWithIdOf(string photoId)
        {
            ReceivedPhoto receivedPhoto = DataManager.Receiver.CallGETById<ReceivedPhoto>(photoId);
            ScenarioContext.Current[SolutionSharedSteps.CURRENT_PHOTO] = receivedPhoto;
        }

        [When(@"I get the image of the received Photo")]
        public void WhenIGetTheOfTheFromReceivedPhoto()
        {
            ReceivedPhoto receivedPhoto = ScenarioContext.Current.Get<ReceivedPhoto>(SolutionSharedSteps.CURRENT_PHOTO);
            HttpResponseMessage image = DataManager.Receiver.CallGETWithPartialUrl(receivedPhoto.Url);
            ScenarioContext.Current[SolutionSharedSteps.CURRENT_RECEIVED_OBJECT] = image;
        }

        [When(@"I get the userId from received album")]
        public void WhenIGetTheUserIdFromReceivedAlbum()
        {
            ReceivedAlbum receivedAlbum = ScenarioContext.Current.Get<ReceivedAlbum>(SolutionSharedSteps.CURRENT_ALBUM);
            string userId = receivedAlbum.UserId.ToString();
            ScenarioContext.Current[SolutionSharedSteps.CURRENT_ID] = userId;
        }
        
        [Then(@"I see that received user email is equal to: (.*)")]
        public void ThenISeeThatReceivedUserEmailIsEqualTo(string userEmail)
        {
            ReceivedUser receivedUser = ScenarioContext.Current.Get<ReceivedUser>(SolutionSharedSteps.CURRENT_USER);
            Assert.AreEqual(userEmail, receivedUser.email);
        }

        [Then(@"I see that received image matches to expected one")]
        public void ThenISeeThatReceivedImageMatchesToExpectedOne()
        {
            HttpResponseMessage response =
                ScenarioContext.Current.Get<HttpResponseMessage>(SolutionSharedSteps.CURRENT_RECEIVED_OBJECT);
            string pathToExpectedImage = Helpers.GetPathToFiles(FileType.PNG).ElementAt(0);

            byte[] expectedByteArray = File.ReadAllBytes(pathToExpectedImage);
            byte[] actualBytesArray = response.Content.ReadAsByteArrayAsync().Result;

            Assert.IsTrue(Helpers.CompareByteArrays(expectedByteArray, actualBytesArray), "Images are not equals");
        }
    }
}
