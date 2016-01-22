using System;
using System.Data.Linq.Mapping;

namespace Bloom.State.Domain.Models
{
    [Table(Name = "suite_state")]
    public class SuiteState
    {
        public static SuiteState Create()
        {
            var suiteState = new SuiteState
            {
                Id = Guid.NewGuid()
            };

            return suiteState;
        }

        [Column(Name = "id", IsPrimaryKey = true)]
        public Guid Id { get; set; }

        [Column(Name = "last_process_access")]
        public string LastProcessAccess { get; set; }

        [Column(Name = "process_accessed_on")]
        public DateTime ProcessAccessedOn { get; set; }
    }
}
