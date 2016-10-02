using Microsoft.Practices.Prism.Modularity;

namespace Bloom.LibraryModule
{
    /// <summary>
    /// Shared library module.
    /// </summary>
    /// <seealso cref="Microsoft.Practices.Prism.Modularity.IModule" />
    [Module(ModuleName = "SharedLibraryModule")]
    public class SharedLibraryModuleDefinition : IModule
    {
        /// <summary>
        /// Notifies the module that it has been initialized.
        /// </summary>
        public void Initialize()
        {
            // Shared services are registered in application bootstrap.
        }
    }
}
