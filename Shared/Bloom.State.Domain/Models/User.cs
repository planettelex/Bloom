using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using Bloom.Domain.Models;
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
            if (person.Photos != null && person.Photos.Count > 0)
            {
                var profilePhoto = person.Photos[0];
                user.ProfileImageUrl = profilePhoto.Photo != null ? profilePhoto.Photo.Url : null;
            }
            return user;
        }
        
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
        public DateTime? LastLogin { get; set; }

        /// <summary>
        /// Creates a new person from this user.
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
            if (!string.IsNullOrEmpty(ProfileImageUrl))
            {
                var photo = Photo.Create(ProfileImageUrl);
                person.Photos = new List<PersonPhoto> { new PersonPhoto(person, photo) };
            }
            return person;
        }
    }
}
