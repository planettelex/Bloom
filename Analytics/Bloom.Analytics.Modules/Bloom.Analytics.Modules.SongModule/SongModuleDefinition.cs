using Bloom.Analytics.Modules.SongModule.Services;
using Prism.Modularity;
using Microsoft.Practices.Unity;

namespace Bloom.Analytics.Modules.SongModule
{
    /// <summary>
    /// Analytics song module.
    /// </summary>
    [Module(ModuleName = "SongModule")]
    public class SongModuleDefinition : IModule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SongModuleDefinition"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public SongModuleDefinition(IUnityContainer container)
        {
            _container = container;
        }
        private readonly IUnityContainer _container;

        /// <summary>
        /// Notifies the module that it has be initialized.
        /// </summary>
        public void Initialize()
        {
            // Register services this module provides
            _container.RegisterType<ISongService, SongService>(new ContainerControlledLifetimeManager());
            _container.Resolve(typeof(ISongService));
        }
    }
}
