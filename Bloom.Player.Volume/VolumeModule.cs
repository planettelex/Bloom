using Bloom.Player.Volume.Views;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

namespace Bloom.Player.Volume
{
    /// <summary>
    /// Player volume Prism module.
    /// </summary>
    [Module(ModuleName = "VolumeModule")]
    public class VolumeModule : IModule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VolumeModule"/> class.
        /// </summary>
        /// <param name="regionManager">The region manager.</param>
        public VolumeModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// Notifies the module that it has be initialized.
        /// </summary>
        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion("VolumeRegion", typeof(VolumeView));
        }
    }
}
