using System;

namespace Bloom.Browser.Common
{
    /// <summary>
    /// Encapsulates the state of the player application.
    /// </summary>
    public class State
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="State"/> class.
        /// </summary>
        public State()
        {
            _selectedTabId = Properties.Settings.Default.SelectedTabId;
            _skin = Properties.Settings.Default.Skin;
        }

        /// <summary>
        /// Gets or sets the active tab identifier.
        /// </summary>
        public Guid SelectedTabId
        {
            get { return _selectedTabId; }
            set
            {
                _selectedTabId = value;
                Properties.Settings.Default.SelectedTabId = _selectedTabId;
            }
        }
        private Guid _selectedTabId;

        /// <summary>
        /// Gets or sets the skin name.
        /// </summary>
        public string Skin
        {
            get { return _skin; }
            set
            {
                _skin = value;
                Properties.Settings.Default.Skin = _skin;
            }
        }
        private string _skin;

        /// <summary>
        /// Saves the state to the default user settings.
        /// </summary>
        public void Save()
        {
            Properties.Settings.Default.Save();
        }
    }
}
