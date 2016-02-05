using System;
using System.Data.Linq.Mapping;

namespace Bloom.State.Domain.Models
{
    [Table(Name = "suite_state")]
    public class SuiteState
    {
        public SuiteState()
        {
            SuiteName = Properties.Settings.Default.SuiteName;
        }

        [Column(Name = "suite_name", IsPrimaryKey = true)]
        public string SuiteName { get; set; }

        [Column(Name = "last_process_access", UpdateCheck = UpdateCheck.Never)]
        public string LastProcessAccess { get; set; }

        [Column(Name = "process_accessed_on", UpdateCheck = UpdateCheck.Never)]
        public DateTime ProcessAccessedOn { get; set; }
    }
}
