using System;
using System.Collections.Generic;

namespace Fareportal.TestProject.AutomationTesting.Common.DataManager.DataBuilding.Builders
{
    internal class DefaultBuilder : IDataBuilder
    {
        /// <summary>
        /// Build and return object without external data
        /// </summary>
        public T Create<T>(Dictionary<string, string> parameters = null) where T : class, IDataGeneratable, new()
        {
            if (parameters != null)
                throw new ArgumentException("No parameters should be defined for default builder!\r\n" +
                                            "To generate entity with parameters use the custom builder.");
            return DataGeneratorFactory.Create<T>();
        }
    }
}
