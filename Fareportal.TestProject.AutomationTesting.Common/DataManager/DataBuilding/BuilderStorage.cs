using System;
using System.Collections.Generic;
using System.Linq;

namespace Fareportal.TestProject.AutomationTesting.Common.DataManager.DataBuilding
{
    public class BuilderStorage
    {
        private Dictionary<Type, List<ObjectToPostId>> _dataCreated;

        private static BuilderStorage _instance;

        private BuilderStorage()
        {
            _dataCreated = new Dictionary<Type, List<ObjectToPostId>>();
        }
        public static BuilderStorage GetInstance => _instance ?? (_instance = new BuilderStorage());


        /// <summary>
        /// Save the posted item
        /// </summary>
        /// <param name="objectType">Type of object to save</param>
        /// <param name="objectCreated">New object with id</param>
        public void SaveItem(Type objectType, ObjectToPostId objectCreated)
        {
            if (_dataCreated.ContainsKey(objectType))
                _dataCreated[objectType].Add(objectCreated);
            else
                _dataCreated.Add(objectType, new List<ObjectToPostId> { objectCreated });
        }

        /// <summary>
        /// Update the stored item
        /// </summary>
        /// <param name="objectType">Type of object to update</param>
        /// <param name="objectCreated">New object with stale id</param>
        public void UpdateItem(Type objectType, ObjectToPostId objectCreated)
        {
            List<ObjectToPostId> listSpecificItems = _dataCreated[objectType];
            ObjectToPostId updatedObj = listSpecificItems.First(el => el.PostId == objectCreated.PostId);
            int index = listSpecificItems.IndexOf(updatedObj);
            listSpecificItems[index] = objectCreated;
            _dataCreated[objectType] = listSpecificItems;
        }

        /// <summary>
        /// Return all posted objects and postIds from storage
        /// </summary>
        public Dictionary<Type, List<ObjectToPostId>> GetAllItems()
        {
            return _dataCreated;
        }

        /// <summary>
        /// Return last postId
        /// </summary>
        /// <typeparam name="TData">Posted Type</typeparam>
        public string GetRecentId<TData>() where TData : class, IDataGeneratable, new()
        {
            if (_dataCreated[typeof(TData)].Count == 0)
                throw new NullReferenceException($"No postId found for type: {typeof(TData)}");
            return _dataCreated[typeof(TData)].LastOrDefault().PostId;
        }
    }
}
