using Bloom.Taxonomies.Views;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

namespace Bloom.Taxonomies
{
    /// <summary>
    /// Shared taxonomies Prism module.
    /// </summary>
    [Module(ModuleName = "TaxonomiesModule")]
    public class TaxonomiesModule : IModule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaxonomiesModule"/> class.
        /// </summary>
        /// <param name="regionManager">The region manager.</param>
        public TaxonomiesModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// Notifies the module that it has be initialized.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion("SidebarRegion", typeof(TaxonomiesView));
        }
    }
}
