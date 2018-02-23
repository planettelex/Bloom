using System;
using System.Data.Linq.Mapping;
using Bloom.Domain.Models;
using Prism.Mvvm;

namespace Bloom.State.Domain.Models
{
    /// <summary>
    /// Represents a person who uses any application.
    /// <seealso cref="BindableBase" />
    /// </summary>
    [Table(Name = "user")]
    public class User : BindableBase
    {
        /// <summary>
        /// Creates a new user instance from a person.
        /// </summary>
        /// <param name="person">The person.</param>
        public static User Create(Person person)
        {
            if (person == null)
                return null;

            var user = new User
            {
                PersonId = person.Id,
                Name = person.Name,
                Birthday = person.BornOn,
                Twitter = person.Twitter
            };
            if (person.ProfileImage != null)
                user.ProfileImagePath = person.ProfileImage.FilePath;
            
            return user;
        }

        /// <summary>
        /// Gets the anonymous user.
        /// </summary>
        public static User Anonymous => new User
        {
            PersonId = Guid.Empty,
            Name = "Anonymous"
        };

        /// <summary>
        /// Gets or sets the user's person identifier.
        /// </summary>
        [Column(Name = "person_id", IsPrimaryKey = true)]
        public Guid PersonId { get; set; }

        /// <summary>
        /// Gets or sets the user's name.
        /// </summary>
        [Column(Name = "name", UpdateCheck = UpdateCheck.Never)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the user's birthday.
        /// </summary>
        [Column(Name = "birthday", UpdateCheck = UpdateCheck.Never)]
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// Gets or sets the user's Twitter.
        /// </summary>
        [Column(Name = "twitter", UpdateCheck = UpdateCheck.Never)]
        public string Twitter { get; set; }

        /// <summary>
        /// Gets or sets the user's profile image path.
        /// </summary>
        [Column(Name = "profile_image_path", UpdateCheck = UpdateCheck.Never)]
        public string ProfileImagePath { get; set; }

        /// <summary>
        /// Gets or sets the user's last login datetime.
        /// </summary>
        [Column(Name = "last_login", UpdateCheck = UpdateCheck.Never)]
        public DateTime? LastLogin { get; set; }

        /// <summary>
        /// Returns this user as a new person object.
        /// </summary>
        public Person AsPerson()
        {
            var person = new Person
            {
                Id = PersonId,
                Name = Name,
                BornOn = Birthday,
                Twitter = Twitter
            };
            if (!string.IsNullOrEmpty(ProfileImagePath))
                person.SetProfileImage(ProfileImagePath);
            
            return person;
        }
    }
}
