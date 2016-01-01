using Bloom.LibraryModule.WindowModels;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Bloom.LibraryModule.Windows
{
    /// <summary>
    /// Interaction logic for ConnectedLibrariesWindow.xaml
    /// </summary>
    public partial class ConnectedLibrariesWindow
    {
        public ConnectedLibrariesWindow(ConnectedLibrariesWindowModel windowModel, IEventAggregator eventAggregator)
        {
            InitializeComponent();
            _eventAggregator = eventAggregator;
            windowModel.CloseCommand = new DelegateCommand<object>(Close, CanClose);
            DataContext = windowModel;
        }
        private readonly IEventAggregator _eventAggregator;

        private bool CanClose(object nothing)
        {
            return true;
        }

        private void Close(object nothing)
        {
            Close();
        }
    }
}
