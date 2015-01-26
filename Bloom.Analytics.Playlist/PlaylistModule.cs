using Bloom.Analytics.Playlist.Services;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;

namespace Bloom.Analytics.Playlist
{
    /// <summary>
    /// Analytics playlist module.
    /// </summary>
    [Module(ModuleName = "PlaylistModule")]
    public class PlaylistModule : IModule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlaylistModule"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public PlaylistModule(IUnityContainer container)
        {
            _container = container;
        }
        private readonly IUnityContainer _container;

        /// <summary>
        /// Notifies the module that it has be initialized.
        /// </summary>
        public void Initialize()
        {
            // Register services this module provides
            _container.RegisterType<IPlaylistService, PlaylistService>(new ContainerControlledLifetimeManager());
            _container.Resolve(typeof(IPlaylistService));
        }
    }
}
