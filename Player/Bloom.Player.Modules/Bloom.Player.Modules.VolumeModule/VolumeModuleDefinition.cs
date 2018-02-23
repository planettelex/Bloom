using Bloom.Player.Modules.VolumeModule.Views;
using Prism.Modularity;
using Prism.Regions;

namespace Bloom.Player.Modules.VolumeModule
{
    /// <summary>
    /// Player volume Prism module.
    /// </summary>
    [Module(ModuleName = "VolumeModule")]
    public class VolumeModuleDefinition : IModule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VolumeModule"/> class.
        /// </summary>
        /// <param name="regionManager">The region manager.</param>
        public VolumeModuleDefinition(IRegionManager regionManager)
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
