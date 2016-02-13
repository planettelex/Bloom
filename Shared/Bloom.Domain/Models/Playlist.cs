using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a music playlist.
    /// </summary>
    [Table(Name = "playlist")]
    public class Playlist
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
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the playlist description.
        /// </summary>
        [Column(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the playlist length, in milliseconds.
        /// </summary>
        [Column(Name = "length")]
        public int Length { get; set; }

        /// <summary>
        /// Gets or sets the date this playlist was created on.
        /// </summary>
        [Column(Name = "created_on")]
        public DateTime CreatedOn { get; set; }

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

        #region AddTrack

        /// <summary>
        /// Creates and adds a track to this playlist.
        /// </summary>
        /// <param name="song">The song.</param>
        /// <param name="trackNumber">The track number.</param>
        /// <returns>A new playlist track.</returns>
        /// <exception cref="System.ArgumentNullException">song</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">trackNumber</exception>
        public PlaylistTrack AddTrack(Song song, int trackNumber)
        {
            if (song == null)
                throw new ArgumentNullException("song");

            if (trackNumber < 1)
                throw new ArgumentOutOfRangeException("trackNumber");

            if (Tracks == null)
                Tracks = new List<PlaylistTrack>();

            var playlistTrack = PlaylistTrack.Create(this, song, trackNumber);
            Tracks.Add(playlistTrack);

            return playlistTrack;
        }

        #endregion

        /// <summary>
        /// Gets or sets the playlist artwork.
        /// </summary>
        public List<PlaylistArtwork> Artwork { get; set; }

        #region AddArtwork

        /// <summary>
        /// Creates and adds artwork to this playlist.
        /// </summary>
        /// <param name="url">The playlist URL.</param>
        /// <returns>A new playlist artwork.</returns>
        /// <exception cref="System.ArgumentNullException">url</exception>
        public PlaylistArtwork AddArtwork(string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException("url");

            if (Artwork == null)
                Artwork = new List<PlaylistArtwork>();

            var highestPriority = Artwork.Any() ? Artwork.Max(art => art.Priority) : 0;
            var nextPriority = highestPriority + 1;
            var playlistArtwork = PlaylistArtwork.Create(this, url, nextPriority);
            Artwork.Add(playlistArtwork);

            return playlistArtwork;
        }

        #endregion
    }
}
