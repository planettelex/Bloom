using Bloom.Analytics.AlbumModule.Services;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;

namespace Bloom.Analytics.AlbumModule
{
    /// <summary>
    /// Analytics album module.
    /// </summary>
    [Module(ModuleName = "AlbumModule")]
    public class AlbumModuleDefinition : IModule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumModule"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public AlbumModuleDefinition(IUnityContainer container)
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
            _container.RegisterType<IAlbumService, AlbumService>(new ContainerControlledLifetimeManager());
            _container.Resolve(typeof(AlbumService));
        }
    }
}
