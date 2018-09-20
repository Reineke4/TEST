using Ninject;
using Ninject.Modules;

namespace Fareportal.AutomationFramework.RestClient.Core
{
    internal class DependencyManager
    {
        private static Dependencies _ninjectModule;
        private static IKernel _kernel;

        private static Dependencies _ninject
        {
            get { return _ninjectModule ?? (_ninjectModule = new Dependencies()); }
        }

        public static IKernel Kernel
        {
            get { return _kernel ?? (_kernel = new StandardKernel(_ninject)); }
        }
    }

    internal class Dependencies : NinjectModule
    {
        public override void Load()
        {
            Bind<IRestClient>().To<RestClient>();
            Bind<IResponseIdContainer>().To<PostResponseIdContainer>();
        }
    }
}
