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
        public UserProfileWindow(UserProfileWindowModel windowModel, IFileSystemService fileSystemService)
        {
            InitializeComponent();
            windowModel.IsLoading = true;
            _fileSystemService = fileSystemService;
            _profileImageFileDialog = new OpenFileDialog { Multiselect = false };
            windowModel.CancelCommand = new DelegateCommand<object>(CancelWindow, CanCancelWindow);
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

        protected UserProfileWindowModel Model
        {
            get { return (UserProfileWindowModel) DataContext; }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            Model.IsLoading = false;
        }

        private bool CanCancelWindow(object nothing)
        {
            return true;
        }

        private void CancelWindow(object nothing)
        {
            Close();
        }

        private bool CanSetProfileImage(object nothing)
        {
            return true;
        }

        private void SetProfileImage(object nothing)
        {
            _profileImageFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            _profileImageFileDialog.FileName = string.Empty;
            _profileImageFileDialog.Title = "Choose an image for your profile picture.";

            var result = _profileImageFileDialog.ShowDialog();
            if (result != null && result.Value)
            {
                var isValid = Model["ProfileImagePath"] == null;
                if (isValid)
                {
                    var filePath = _profileImageFileDialog.FileName;
                    Model.ProfileImagePath = _fileSystemService.CopyProfileImage(Model.State.User, filePath);
                    PlaceholderImage.Visibility = Visibility.Collapsed;
                    ProfileImage.Visibility = Visibility.Visible;
                }
            }
        }

        private bool CanSaveChanges(object nothing)
        {
            return true;
        }

        private void SaveChanges(object nothing)
        {
            if (Model.HasChanges())
                Model.SaveChanges();

            Close();
        }

        private void OnTwitterUpdated(object sender, KeyEventArgs e)
        {
            var textBox = ((TextBox) sender);
            var twitterName = textBox.Text;

            textBox.Text = "@" + twitterName.Trim('@');
            textBox.CaretIndex = textBox.Text.Length;
        }
    }
}
