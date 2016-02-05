using Bloom.PubSubEvents;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;

namespace Bloom.TaxonomiesModule.ViewModels
{
    public class TaxonomiesViewModel
    {
        public TaxonomiesViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            eventAggregator.GetEvent<UserChangedEvent>().Subscribe(SetState);
        }
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// Gets the state.
        /// </summary>
        public TabbedApplicationState State { get; private set; }

        public void SetState(object nothing)
        {
            SetState();
        }

        public void SetState()
        {
            State = (TabbedApplicationState) _regionManager.Regions[Common.Settings.MenuRegion].Context;
        }
    }
}
