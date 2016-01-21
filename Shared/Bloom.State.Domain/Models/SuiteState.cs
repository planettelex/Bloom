using System.Data.Linq.Mapping;

namespace Bloom.State.Domain.Models
{
    [Table(Name = "suite_state")]
    public class SuiteState
    {
        [Column(Name = "last_process_access", IsPrimaryKey = true)]
        public string LastProcessAccess { get; set; }
    }
}
