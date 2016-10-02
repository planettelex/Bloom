using Microsoft.Practices.Prism.Modularity;

namespace Bloom.UserModule
{
    /// <summary>
    /// Shared user module.
    /// </summary>
    /// <seealso cref="Microsoft.Practices.Prism.Modularity.IModule" />
    [Module(ModuleName = "SharedUserModule")]
    public class SharedUserModuleDefinition : IModule
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
