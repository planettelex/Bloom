using System;
using System.Windows.Input;
using Bloom.Common;
using Bloom.Domain.Models;
using Bloom.Events;
using Prism.Commands;
using Prism.Events;

namespace Bloom.Modules.TaxonomiesModule.ViewModels
{
    /// <summary>
    /// View model for LibraryView.xaml
    /// </summary>
    public class LibraryViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryViewModel"/> class.
        /// </summary>
        /// <param name="library">The library.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <exception cref="System.ArgumentNullException">library</exception>
        public LibraryViewModel(Library library, IEventAggregator eventAggregator)
        {
            if (library == null)
                throw new ArgumentNullException(nameof(library));

            EventAggregator = eventAggregator;
            Library = library;

            AddHomeTabCommand = new DelegateCommand<object>(AddHomeTab, CanAddHomeTab);
            AddLibraryTabCommand = new DelegateCommand<object>(AddLibraryTab, CanAddLibraryTab);
            AddPersonTabCommand = new DelegateCommand<object>(AddPersonTab, CanAddPersonTab);
            AddArtistTabCommand = new DelegateCommand<object>(AddArtistTab, CanAddArtistTab);
            AddAlbumTabCommand = new DelegateCommand<object>(AddAlbumTab, CanAddAlbumTab);
            AddSongTabCommand = new DelegateCommand<object>(AddSongTab, CanAddSongTab);
            AddPlaylistTabCommand = new DelegateCommand<object>(AddPlaylistTab, CanAddPlaylistTab);
        }

        /// <summary>
        /// Gets the event aggregator.
        /// </summary>
        public IEventAggregator EventAggregator { get; }

        /// <summary>
        /// Gets or sets the library.
        /// </summary>
        public Library Library { get; set; }

        /// <summary>
        /// Gets or sets the add home tab command.
        /// </summary>
        public ICommand AddHomeTabCommand { get; set; }

        /// <summary>
        /// Determines whether this instance can use the add home tab command.
        /// </summary>
        private bool CanAddHomeTab(object nothing)
        {
            return true;
        }

        /// <summary>
        /// The add home tab command.
        /// </summary>
        private void AddHomeTab(object nothing)
        {
            EventAggregator.GetEvent<NewHomeTabEvent>().Publish(null);
        }

        /// <summary>
        /// Gets or sets the add library tab command.
        /// </summary>
        public ICommand AddLibraryTabCommand { get; set; }

        /// <summary>
        /// Determines whether this instance can use the add library tab command.
        /// </summary>
        private bool CanAddLibraryTab(object nothing)
        {
            return true;
        }

        /// <summary>
        /// The add library tab command.
        /// </summary>
        private void AddLibraryTab(object nothing)
        {
            EventAggregator.GetEvent<NewLibraryTabEvent>().Publish(Library.Id);
        }

        /// <summary>
        /// Gets or sets the add person tab command.
        /// </summary>
        public ICommand AddPersonTabCommand { get; set; }

        /// <summary>
        /// Determines whether this instance use the add person tab command.
        /// </summary>
        /// <param name="nothing">The nothing.</param>
        /// <returns></returns>
        private bool CanAddPersonTab(object nothing)
        {
            return true;
        }

        /// <summary>
        /// The add person tab command.
        /// </summary>
        /// <param name="nothing">The nothing.</param>
        private void AddPersonTab(object nothing)
        {
            var personBuid = new Buid(Library.Id, BloomEntity.Person, Guid.NewGuid());
            EventAggregator.GetEvent<NewPersonTabEvent>().Publish(personBuid);
        }

        /// <summary>
        /// Gets or sets the add artist tab command.
        /// </summary>
        public ICommand AddArtistTabCommand { get; set; }

        /// <summary>
        /// Determines whether this instance use the add artist tab command.
        /// </summary>
        private bool CanAddArtistTab(object nothing)
        {
            return true;
        }

        /// <summary>
        /// The add artist command.
        /// </summary>
        /// <param name="nothing">The nothing.</param>
        private void AddArtistTab(object nothing)
        {
            var artistBuid = new Buid(Library.Id, BloomEntity.Artist, Guid.NewGuid());
            EventAggregator.GetEvent<NewArtistTabEvent>().Publish(artistBuid);
        }

        /// <summary>
        /// Gets or sets the add album tab command.
        /// </summary>
        public ICommand AddAlbumTabCommand { get; set; }

        /// <summary>
        /// Determines whether this instance can use the add album tab command.
        /// </summary>
        private bool CanAddAlbumTab(object nothing)
        {
            return true;
        }

        /// <summary>
        /// The add album tab command.
        /// </summary>
        private void AddAlbumTab(object nothing)
        {
            var albumBuid = new Buid(Library.Id, BloomEntity.Album, Guid.NewGuid());
            EventAggregator.GetEvent<NewAlbumTabEvent>().Publish(albumBuid);
        }

        /// <summary>
        /// Gets or sets the add song tab command.
        /// </summary>
        public ICommand AddSongTabCommand { get; set; }

        /// <summary>
        /// Determines whether this instance can use the add song command.
        /// </summary>
        private bool CanAddSongTab(object nothing)
        {
            return true;
        }

        /// <summary>
        /// The add song command.
        /// </summary>
        /// <param name="nothing">The nothing.</param>
        private void AddSongTab(object nothing)
        {
            var songBuid = new Buid(Library.Id, BloomEntity.Song, Guid.NewGuid());
            EventAggregator.GetEvent<NewSongTabEvent>().Publish(songBuid);
        }

        /// <summary>
        /// Gets or sets the add playlist tab command.
        /// </summary>
        public ICommand AddPlaylistTabCommand { get; set; }

        /// <summary>
        /// Determines whether this instance can use the add playlist tab command.
        /// </summary>
        private bool CanAddPlaylistTab(object nothing)
        {
            return true;
        }

        /// <summary>
        /// The add playlist tab command.
        /// </summary>
        private void AddPlaylistTab(object nothing)
        {
            var playlistBuid = new Buid(Library.Id, BloomEntity.Album, Guid.NewGuid());
            EventAggregator.GetEvent<NewPlaylistTabEvent>().Publish(playlistBuid);
        }
    }
}
