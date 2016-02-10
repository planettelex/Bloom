using System.Windows.Media;
using Bloom.UserModule.WindowModels;
using Microsoft.Practices.Prism.Commands;

namespace Bloom.UserModule.Windows
{
    /// <summary>
    /// Interaction logic for ChangeUserWindow.xaml
    /// </summary>
    public partial class ChangeUserWindow
    {
        public ChangeUserWindow(ChangeUserWindowModel windowModel)
        {
            InitializeComponent();
            windowModel.IsLoading = true;
            windowModel.ChangeUserCommand = new DelegateCommand<object>(ChangeUser, CanChangeUser);
            windowModel.CancelCommand = new DelegateCommand<object>(Cancel, CanCancel);
            DataContext = windowModel;
        }

        protected ChangeUserWindowModel Model
        {
            get { return (ChangeUserWindowModel) DataContext; }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            Model.IsLoading = false;
        }

        private bool CanChangeUser(object nothing)
        {
            return true;
        }

        private void ChangeUser(object nothing)
        {
            Model.ChangeUser();
            Close();
        }

        private bool CanCancel(object nothing)
        {
            return true;
        }

        private void Cancel(object nothing)
        {
            Close();
        }
    }
}
