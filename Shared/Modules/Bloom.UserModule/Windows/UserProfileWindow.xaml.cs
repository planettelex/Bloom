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
        public UserProfileWindow(UserProfileWindowModel windowModel)
        {
            InitializeComponent();
            _profileImageFileDialog = new OpenFileDialog { Multiselect = false };
            windowModel.CancelCommand = new DelegateCommand<object>(CancelWindow, CanCancelWindow);
            windowModel.SetProfileImageCommand = new DelegateCommand<object>(SetProfileImage, CanSetProfileImage);

            if (string.IsNullOrEmpty(windowModel.ProfileImagePath))
                PlaceholderImage.Visibility = Visibility.Visible;
            else
                ProfileImage.Visibility = Visibility.Visible;

            DataContext = windowModel;
        }
        private readonly OpenFileDialog _profileImageFileDialog;

        protected UserProfileWindowModel Model
        {
            get { return (UserProfileWindowModel) DataContext; }
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
                Model.ProfileImagePath = _profileImageFileDialog.FileName; // Temp
                // Copy image to Bloom local resources
                PlaceholderImage.Visibility = Visibility.Collapsed;
                ProfileImage.Visibility = Visibility.Visible;
            }
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
