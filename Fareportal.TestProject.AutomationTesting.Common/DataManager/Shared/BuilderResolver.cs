using System;
using Fareportal.TestProject.AutomationTesting.Common.DataManager.DataBuilding;
using Fareportal.TestProject.AutomationTesting.Common.DataManager.DataBuilding.Builders;
using Fareportal.TestProject.AutomationTesting.Common.DataManager.DataBuilding.Builders.TestProjectBuilders;
using Fareportal.TestProject.AutomationTesting.Common.DataManager.DataBuilding.Data;
using Fareportal.TestProject.AutomationTesting.Common.DataManager.DataReceiving.Receivers;
using Fareportal.TestProject.AutomationTesting.Common.DataManager.DataReceiving.Responses;

namespace Fareportal.TestProject.AutomationTesting.Common.DataManager.Shared
{
    internal static class BuilderResolver
    {
        /// <summary>
        /// Return builder according to the object type
        /// </summary>
        /// <returns>Concrete builder</returns>
        public static IDataBuilder GetObjectBuilder<T>() where T : class, IDataGeneratable, new()
        {
            object data = new T();

            switch (data)
            {
                case Post _:
                    return new PostBuilder();

                default:
                    throw new NotImplementedException($"No object builder implemented for type: {data.GetType().ToString().ToUpper()}");
            }
        }

        /// <summary>
        /// Return receiver according to the object type
        /// </summary>
        /// <returns>Conrcrete receiver</returns>
        public static IDataReceiver GetObjectReceiver<T>() where T : class, new()
        {
            object data = new T();

            switch (data)
            {
                case ReceivedAlbum _:
                case ReceivedUser _:
                case ReceivedPost _:
                    return new DefaultReceiver();

                default:
                    throw new NotImplementedException($"No object receiver implemented for type: {data.GetType().ToString().ToUpper()}");
            }
        }

        /// <summary>
        /// Return receiver of object list, according to the object type
        /// </summary>
        /// <returns>Conrcrete receiver</returns>
        public static IListDataReceiver GetListObjectReceiver<T>() where T : class, new()
        {
            object data = new T();

            switch (data)
            {
                case ReceivedAlbum _:
                case ReceivedUser _:
                case ReceivedComment _:
                case ReceivedPhoto _:
                case ReceivedTodo _:
                case ReceivedPost _:
                    return new DefaultListReceiver();

                default:
                    throw new NotImplementedException($"No object receiver implemented for type: {data.GetType().ToString().ToUpper()}");
            }
        }
    }
}