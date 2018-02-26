using Bloom.Events;
using Bloom.State.Domain.Models;
using Prism.Events;
using Prism.Regions;

namespace Bloom.Modules.TaxonomiesModule.ViewModels
{
    /// <summary>
    /// View model for TaxonomiesView.xaml
    /// </summary>
    public class TaxonomiesViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaxonomiesViewModel"/> class.
        /// </summary>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        public TaxonomiesViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            EventAggregator = eventAggregator;
            EventAggregator.GetEvent<UserChangedEvent>().Subscribe(SetState);
        }
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// Gets the event aggregator.
        /// </summary>
        public IEventAggregator EventAggregator { get; }

        /// <summary>
        /// Gets the state.
        /// </summary>
        public TabbedApplicationState State { get; private set; }

        /// <summary>
        /// Sets the state.
        /// </summary>
        private void SetState(object nothing)
        {
            SetState();
        }

        /// <summary>
        /// Sets the state.
        /// </summary>
        public void SetState()
        {
            State = (TabbedApplicationState) _regionManager.Regions[Common.Settings.MenuRegion].Context;
        }
    }
}
