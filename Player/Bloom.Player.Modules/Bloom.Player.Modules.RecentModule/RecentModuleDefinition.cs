using Bloom.Player.Modules.RecentModule.Views;
using Prism.Modularity;
using Prism.Regions;

namespace Bloom.Player.Modules.RecentModule
{
    /// <summary>
    /// Player recent Prism module.
    /// </summary>
    [Module(ModuleName = "RecentModule")]
    public class RecentModuleDefinition : IModule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecentModule"/> class.
        /// </summary>
        /// <param name="regionManager">The region manager.</param>
        public RecentModuleDefinition(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// Notifies the module that it has be initialized.
        /// </summary>
        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion("RecentRegion", typeof(RecentView));
        }
    }
}
