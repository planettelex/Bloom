using System;
using System.Windows.Input;
using Bloom.Common;
using Bloom.PubSubEvents;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Bloom.TaxonomiesModule.ViewModels
{
    public class TaxonomiesViewModel
    {
        public TaxonomiesViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            AddLibraryTabCommand = new DelegateCommand<object>(AddLibraryTab, CanAddLibraryTab);
            AddPersonTabCommand = new DelegateCommand<object>(AddPersonTab, CanAddPersonTab);
            AddArtistTabCommand = new DelegateCommand<object>(AddArtistTab, CanAddArtistTab);
            AddAlbumTabCommand = new DelegateCommand<object>(AddAlbumTab, CanAddAlbumTab);
            AddSongTabCommand = new DelegateCommand<object>(AddSongTab, CanAddSongTab);
            AddPlaylistTabCommand = new DelegateCommand<object>(AddPlaylistTab, CanAddPlaylistTab);
        }
        private readonly IEventAggregator _eventAggregator;

        public ICommand AddLibraryTabCommand { get; set; }

        private bool CanAddLibraryTab(object nothing)
        {
            return true;
        }

        private void AddLibraryTab(object nothing)
        {
            var libraryId = Guid.NewGuid();
            _eventAggregator.GetEvent<NewLibraryTabEvent>().Publish(libraryId);
        }

        public ICommand AddPersonTabCommand { get; set; }

        private bool CanAddPersonTab(object nothing)
        {
            return true;
        }

        private void AddPersonTab(object nothing)
        {
            var personBuid = new Buid(Guid.NewGuid(), BloomEntity.Person, Guid.NewGuid());
            _eventAggregator.GetEvent<NewPersonTabEvent>().Publish(personBuid);
        }

        public ICommand AddArtistTabCommand { get; set; }

        private bool CanAddArtistTab(object nothing)
        {
            return true;
        }

        private void AddArtistTab(object nothing)
        {
            var artistBuid = new Buid(Guid.NewGuid(), BloomEntity.Artist, Guid.NewGuid());
            _eventAggregator.GetEvent<NewArtistTabEvent>().Publish(artistBuid);
        }

        public ICommand AddAlbumTabCommand { get; set; }

        private bool CanAddAlbumTab(object nothing)
        {
            return true;
        }

        private void AddAlbumTab(object nothing)
        {
            var albumBuid = new Buid(Guid.NewGuid(), BloomEntity.Album, Guid.NewGuid());
            _eventAggregator.GetEvent<NewAlbumTabEvent>().Publish(albumBuid);
        }

        public ICommand AddSongTabCommand { get; set; }

        private bool CanAddSongTab(object nothing)
        {
            return true;
        }

        private void AddSongTab(object nothing)
        {
            var songBuid = new Buid(Guid.NewGuid(), BloomEntity.Song, Guid.NewGuid());
            _eventAggregator.GetEvent<NewSongTabEvent>().Publish(songBuid);
        }

        public ICommand AddPlaylistTabCommand { get; set; }

        private bool CanAddPlaylistTab(object nothing)
        {
            return true;
        }

        private void AddPlaylistTab(object nothing)
        {
            var playlistBuid = new Buid(Guid.NewGuid(), BloomEntity.Album, Guid.NewGuid());
            _eventAggregator.GetEvent<NewPlaylistTabEvent>().Publish(playlistBuid);
        }
    }
}
