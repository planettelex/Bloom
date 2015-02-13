using Bloom.Player.PlayingModule.Views;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

namespace Bloom.Player.PlayingModule
{
    /// <summary>
    /// Player playing Prism module.
    /// </summary>
    [Module(ModuleName = "PlayingModule")]
    public class PlayingModuleDefinition : IModule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayingModule"/> class.
        /// </summary>
        /// <param name="regionManager">The region manager.</param>
        public PlayingModuleDefinition(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// Notifies the module that it has be initialized.
        /// </summary>
        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion("PlayingRegion", typeof(PlayingView));
        }
    }
}
