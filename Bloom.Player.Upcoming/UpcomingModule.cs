using Bloom.Player.Upcoming.Views;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

namespace Bloom.Player.Upcoming
{
    /// <summary>
    /// Player upcoming Prism module.
    /// </summary>
    [Module(ModuleName = "UpcomingModule")]
    public class UpcomingModule : IModule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpcomingModule"/> class.
        /// </summary>
        /// <param name="regionManager">The region manager.</param>
        public UpcomingModule(IRegionManager regionManager)
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
