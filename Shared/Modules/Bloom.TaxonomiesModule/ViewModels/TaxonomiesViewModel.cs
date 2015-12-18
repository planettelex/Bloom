using Bloom.State.Domain.Models;
using Microsoft.Practices.Prism.Regions;

namespace Bloom.TaxonomiesModule.ViewModels
{
    public class TaxonomiesViewModel
    {
        public TaxonomiesViewModel(IRegionManager regionManager)
        {
            State = (TabbedApplicationState) regionManager.Regions["MenuRegion"].Context;
        }

        /// <summary>
        /// Gets the state.
        /// </summary>
        public TabbedApplicationState State { get; private set; }
    }
}
