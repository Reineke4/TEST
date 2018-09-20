using Fareportal.TestProject.AutomationTesting.Common.Authorization;
using Fareportal.TestProject.AutomationTesting.Common.Utils;
using TechTalk.SpecFlow;

namespace Fareportal.TestProject.AutomationTesting.API.Tests.StepDefinitions
{
    [Binding]
    public sealed class SpecFlowHooks
    {
        private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();

        [BeforeFeature]
        public static void BeforeFeature()
        {
            Log.Info($"Feature: \"{FeatureContext.Current.FeatureInfo.Title}\" started");
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            Log.Info($"\t Scenario: \"{ScenarioContext.Current.ScenarioInfo.Title}\" started");

            RestClientHelper.GetInstance.TestProjecReset();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            Log.Info($"\t Scenario: \"{ScenarioContext.Current.ScenarioInfo.Title}\" ended \r\n");
        }

        [AfterFeature]
        public static void AfterFeature()
        {
            Helpers.CleanUp();

            Log.Info($"Feature: \"{FeatureContext.Current.FeatureInfo.Title}\" ended  \r\n");
        }
    }
}
