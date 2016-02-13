using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using Microsoft.Practices.Prism.Mvvm;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a person.
    /// </summary>
    [Table(Name = "person")]
    public class Person : BindableBase
    {
        /// <summary>
        /// Creates a new person instance.
        /// </summary>
        /// <param name="name">The person's name.</param>
        public static Person Create(string name)
        {
            var person = new Person
            {
                Id = Guid.NewGuid(),
                Name = name
            };
            return person;
        }

        /// <summary>
        /// Gets or sets the person identifier.
        /// </summary>
        [Column(Name = "id", IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the person's name.
        /// </summary>
        [Column(Name = "name")]
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        private string _name;

        /// <summary>
        /// Gets or sets the date the person was born.
        /// </summary>
        [Column(Name = "born_on")]
        public DateTime? BornOn { get; set; }

        /// <summary>
        /// Gets or sets the date the person died.
        /// </summary>
        [Column(Name = "died_on")]
        public DateTime? DiedOn { get; set; }

        /// <summary>
        /// Gets or sets the person's bio.
        /// </summary>
        [Column(Name = "bio")]
        public string Bio { get; set; }

        /// <summary>
        /// Gets or sets the person's Twitter username.
        /// </summary>
        [Column(Name = "twitter")]
        public string Twitter { get; set; }

        /// <summary>
        /// Gets or sets the person's photos.
        /// </summary>
        public List<Photo> Photos { get; set; }

        public void SetProfileImage(string profileImageUrl)
        {
            if (Photos == null)
                Photos = new List<Photo> { Photo.Create(profileImageUrl) };
            else if (Photos.Count == 0)
                Photos.Add(Photo.Create(profileImageUrl));
            else
                Photos[0].Url = profileImageUrl;
        }

        /// <summary>
        /// Gets the profile image.
        /// </summary>
        public Photo ProfileImage
        {
            get
            {
                if (Photos == null || Photos.Count == 0)
                    return null;

                return Photos[0];
            }
            set
            {
                if (value == null || value.Id == Guid.Empty)
                    return;

                if (Photos == null)
                    Photos = new List<Photo> { value };
                else if (Photos.Count == 0)
                    Photos.Add(value);
                else 
                    Photos[0] = value;
            }
        }
    }
}
