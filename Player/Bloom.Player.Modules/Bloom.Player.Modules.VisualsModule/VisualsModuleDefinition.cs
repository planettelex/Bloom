using Bloom.Player.Modules.VisualsModule.Views;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

namespace Bloom.Player.Modules.VisualsModule
{
    /// <summary>
    /// Player visuals Prism module.
    /// </summary>
    [Module(ModuleName = "VisualsModule")]
    public class VisualsModuleDefinition : IModule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VisualsModule"/> class.
        /// </summary>
        /// <param name="regionManager">The region manager.</param>
        public VisualsModuleDefinition(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// Notifies the module that it has be initialized.
        /// </summary>
        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion("VisualsRegion", typeof(VisualsView));
        }
    }
}
