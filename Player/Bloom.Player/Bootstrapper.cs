using System.Windows;
using Bloom.Data;
using Bloom.Data.Interfaces;
using Bloom.LibraryModule;
using Bloom.LibraryModule.Services;
using Bloom.Player.MenuModule;
using Bloom.Player.PlayingModule;
using Bloom.Player.RecentModule;
using Bloom.Player.State.Services;
using Bloom.Player.UpcomingModule;
using Bloom.Player.VisualsModule;
using Bloom.Player.VolumeModule;
using Bloom.Services;
using Bloom.State.Data;
using Bloom.UserModule;
using Bloom.UserModule.Services;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.UnityExtensions;

namespace Bloom.Player
{
    /// <summary>
    /// The bootstrapper initialized the application and resolves dependencies.
    /// </summary>
    public class Bootstrapper : UnityBootstrapper
    {
        /// <summary>
        /// Creates the shell or main window of the application.
        /// </summary>
        /// <returns>
        /// The shell of the application.
        /// </returns>
        /// <remarks>
        /// If the returned instance is a <see cref="T:System.Windows.DependencyObject" />, the
        /// <see cref="T:Microsoft.Practices.Prism.Bootstrapper" /> will attach the default <see cref="T:Microsoft.Practices.Prism.Regions.IRegionManager" /> of
        /// the application in its <see cref="F:Microsoft.Practices.Prism.Regions.RegionManager.RegionManagerProperty" /> attached property
        /// in order to be able to add regions by using the <see cref="F:Microsoft.Practices.Prism.Regions.RegionManager.RegionNameProperty" />
        /// attached property from XAML.
        /// </remarks>
        protected override DependencyObject CreateShell()
        {
            RegisterDataSources();
            RegisterServices();
            return Container.Resolve<Shell>();
        }

        /// <summary>
        /// Initializes the shell.
        /// </summary>
        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (Window) Shell;
            Application.Current.MainWindow.Show();
        }

        /// <summary>
        /// Configures the <see cref="T:Microsoft.Practices.Prism.Modularity.IModuleCatalog" /> used by Prism.
        /// </summary>
        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            var moduleCatalog = (ModuleCatalog) ModuleCatalog;
            moduleCatalog.AddModule(typeof (SharedUserModuleDefinition));
            moduleCatalog.AddModule(typeof (SharedLibraryModuleDefinition));
            moduleCatalog.AddModule(typeof (MenuModuleDefinition));
            moduleCatalog.AddModule(typeof (PlayingModuleDefinition));
            moduleCatalog.AddModule(typeof (UpcomingModuleDefinition));
            moduleCatalog.AddModule(typeof (RecentModuleDefinition));
            moduleCatalog.AddModule(typeof (VisualsModuleDefinition));
            moduleCatalog.AddModule(typeof (VolumeModuleDefinition));
        }

        /// <summary>
        /// Registers the data sources with the DI container.
        /// </summary>
        protected void RegisterDataSources()
        {
            Container.RegisterType<IDataSource, StateDataSource>(new ContainerControlledLifetimeManager());
            var stateDataSource = Container.Resolve<IDataSource>();
            stateDataSource.RegisterRepositories();

            Container.RegisterType<IDataSource, LibraryDataSource>("Library", new ContainerControlledLifetimeManager());
            var libraryDataSource = Container.Resolve<IDataSource>("Library");
            libraryDataSource.RegisterRepositories();
        }

        /// <summary>
        /// Registers the services into the DI container.
        /// </summary>
        protected void RegisterServices()
        {
            Container.RegisterType<ISharedUserService, SharedUserService>(new ContainerControlledLifetimeManager());
            Container.Resolve<ISharedUserService>();
            Container.RegisterType<ISharedLibraryService, SharedLibraryService>(new ContainerControlledLifetimeManager());
            Container.Resolve<ISharedLibraryService>();
            Container.RegisterType<IPlayerStateService, PlayerStateService>(new ContainerControlledLifetimeManager());
            Container.Resolve<IPlayerStateService>();
            Container.RegisterType<ISkinningService, SkinningService>(new ContainerControlledLifetimeManager());
            Container.Resolve<ISkinningService>();
            Container.RegisterType<IProcessService, ProcessService>(new ContainerControlledLifetimeManager());
            Container.Resolve<IProcessService>();
        }
    }
}
