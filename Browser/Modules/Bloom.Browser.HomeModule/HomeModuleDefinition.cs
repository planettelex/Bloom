using Bloom.Browser.HomeModule.Services;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Unity;

namespace Bloom.Browser.HomeModule
{
    /// <summary>
    /// Shared home Prism module.
    /// </summary>
    [Module(ModuleName = "HomeModule")]
    public class HomeModuleDefinition : IModule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HomeModuleDefinition" /> class.
        /// </summary>
        /// <param name="container">The DI container.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        public HomeModuleDefinition(IUnityContainer container, IEventAggregator eventAggregator)
        {
            _container = container;
            _eventAggregator = eventAggregator;
        }
        private readonly IUnityContainer _container;
        // ReSharper disable once NotAccessedField.Local
        private readonly IEventAggregator _eventAggregator;

        public void Initialize()
        {
            // Register services this module provides
            _container.RegisterType<IHomeService, HomeService>(new ContainerControlledLifetimeManager());
            _container.Resolve(typeof(IHomeService));
        }
    }
}
