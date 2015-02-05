using System;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents an activity (e.g. Workout, Cleaning).
    /// </summary>
    public class Activity
    {
        /// <summary>
        /// Gets or sets the activity identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the activity name.
        /// </summary>
        public string Name { get; set; }
    }
}
