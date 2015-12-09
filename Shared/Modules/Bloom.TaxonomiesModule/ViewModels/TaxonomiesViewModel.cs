using System;
using System.Windows.Input;
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
            var personId = Guid.NewGuid();
            _eventAggregator.GetEvent<NewPersonTabEvent>().Publish(personId);
        }

        public ICommand AddArtistTabCommand { get; set; }

        private bool CanAddArtistTab(object nothing)
        {
            return true;
        }

        private void AddArtistTab(object nothing)
        {
            var artistId = Guid.NewGuid();
            _eventAggregator.GetEvent<NewArtistTabEvent>().Publish(artistId);
        }

        public ICommand AddAlbumTabCommand { get; set; }

        private bool CanAddAlbumTab(object nothing)
        {
            return true;
        }

        private void AddAlbumTab(object nothing)
        {
            var albumId = Guid.NewGuid();
            _eventAggregator.GetEvent<NewAlbumTabEvent>().Publish(albumId);
        }

        public ICommand AddSongTabCommand { get; set; }

        private bool CanAddSongTab(object nothing)
        {
            return true;
        }

        private void AddSongTab(object nothing)
        {
            var songId = Guid.NewGuid();
            _eventAggregator.GetEvent<NewSongTabEvent>().Publish(songId);
        }

        public ICommand AddPlaylistTabCommand { get; set; }

        private bool CanAddPlaylistTab(object nothing)
        {
            return true;
        }

        private void AddPlaylistTab(object nothing)
        {
            var playlistId = Guid.NewGuid();
            _eventAggregator.GetEvent<NewPlaylistTabEvent>().Publish(playlistId);
        }
    }
}
