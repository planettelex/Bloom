using Bloom.Browser.ArtistModule.Services;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;

namespace Bloom.Browser.ArtistModule
{
    /// <summary>
    /// Browser artist module.
    /// </summary>
    [Module(ModuleName = "ArtistModule")]
    public class ArtistModuleDefinition : IModule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArtistModuleDefinition"/> class.
        /// </summary>
        /// <param name="container">The DI container.</param>
        public ArtistModuleDefinition(IUnityContainer container)
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
