using Prism.Modularity;

namespace Bloom.Modules.LibraryModule
{
    /// <summary>
    /// Shared library module.
    /// </summary>
    /// <seealso cref="IModule" />
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
