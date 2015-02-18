using System;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents an activity (e.g. Workout, Cleaning).
    /// </summary>
    [Table(Name = "activity")]
    public class Activity
    {
        /// <summary>
        /// Creates a new activity instance.
        /// </summary>
        /// <param name="name">The activity name.</param>
        public static Activity Create(string name)
        {
            return new Activity
            {
                Id = Guid.NewGuid(),
                Name = name
            };
        }

        /// <summary>
        /// Gets or sets the activity identifier.
        /// </summary>
        [Column(Name = "id", IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the activity name.
        /// </summary>
        [Column(Name = "name")]
        public string Name { get; set; }
    }
}
