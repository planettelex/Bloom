using Bloom.LibraryModule.Services;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;

namespace Bloom.LibraryModule
{
    [Module(ModuleName = "SharedLibraryModule")]
    public class SharedLibraryModuleDefinition : IModule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SharedLibraryModuleDefinition"/> class.
        /// </summary>
        /// <param name="container">The DI container.</param>
        public SharedLibraryModuleDefinition(IUnityContainer container)
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
            _container.RegisterType<ISharedLibraryService, SharedLibraryService>(new ContainerControlledLifetimeManager());
            _container.Resolve(typeof(ISharedLibraryService));
        }
    }
}
