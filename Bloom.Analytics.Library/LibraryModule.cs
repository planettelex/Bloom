using Bloom.Analytics.Library.Services;
using Bloom.PubSubEvents;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Unity;

namespace Bloom.Analytics.Library
{
    /// <summary>
    /// Browser library module.
    /// </summary>
    [Module(ModuleName = "LibraryModule")]
    public class LibraryModule : IModule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryModule"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        public LibraryModule(IUnityContainer container, IEventAggregator eventAggregator)
        {
            _container = container;
            _eventAggregator = eventAggregator;
        }
        private readonly IUnityContainer _container;
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// Notifies the module that it has be initialized.
        /// </summary>
        public void Initialize()
        {
            // Register services this module provides
            _container.RegisterType<ILibraryService, LibraryService>(new ContainerControlledLifetimeManager());
            _container.Resolve(typeof (ILibraryService));

            // Create an initial library tab
            _eventAggregator.GetEvent<NewLibraryTabEvent>().Publish(null);
        }
    }
}
