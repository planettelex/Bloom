using Bloom.Browser.Modules.LibraryModule.Services;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;

namespace Bloom.Browser.Modules.LibraryModule
{
    /// <summary>
    /// Browser library module.
    /// </summary>
    /// <seealso cref="Microsoft.Practices.Prism.Modularity.IModule" />
    [Module(ModuleName = "LibraryModule")]
    public class LibraryModuleDefinition : IModule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryModuleDefinition"/> class.
        /// </summary>
        /// <param name="container">The DI container.</param>
        public LibraryModuleDefinition(IUnityContainer container)
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
            _container.RegisterType<IImportService, ImportService>(new ContainerControlledLifetimeManager());
            _container.Resolve(typeof(IImportService));
            _container.RegisterType<ILibraryService, LibraryService>(new ContainerControlledLifetimeManager());
            _container.Resolve(typeof(ILibraryService));
        }
    }
}
