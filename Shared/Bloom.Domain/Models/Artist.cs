using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using Microsoft.Practices.Prism.Mvvm;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a musical artist.
    /// </summary>
    [Table(Name = "artist")]
    public class Artist : BindableBase
    {
        /// <summary>
        /// Creates a new artist instance.
        /// </summary>
        /// <param name="name">The artist name.</param>
        public static Artist Create(string name)
        {
            return new Artist
            {
                Id = Guid.NewGuid(),
                Name = name
            };
        }
        
        /// <summary>
        /// Gets or sets the artist identifier.
        /// </summary>
        [Column(Name = "id", IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the artist name.
        /// </summary>
        [Column(Name = "name")]
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        private string _name;

        /// <summary>
        /// Gets or sets the date this artist started their career.
        /// </summary>
        [Column(Name = "started_on")]
        public DateTime? StartedOn { get; set; }

        /// <summary>
        /// Gets or sets the date this artist ended their career.
        /// </summary>
        [Column(Name = "ended_on")]
        public DateTime? EndedOn { get; set; }

        /// <summary>
        /// Gets or sets the artist's bio.
        /// </summary>
        [Column(Name = "bio")]
        public string Bio { get; set; }

        /// <summary>
        /// Gets or sets the artist's Twitter username.
        /// </summary>
        [Column(Name = "twitter")]
        public string Twitter { get; set; }

        /// <summary>
        /// Gets or sets a whether this is a solo artist.
        /// </summary>
        [Column(Name = "is_solo")]
        public bool IsSolo { get; set; }

        /// <summary>
        /// Gets or sets the artist's photos.
        /// </summary>
        public List<Photo> Photos { get; set; }

        /// <summary>
        /// Gets or sets the artist members.
        /// </summary>
        public List<ArtistMember> Members { get; set; }

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
