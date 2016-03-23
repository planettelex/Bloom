using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using Microsoft.Practices.Prism.Mvvm;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a music playlist.
    /// </summary>
    [Table(Name = "playlist")]
    public class Playlist : BindableBase
    {
        /// <summary>
        /// Creates a new playlist instance.
        /// </summary>
        /// <param name="name">The playlist name.</param>
        /// <param name="createdBy">The person this playlist was created by.</param>
        public static Playlist Create(string name, Person createdBy)
        {
            return new Playlist
            {
                Id = Guid.NewGuid(),
                Name = name,
                CreatedById = createdBy.Id,
                CreatedBy = createdBy,
                CreatedOn = DateTime.Now
            };
        }
        
        /// <summary>
        /// Gets or sets the playlist identifier.
        /// </summary>
        [Column(Name = "id", IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the playlist name.
        /// </summary>
        [Column(Name = "name")]
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        private string _name;

        /// <summary>
        /// Gets or sets the playlist description.
        /// </summary>
        [Column(Name = "description")]
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }
        private string _description;

        /// <summary>
        /// Gets or sets the playlist length, in milliseconds.
        /// </summary>
        [Column(Name = "length")]
        public int Length
        {
            get { return _length; }
            set { SetProperty(ref _length, value); }
        }
        private int _length;

        /// <summary>
        /// Gets or sets the date this playlist was created on.
        /// </summary>
        [Column(Name = "created_on")]
        public DateTime CreatedOn
        {
            get { return _createdOn; }
            set { SetProperty(ref _createdOn, value); }
        }
        private DateTime _createdOn;

        /// <summary>
        /// Gets or sets library owner identifier who created the playlist.
        /// </summary>
        [Column(Name = "created_by_id")]
        public Guid CreatedById { get; set; }

        /// <summary>
        /// Gets or sets library owner who created the playlist.
        /// </summary>
        public Person CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the playlist tracks.
        /// </summary>
        public List<PlaylistTrack> Tracks { get; set; }

        /// <summary>
        /// Gets or sets the playlist artwork.
        /// </summary>
        public List<PlaylistArtwork> Artwork { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        public override string ToString()
        {
            return Name;
        }
    }
}
