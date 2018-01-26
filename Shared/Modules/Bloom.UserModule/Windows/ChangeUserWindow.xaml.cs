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
        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeUserWindow"/> class.
        /// </summary>
        /// <param name="windowModel">The window model.</param>
        public ChangeUserWindow(ChangeUserWindowModel windowModel)
        {
            InitializeComponent();
            windowModel.IsLoading = true;
            windowModel.ChangeUserCommand = new DelegateCommand<object>(ChangeUser, CanChangeUser);
            windowModel.CancelCommand = new DelegateCommand<object>(Cancel, CanCancel);

            DataContext = windowModel;
        }

        /// <summary>
        /// Gets the window model.
        /// </summary>
        protected ChangeUserWindowModel WindowModel => (ChangeUserWindowModel) DataContext;

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
        /// Determines whether this window can use the change user command.
        /// </summary>
        private bool CanChangeUser(object nothing)
        {
            return true;
        }

        /// <summary>
        /// The change user command.
        /// </summary>
        private void ChangeUser(object nothing)
        {
            WindowModel.ChangeUser();
            Close();
        }
    }
}
