namespace Fareportal.TestProject.AutomationTesting.Common.DataManager.DataBuilding
{
    public class ObjectToPostId
    {
        public readonly object ObjectCreated;
        public readonly string PostId;

        public ObjectToPostId(object objectCreated, string postId)
        {
            ObjectCreated = objectCreated;
            PostId = postId;
        }

        /// <summary>
        /// Get the object with defined type
        /// </summary>
        /// <typeparam name="TObjectType">Decired type</typeparam>
        public TObjectType GetObject<TObjectType>()
        {
            return (TObjectType)ObjectCreated;
        }
    }
}
