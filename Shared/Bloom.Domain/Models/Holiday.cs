using System;
using System.Data.Linq.Mapping;
using Bloom.Domain.Enums;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a holiday.
    /// </summary>
    [Table(Name = "holiday")]
    public class Holiday
    {
        /// <summary>
        /// Creates a new holiday instance.
        /// </summary>
        /// <param name="name">The holiday name.</param>
        public static Holiday Create(string name)
        {
            return new Holiday
            {
                Id = Guid.NewGuid(),
                Name = name
            };
        }

        /// <summary>
        /// Gets or sets the holiday identifier.
        /// </summary>
        [Column(Name = "id", IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the holiday name.
        /// </summary>
        [Column(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the holiday description.
        /// </summary>
        [Column(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the month the holiday starts.
        /// </summary>
        [Column(Name = "start_month")]
        public Month StartMonth { get; set; }

        /// <summary>
        /// Gets or sets the day the holiday starts.
        /// </summary>
        [Column(Name = "start_day")]
        public int StartDay { get; set; }

        /// <summary>
        /// Gets or sets the month the holiday ends.
        /// </summary>
        [Column(Name = "end_month")]
        public Month EndMonth { get; set; }

        /// <summary>
        /// Gets or sets the day the holiday ends.
        /// </summary>
        [Column(Name = "end_day")]
        public int EndDay { get; set; }
    }
}
