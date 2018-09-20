using System;
using Fareportal.TestProject.AutomationTesting.Common.DataManager.DataBuilding.Data;
using Fareportal.TestProject.AutomationTesting.Common.DataManager.DataReceiving.Responses;

namespace Fareportal.TestProject.AutomationTesting.Common.DataManager.Shared
{
    internal static class UrlResolver
    {
        private const string POST = "/posts";
        private const string COMMENT = "/comments";
        private const string PHOTOS = "/photos";
        private const string TODOS = "/todos";
        private const string USERS = "/users";
        private const string ALBUM = "/albums";

        /// <summary>
        /// Return the url by provided object
        /// </summary>
        /// <returns>Endpoint without host</returns>
        public static string GetUrl<TData>(TData data) where TData : class
        {
            if (data is Post || data is ReceivedPost)
                return POST;

            if (data is ReceivedComment)
                return COMMENT;

            if (data is ReceivedPhoto)
                return PHOTOS;

            if (data is ReceivedTodo)
                return TODOS;

            if (data is ReceivedUser)
                return USERS;

            if (data is ReceivedAlbum)
                return ALBUM;

            throw new NotImplementedException("No url for incoming data type: " + data.GetType());
        }
    }
}