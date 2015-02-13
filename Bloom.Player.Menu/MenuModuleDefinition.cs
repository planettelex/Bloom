using Bloom.Player.MenuModule.Views;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

namespace Bloom.Player.MenuModule
{
    /// <summary>
    /// Player menu Prism module.
    /// </summary>
    [Module(ModuleName = "MenuModule")]
    public class MenuModuleDefinition : IModule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuModule"/> class.
        /// </summary>
        /// <param name="regionManager">The region manager.</param>
        public MenuModuleDefinition(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// Notifies the module that it has be initialized.
        /// </summary>
        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion("MenuRegion", typeof (MenuView));
        }
    }
}
