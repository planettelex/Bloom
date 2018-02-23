using Bloom.Browser.Modules.MenuModule.Views;
using Prism.Modularity;
using Prism.Regions;

namespace Bloom.Browser.Modules.MenuModule
{
    /// <summary>
    /// Browser menu Prism module.
    /// </summary>
    [Module(ModuleName = "MenuModule")]
    public class MenuModuleDefinition : IModule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuModuleDefinition"/> class.
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
            _regionManager.RegisterViewWithRegion(Common.Settings.MenuRegion, typeof (MenuView));
        }
    }
}
