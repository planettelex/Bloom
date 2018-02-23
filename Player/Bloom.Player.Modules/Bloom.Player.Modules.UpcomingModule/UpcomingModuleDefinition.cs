using Bloom.Player.UpcomingModule.Views;
using Prism.Modularity;
using Prism.Regions;

namespace Bloom.Player.UpcomingModule
{
    /// <summary>
    /// Player upcoming Prism module.
    /// </summary>
    [Module(ModuleName = "UpcomingModule")]
    public class UpcomingModuleDefinition : IModule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpcomingModule"/> class.
        /// </summary>
        /// <param name="regionManager">The region manager.</param>
        public UpcomingModuleDefinition(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// Notifies the module that it has be initialized.
        /// </summary>
        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion("UpcomingRegion", typeof(UpcomingView));
        }
    }
}
