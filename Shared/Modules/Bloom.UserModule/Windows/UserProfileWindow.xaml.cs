using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Bloom.Services;
using Bloom.UserModule.WindowModels;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Win32;

namespace Bloom.UserModule.Windows
{
    /// <summary>
    /// Interaction logic for UserProfileWindow.xaml
    /// </summary>
    public partial class UserProfileWindow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserProfileWindow"/> class.
        /// </summary>
        /// <param name="windowModel">The window model.</param>
        /// <param name="fileSystemService">The file system service.</param>
        public UserProfileWindow(UserProfileWindowModel windowModel, IFileSystemService fileSystemService)
        {
            InitializeComponent();
            windowModel.IsLoading = true;
            _fileSystemService = fileSystemService;
            _profileImageFileDialog = new OpenFileDialog { Multiselect = false };
            windowModel.CancelCommand = new DelegateCommand<object>(Cancel, CanCancel);
            windowModel.SetProfileImageCommand = new DelegateCommand<object>(SetProfileImage, CanSetProfileImage);
            windowModel.SaveChangesCommand = new DelegateCommand<object>(SaveChanges, CanSaveChanges);

            if (string.IsNullOrEmpty(windowModel.ProfileImagePath))
                PlaceholderImage.Visibility = Visibility.Visible;
            else
                ProfileImage.Visibility = Visibility.Visible;

            DataContext = windowModel;
        }
        private readonly OpenFileDialog _profileImageFileDialog;
        private readonly IFileSystemService _fileSystemService;

        /// <summary>
        /// Gets the window model.
        /// </summary>
        protected UserProfileWindowModel WindowModel => (UserProfileWindowModel) DataContext;

        /// <summary>
        /// When overridden in a derived class, participates in rendering operations that are directed by the layout system. The rendering instructions for this element are not used directly when this method is invoked, and are instead preserved for later asynchronous use by layout and drawing.
        /// </summary>
        /// <param name="drawingContext">The drawing instructions for a specific element. This context is provided to the layout system.</param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            WindowModel.IsLoading = false;
        }

        /// <summary>
        /// Determines whether this window use the cancel command.
        /// </summary>
        private bool CanCancel(object nothing)
        {
            return true;
        }

        /// <summary>
        /// The cancel command.
        /// </summary>
        private void Cancel(object nothing)
        {
            Close();
        }

        /// <summary>
        /// Determines whether this window can use the set profile image command.
        /// </summary>
        private bool CanSetProfileImage(object nothing)
        {
            return true;
        }

        /// <summary>
        /// The set profie image command.
        /// </summary>
        private void SetProfileImage(object nothing)
        {
            _profileImageFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            _profileImageFileDialog.FileName = string.Empty;
            _profileImageFileDialog.Title = "Choose an image for your profile picture.";

            var result = _profileImageFileDialog.ShowDialog();
            if (result != null && result.Value)
            {
                var isValid = WindowModel["ProfileImagePath"] == null;
                if (isValid)
                {
                    var filePath = _profileImageFileDialog.FileName;
                    WindowModel.ProfileImagePath = _fileSystemService.CopyProfileImage(WindowModel.State.User, filePath);
                    PlaceholderImage.Visibility = Visibility.Collapsed;
                    ProfileImage.Visibility = Visibility.Visible;
                }
            }
        }

        /// <summary>
        /// Determines whether this window can use the save changes command.
        /// </summary>
        private bool CanSaveChanges(object nothing)
        {
            return true;
        }

        /// <summary>
        /// The save changes command.
        /// </summary>
        private void SaveChanges(object nothing)
        {
            if (WindowModel.HasChanges())
                WindowModel.SaveChanges();

            Close();
        }

        /// <summary>
        /// Called when a key up occurs in the Twitter text box.
        /// </summary>
        /// <param name="sender">The sender control.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void TwitterUpdated(object sender, KeyEventArgs e)
        {
            var textBox = ((TextBox) sender);
            var twitterName = textBox.Text;

            textBox.Text = "@" + twitterName.Trim('@');
            textBox.CaretIndex = textBox.Text.Length;
        }
    }
}
