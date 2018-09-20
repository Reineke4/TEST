namespace Fareportal.AutomationFramework.RestClient.Core
{
    public interface IHasRestApiConfig
    {
        string RootApiUrl { get; set; }
        IHasRestApiConfig Clone();
    }
}
