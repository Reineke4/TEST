using Fareportal.TestProject.AutomationTesting.Common.Utils;
using Newtonsoft.Json;

namespace Fareportal.TestProject.AutomationTesting.Common.DataManager.DataBuilding.Data
{
    public class Post: IDataGeneratable
    {
        [JsonProperty("userId")]
        public int? UserId { get; set; }
        [JsonProperty("Title")]
        public string Title { get; set; }
        [JsonProperty("Body")]
        public string Body { get; set; }

        /// <summary>
        /// Generate the new instance of Post with default fake data
        /// </summary>
        public object Generate()
        {
            Post post = new Post
            {
                UserId = Randomizer.RandomNumber(1, 10),
                Title = Faker.Lorem.Sentence(),
                Body = Faker.Lorem.Sentence()
            };
            return post;
        }
    }
}
