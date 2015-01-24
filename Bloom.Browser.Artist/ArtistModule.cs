using Bloom.Browser.Artist.Services;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;

namespace Bloom.Browser.Artist
{
    /// <summary>
    /// Browser artist module.
    /// </summary>
    [Module(ModuleName = "ArtistModule")]
    public class ArtistModule : IModule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArtistModule"/> class.
        /// </summary>
        /// <param name="container">The DI container.</param>
        public ArtistModule(IUnityContainer container)
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
            _container.RegisterType<IArtistService, ArtistService>(new ContainerControlledLifetimeManager());
            _container.Resolve(typeof(IArtistService));
        }
    }
}
