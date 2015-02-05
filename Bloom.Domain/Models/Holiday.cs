using System;
using Bloom.Domain.Enums;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a holiday.
    /// </summary>
    public class Holiday
    {
        /// <summary>
        /// Gets or sets the holiday identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the holiday name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the holiday description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the month the holiday starts.
        /// </summary>
        public Month StartMonth { get; set; }

        /// <summary>
        /// Gets or sets the day the holiday starts.
        /// </summary>
        public int StartDay { get; set; }

        /// <summary>
        /// Gets or sets the month the holiday ends.
        /// </summary>
        public Month EndMonth { get; set; }

        /// <summary>
        /// Gets or sets the day the holiday ends.
        /// </summary>
        public int EndDay { get; set; }
    }
}
