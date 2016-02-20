using System;
using System.Data.Linq.Mapping;

namespace Bloom.State.Domain.Models
{
    /// <summary>
    /// Represents state information common to the entire application suite.
    /// </summary>
    [Table(Name = "suite_state")]
    public class SuiteState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SuiteState"/> class.
        /// </summary>
        public SuiteState()
        {
            SuiteName = Properties.Settings.Default.SuiteName;
        }

        /// <summary>
        /// Gets or sets the name of the suite.
        /// </summary>
        [Column(Name = "suite_name", IsPrimaryKey = true)]
        public string SuiteName { get; set; }

        /// <summary>
        /// Gets or sets the last process to access the state data source.
        /// </summary>
        [Column(Name = "last_process_access", UpdateCheck = UpdateCheck.Never)]
        public string LastProcessAccess { get; set; }

        /// <summary>
        /// Gets or sets when the last process to access state has done so.
        /// </summary>
        [Column(Name = "process_accessed_on", UpdateCheck = UpdateCheck.Never)]
        public DateTime ProcessAccessedOn { get; set; }
    }
}
