using System.Windows.Input;
using Bloom.PubSubEvents;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Bloom.Taxonomies.ViewModels
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
            _eventAggregator.GetEvent<NewLibraryTabEvent>().Publish(null);
        }

        public ICommand AddPersonTabCommand { get; set; }

        private bool CanAddPersonTab(object nothing)
        {
            return true;
        }

        private void AddPersonTab(object nothing)
        {
            _eventAggregator.GetEvent<NewPersonTabEvent>().Publish(null);
        }

        public ICommand AddArtistTabCommand { get; set; }

        private bool CanAddArtistTab(object nothing)
        {
            return true;
        }

        private void AddArtistTab(object nothing)
        {
            _eventAggregator.GetEvent<NewArtistTabEvent>().Publish(null);
        }

        public ICommand AddAlbumTabCommand { get; set; }

        private bool CanAddAlbumTab(object nothing)
        {
            return true;
        }

        private void AddAlbumTab(object nothing)
        {
            _eventAggregator.GetEvent<NewAlbumTabEvent>().Publish(null);
        }

        public ICommand AddSongTabCommand { get; set; }

        private bool CanAddSongTab(object nothing)
        {
            return true;
        }

        private void AddSongTab(object nothing)
        {
            _eventAggregator.GetEvent<NewSongTabEvent>().Publish(null);
        }

        public ICommand AddPlaylistTabCommand { get; set; }

        private bool CanAddPlaylistTab(object nothing)
        {
            return true;
        }

        private void AddPlaylistTab(object nothing)
        {
            _eventAggregator.GetEvent<NewPlaylistTabEvent>().Publish(null);
        }
    }
}
