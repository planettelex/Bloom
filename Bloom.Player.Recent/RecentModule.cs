using Bloom.Player.Recent.Views;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

namespace Bloom.Player.Recent
{
    /// <summary>
    /// Player recent Prism module.
    /// </summary>
    [Module(ModuleName = "RecentModule")]
    public class RecentModule : IModule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecentModule"/> class.
        /// </summary>
        /// <param name="regionManager">The region manager.</param>
        public RecentModule(IRegionManager regionManager)
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
