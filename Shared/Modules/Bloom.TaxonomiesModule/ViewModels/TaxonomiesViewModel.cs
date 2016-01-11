using Bloom.State.Domain.Models;
using Microsoft.Practices.Prism.Regions;

namespace Bloom.TaxonomiesModule.ViewModels
{
    public class TaxonomiesViewModel
    {
        public TaxonomiesViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// Gets the state.
        /// </summary>
        public TabbedApplicationState State { get; private set; }

        public void SetState()
        {
            State = (TabbedApplicationState) _regionManager.Regions[Common.Settings.MenuRegion].Context;
        }
    }
}
