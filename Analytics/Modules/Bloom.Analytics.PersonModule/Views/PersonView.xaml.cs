﻿using Bloom.Analytics.PersonModule.ViewModels;

namespace Bloom.Analytics.PersonModule.Views
{
    /// <summary>
    /// Interaction logic for PersonView.xaml
    /// </summary>
    public partial class PersonView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonView"/> class.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        public PersonView(PersonViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}