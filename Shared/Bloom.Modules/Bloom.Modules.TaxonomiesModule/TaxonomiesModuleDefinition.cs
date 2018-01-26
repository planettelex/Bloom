using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using TaxonomiesView = Bloom.Modules.TaxonomiesModule.Views.TaxonomiesView;

namespace Bloom.Modules.TaxonomiesModule
{
    /// <summary>
    /// Shared taxonomies module.
    /// </summary>
    [Module(ModuleName = "TaxonomiesModule")]
    public class TaxonomiesModuleDefinition : IModule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaxonomiesModuleDefinition"/> class.
        /// </summary>
        /// <param name="regionManager">The region manager.</param>
        public TaxonomiesModuleDefinition(IRegionManager regionManager)
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
