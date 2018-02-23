using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using Prism.Mvvm;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a person.
    /// </summary>
    /// <seealso cref="BindableBase" />
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
        public DateTime? BornOn
        {
            get { return _bornOn; }
            set { SetProperty(ref _bornOn, value); }
        }
        private DateTime? _bornOn;

        /// <summary>
        /// Gets or sets the date the person died.
        /// </summary>
        [Column(Name = "died_on")]
        public DateTime? DiedOn
        {
            get { return _diedOn; }
            set { SetProperty(ref _diedOn, value); }
        }
        private DateTime? _diedOn;

        /// <summary>
        /// Gets or sets the identifier of the city this person is from.
        /// </summary>
        [Column(Name = "from_city_id")]
        public Guid? FromCityId { get; set; }

        /// <summary>
        /// Gets or sets the person's bio.
        /// </summary>
        [Column(Name = "bio")]
        public string Bio
        {
            get { return _bio; }
            set { SetProperty(ref _bio, value); }
        }
        private string _bio;

        /// <summary>
        /// Gets or sets the person's Twitter username.
        /// </summary>
        [Column(Name = "twitter")]
        public string Twitter
        {
            get { return _twitter; }
            set { SetProperty(ref _twitter, value); }
        }
        private string _twitter;

        /// <summary>
        /// Gets or sets a value indicating whether this library follows this person.
        /// </summary>
        [Column(Name = "follow")]
        public bool Follow { get; set; }

        /// <summary>
        /// Gets or sets the person's photos.
        /// </summary>
        public List<Photo> Photos { get; set; }

        /// <summary>
        /// Gets or sets the profile image.
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
                    Photos.Insert(0, value);
            }
        }

        /// <summary>
        /// Sets the profile image.
        /// </summary>
        /// <param name="profileImagePath">The profile image path.</param>
        public void SetProfileImage(string profileImagePath)
        {
            if (Photos == null)
                Photos = new List<Photo> { Photo.Create(profileImagePath) };
            else if (Photos.Count == 0)
                Photos.Add(Photo.Create(profileImagePath));
            else
                Photos[0].FilePath = profileImagePath;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        public override string ToString()
        {
            return Name;
        }
    }
}
