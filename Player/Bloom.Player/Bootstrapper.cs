using System.Windows;
using Bloom.Data;
using Bloom.Data.Interfaces;
using Bloom.Data.Services;
using Bloom.Modules.LibraryModule;
using Bloom.Modules.LibraryModule.Services;
using Bloom.Modules.UserModule;
using Bloom.Modules.UserModule.Services;
using Bloom.Player.Modules.MenuModule;
using Bloom.Player.Modules.PlayingModule;
using Bloom.Player.Modules.RecentModule;
using Bloom.Player.Modules.VisualsModule;
using Bloom.Player.Modules.VolumeModule;
using Bloom.Player.State.Services;
using Bloom.Player.UpcomingModule;
using Bloom.Services;
using Bloom.State.Data;
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
            if (Application.Current.MainWindow != null)
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

            Container.RegisterType<IDataService, LibraryDataService>("Library", new ContainerControlledLifetimeManager());
        }

        /// <summary>
        /// Registers the services into the DI container.
        /// </summary>
        protected void RegisterServices()
        {
            Container.RegisterType<IFileSystemService, FileSystemService>(new ContainerControlledLifetimeManager());
            Container.Resolve<IFileSystemService>();
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
