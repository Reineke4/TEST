namespace Fareportal.TestProject.AutomationTesting.Common.DataManager.DataBuilding
{
    static class DataGeneratorFactory
    {
        /// <summary>
        /// Create the instance of decired type
        /// </summary>
        /// <typeparam name="TData">Type</typeparam>
        public static TData Create<TData>() where TData : class, IDataGeneratable, new()
        {
            return (TData)new TData().Generate();
        }
    }
}
