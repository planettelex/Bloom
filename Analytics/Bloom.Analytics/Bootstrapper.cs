using System.Windows;
using Bloom.Analytics.AlbumModule;
using Bloom.Analytics.ArtistModule;
using Bloom.Analytics.HomeModule;
using Bloom.Analytics.LibraryModule;
using Bloom.Analytics.MenuModule;
using Bloom.Analytics.PersonModule;
using Bloom.Analytics.PlaylistModule;
using Bloom.Analytics.SongModule;
using Bloom.Analytics.State.Services;
using Bloom.Data.Interfaces;
using Bloom.Services;
using Bloom.State.Data;
using Bloom.TaxonomiesModule;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.UnityExtensions;

namespace Bloom.Analytics
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
            moduleCatalog.AddModule(typeof (MenuModuleDefinition));
            moduleCatalog.AddModule(typeof (TaxonomiesModuleDefinition));
            moduleCatalog.AddModule(typeof (HomeModuleDefinition));
            moduleCatalog.AddModule(typeof (LibraryModuleDefinition));
            moduleCatalog.AddModule(typeof (PersonModuleDefinition));
            moduleCatalog.AddModule(typeof (ArtistModuleDefinition));
            moduleCatalog.AddModule(typeof (SongModuleDefinition));
            moduleCatalog.AddModule(typeof (AlbumModuleDefinition));
            moduleCatalog.AddModule(typeof (PlaylistModuleDefinition));
        }

        /// <summary>
        /// Registers the data sources with the DI container.
        /// </summary>
        protected void RegisterDataSources()
        {
            Container.RegisterType<IDataSource, StateDataSource>(new ContainerControlledLifetimeManager());
            var stateDataSource = Container.Resolve<IDataSource>();
            stateDataSource.RegisterRepositories();
        }

        /// <summary>
        /// Registers the services with the DI container.
        /// </summary>
        protected void RegisterServices()
        {
            Container.RegisterType<IUserService, UserService>(new ContainerControlledLifetimeManager());
            Container.Resolve<IUserService>();
            Container.RegisterType<IAnalyticsStateService, AnalyticsStateService>(new ContainerControlledLifetimeManager());
            Container.Resolve<IAnalyticsStateService>();
            Container.RegisterType<ISkinningService, SkinningService>(new ContainerControlledLifetimeManager());
            Container.Resolve<ISkinningService>();
            Container.RegisterType<IProcessService, ProcessService>(new ContainerControlledLifetimeManager());
            Container.Resolve<IProcessService>();
        }
    }
}
