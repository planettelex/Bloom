using Bloom.Browser.Album.Services;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;

namespace Bloom.Browser.Album
{
    /// <summary>
    /// Browser album module.
    /// </summary>
    [Module(ModuleName = "AlbumModule")]
    public class AlbumModule : IModule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumModule"/> class.
        /// </summary>
        /// <param name="container">The DI container.</param>
        public AlbumModule(IUnityContainer container)
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
            _container.Resolve(typeof(IAlbumService));
        }
    }
}
