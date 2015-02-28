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
        public BloomState()
        {
            Connections = new Connections();
        }

        public Connections Connections { get; set; }

        public AnalyticsState Analytics { get; set; }

        public BrowserState Browser { get; set; }

        public PlayerState Player { get; set; }
    }
}
