namespace Fareportal.AutomationFramework.RestClient.Core
{
    public class PostResponseIdContainer : IResponseIdContainer
    {
        public int id { get; set; }

        public string GetId()
        {
            return id.ToString();
        }
    }
}
