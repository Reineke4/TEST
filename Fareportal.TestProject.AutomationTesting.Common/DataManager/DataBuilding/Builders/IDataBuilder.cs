using System.Collections.Generic;

namespace Fareportal.TestProject.AutomationTesting.Common.DataManager.DataBuilding.Builders
{
    internal interface IDataBuilder
    {
        T Create<T>(Dictionary<string, string> parameters = null) where T : class, IDataGeneratable, new();
    }
}
