using System.Data.Linq.Mapping;
using Microsoft.Practices.Prism.Mvvm;

namespace Bloom.State.Domain.Models
{
    /// <summary>
    /// The state of the entire Bloom suite.
    /// </summary>
    [Table(Name = "state")]
    public class BloomState : BindableBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BloomState"/> class.
        /// </summary>
        public BloomState()
        {
            Connections = new Connections();
        }

        /// <summary>
        /// Gets or sets the data connections.
        /// </summary>
        public Connections Connections { get; set; }

        /// <summary>
        /// Gets or sets the analytics state.
        /// </summary>
        public AnalyticsState Analytics { get; set; }

        /// <summary>
        /// Gets or sets the browser state.
        /// </summary>
        public BrowserState Browser { get; set; }

        /// <summary>
        /// Gets or sets the player state.
        /// </summary>
        public PlayerState Player { get; set; }
    }
}
