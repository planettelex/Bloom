namespace Bloom.Browser
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
            _skin = Properties.Settings.Default.Skin;
        }

        /// <summary>
        /// Gets or sets the skin name.
        /// </summary>
        public string Skin
        {
            get
            {
                return _skin;
            }
            set
            {
                _skin = value;
                Properties.Settings.Default.Skin = _skin;
            }
        }
        private string _skin;
    }
}
