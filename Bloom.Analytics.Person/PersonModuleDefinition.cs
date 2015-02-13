using Bloom.Analytics.PersonModule.Services;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;

namespace Bloom.Analytics.PersonModule
{
    /// <summary>
    /// Analytics person module.
    /// </summary>
    [Module(ModuleName = "PersonModule")]
    public class PersonModuleDefinition : IModule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonModule"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public PersonModuleDefinition(IUnityContainer container)
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
            _container.RegisterType<IPersonService, PersonService>(new ContainerControlledLifetimeManager());
            _container.Resolve(typeof(PersonService));
        }
    }
}
