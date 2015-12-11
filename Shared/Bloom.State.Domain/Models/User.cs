using System;
using System.Data.Linq.Mapping;
using Microsoft.Practices.Prism.Mvvm;

namespace Bloom.State.Domain.Models
{
    /// <summary>
    /// Represents a person who uses the Bloom suite.
    /// </summary>
    [Table(Name = "user")]
    public class User : BindableBase
    {
        /// <summary>
        /// Gets or sets the user's person identifier.
        /// </summary>
        [Column(Name = "person_id", IsPrimaryKey = true)]
        public Guid PersonId { get; set; }

        /// <summary>
        /// Gets or sets the user's name.
        /// </summary>
        [Column(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the user's birthday.
        /// </summary>
        [Column(Name = "birthday")]
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// Gets or sets the user's Twitter.
        /// </summary>
        [Column(Name = "twitter")]
        public string Twitter { get; set; }

        /// <summary>
        /// Gets or sets the user's profile image URL.
        /// </summary>
        [Column(Name = "profile_image_url")]
        public string ProfileImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the user's last login datetime.
        /// </summary>
        [Column(Name = "last_login")]
        public DateTime LastLogin { get; set; }
    }
}
