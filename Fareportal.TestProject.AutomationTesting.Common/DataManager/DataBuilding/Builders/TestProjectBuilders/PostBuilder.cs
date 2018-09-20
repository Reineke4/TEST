using System;
using System.Collections.Generic;
using Fareportal.TestProject.AutomationTesting.Common.DataManager.DataBuilding.Data;
using Fareportal.TestProject.AutomationTesting.Common.Utils;
using Fareportal.TestProject.AutomationTesting.Common.Utils.Parameters;

namespace Fareportal.TestProject.AutomationTesting.Common.DataManager.DataBuilding.Builders.TestProjectBuilders
{
    internal class PostBuilder : IDataBuilder
    {
        /// <summary>
        /// Generate the new instance of Settings according to params
        /// </summary>
        /// <param name="parameters">State: new(valid) or for update</param>
        public T Create<T>(Dictionary<string, string> parameters = null) where T : class, IDataGeneratable, new()
        {
            Helpers.CheckParameters(parameters, true);

            Post post = DataGeneratorFactory.Create<Post>();
            switch (parameters[ParamKeys.State].ToLower().Trim())
            {
                case ParamValues.EntityState.Valid:
                    break;

                case ParamValues.EntityState.Invalid:
                    post.Body = null;
                    post.Title = null;
                    post.UserId = null;
                    break;

                default:
                    throw new NotImplementedException($"No implementation for post state: {parameters[ParamKeys.State].ToUpper()}");
            }
            return post as T;
        }
    }
}
