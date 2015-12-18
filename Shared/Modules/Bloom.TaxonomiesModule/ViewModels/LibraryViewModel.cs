using System;
using System.Windows.Input;
using Bloom.Common;
using Bloom.Domain.Models;
using Bloom.PubSubEvents;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Bloom.TaxonomiesModule.ViewModels
{
    public class LibraryViewModel
    {
        public LibraryViewModel(Library library, IEventAggregator eventAggregator)
        {
            if (library == null)
                throw new ArgumentNullException("library");
            
            _eventAggregator = eventAggregator;
            Library = library;

            AddHomeTabCommand = new DelegateCommand<object>(AddHomeTab, CanAddHomeTab);
            AddLibraryTabCommand = new DelegateCommand<object>(AddLibraryTab, CanAddLibraryTab);
            AddPersonTabCommand = new DelegateCommand<object>(AddPersonTab, CanAddPersonTab);
            AddArtistTabCommand = new DelegateCommand<object>(AddArtistTab, CanAddArtistTab);
            AddAlbumTabCommand = new DelegateCommand<object>(AddAlbumTab, CanAddAlbumTab);
            AddSongTabCommand = new DelegateCommand<object>(AddSongTab, CanAddSongTab);
            AddPlaylistTabCommand = new DelegateCommand<object>(AddPlaylistTab, CanAddPlaylistTab);
        }
        private readonly IEventAggregator _eventAggregator;

        public Library Library { get; set; }

        public ICommand AddHomeTabCommand { get; set; }

        private bool CanAddHomeTab(object nothing)
        {
            return true;
        }

        private void AddHomeTab(object nothing)
        {
            _eventAggregator.GetEvent<NewHomeTabEvent>().Publish(null);
        }
        
        public ICommand AddLibraryTabCommand { get; set; }

        private bool CanAddLibraryTab(object nothing)
        {
            return true;
        }

        private void AddLibraryTab(object nothing)
        {
            _eventAggregator.GetEvent<NewLibraryTabEvent>().Publish(Library.Id);
        }

        public ICommand AddPersonTabCommand { get; set; }

        private bool CanAddPersonTab(object nothing)
        {
            return true;
        }

        private void AddPersonTab(object nothing)
        {
            var personBuid = new Buid(Library.Id, BloomEntity.Person, Guid.NewGuid());
            _eventAggregator.GetEvent<NewPersonTabEvent>().Publish(personBuid);
        }

        public ICommand AddArtistTabCommand { get; set; }

        private bool CanAddArtistTab(object nothing)
        {
            return true;
        }

        private void AddArtistTab(object nothing)
        {
            var artistBuid = new Buid(Library.Id, BloomEntity.Artist, Guid.NewGuid());
            _eventAggregator.GetEvent<NewArtistTabEvent>().Publish(artistBuid);
        }

        public ICommand AddAlbumTabCommand { get; set; }

        private bool CanAddAlbumTab(object nothing)
        {
            return true;
        }

        private void AddAlbumTab(object nothing)
        {
            var albumBuid = new Buid(Library.Id, BloomEntity.Album, Guid.NewGuid());
            _eventAggregator.GetEvent<NewAlbumTabEvent>().Publish(albumBuid);
        }

        public ICommand AddSongTabCommand { get; set; }

        private bool CanAddSongTab(object nothing)
        {
            return true;
        }

        private void AddSongTab(object nothing)
        {
            var songBuid = new Buid(Library.Id, BloomEntity.Song, Guid.NewGuid());
            _eventAggregator.GetEvent<NewSongTabEvent>().Publish(songBuid);
        }

        public ICommand AddPlaylistTabCommand { get; set; }

        private bool CanAddPlaylistTab(object nothing)
        {
            return true;
        }

        private void AddPlaylistTab(object nothing)
        {
            var playlistBuid = new Buid(Library.Id, BloomEntity.Album, Guid.NewGuid());
            _eventAggregator.GetEvent<NewPlaylistTabEvent>().Publish(playlistBuid);
        }
    }
}
