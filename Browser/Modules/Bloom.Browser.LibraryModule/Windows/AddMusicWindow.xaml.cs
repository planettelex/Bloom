using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Bloom.Browser.LibraryModule.WindowModels;

namespace Bloom.Browser.LibraryModule.Windows
{
    /// <summary>
    /// Interaction logic for AddMusicWindow.xaml
    /// </summary>
    public partial class AddMusicWindow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddMusicWindow"/> class.
        /// </summary>
        /// <param name="windowModel">The window model.</param>
        public AddMusicWindow(AddMusicWindowModel windowModel)
        {
            InitializeComponent();
            DataContext = windowModel;
        }
    }
}
