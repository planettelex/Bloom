using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using Bloom.Domain.Enums;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents an album, a collection of songs.
    /// </summary>
    [Table(Name = "album")]
    public class Album
    {
        /// <summary>
        /// Creates a new album instance.
        /// </summary>
        /// <param name="name">The album name.</param>
        public static Album Create(string name)
        {
            return new Album
            {
                Id = Guid.NewGuid(),
                Name = name
            };
        }

        /// <summary>
        /// Creates a new album instance.
        /// </summary>
        /// <param name="name">The album name.</param>
        /// <param name="artist">The album artist.</param>
        public static Album Create(string name, Artist artist)
        {
            return new Album
            {
                Id = Guid.NewGuid(),
                Name = name,
                ArtistId = artist.Id,
                Artist = artist
            };
        }
        
        /// <summary>
        /// Gets or sets the album identifier.
        /// </summary>
        [Column(Name = "id", IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the album artist identifier.
        /// </summary>
        [Column(Name = "artist_id")]
        public Guid ArtistId { get; set; }

        /// <summary>
        /// Gets or sets the album artist.
        /// </summary>
        public Artist Artist { get; set; }

        /// <summary>
        /// Gets or sets the album name.
        /// </summary>
        [Column(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the album edition.
        /// </summary>
        [Column(Name = "edition")]
        public string Edition { get; set; }

        /// <summary>
        /// Gets or sets the album length type.
        /// </summary>
        [Column(Name = "length_type")]
        public LengthType LengthType { get; set; }

        /// <summary>
        /// Gets or sets the album length in milliseconds.
        /// </summary>
        [Column(Name = "length")]
        public int Length { get; set; }

        /// <summary>
        /// Gets or sets the album description.
        /// </summary>
        [Column(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the album liner notes.
        /// </summary>
        [Column(Name = "liner_notes")]
        public string LinerNotes { get; set; }

        /// <summary>
        /// Gets or sets whether this album is live.
        /// </summary>
        [Column(Name = "is_live")]
        public bool IsLive { get; set; }

        /// <summary>
        /// Gets or sets whether this is a remix album.
        /// </summary>
        [Column(Name = "is_remix")]
        public bool IsRemix { get; set; }

        /// <summary>
        /// Gets or sets whether this is a tribute album.
        /// </summary>
        [Column(Name = "is_tribute")]
        public bool IsTribute { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the artist this album is a tribute to.
        /// </summary>
        [Column(Name = "tribute_artist_id")]
        public Guid TributeArtistId { get; set; }

        /// <summary>
        /// Gets or sets whether this album is a soundtrack.
        /// </summary>
        [Column(Name = "is_soundtrack")]
        public bool IsSoundtrack { get; set; }

        /// <summary>
        /// Gets or sets whether this album is for a holiday.
        /// </summary>
        [Column(Name = "is_holiday")]
        public bool IsHoliday { get; set; }

        /// <summary>
        /// Gets or sets identifier of the holiday this album is for.
        /// </summary>
        [Column(Name = "holiday_id")]
        public Guid HolidayId { get; set; }

        /// <summary>
        /// Gets or sets the holiday this album is for.
        /// </summary>
        public Holiday Holiday { get; set; }

        /// <summary>
        /// Gets or sets whether this album is a bootleg.
        /// </summary>
        [Column(Name = "is_bootleg")]
        public bool IsBootleg { get; set; }

        /// <summary>
        /// Gets or sets whether this album is promotional.
        /// </summary>
        [Column(Name = "is_promotional")]
        public bool IsPromotional { get; set; }

        /// <summary>
        /// Gets or sets whether this album is a compilation.
        /// </summary>
        [Column(Name = "is_compilation")]
        public bool IsCompilation { get; set; }

        /// <summary>
        /// Gets or sets whether this album contains mixed artists.
        /// </summary>
        [Column(Name = "is_mixed_artist")]
        public bool IsMixedArtist { get; set; }

        /// <summary>
        /// Gets or sets whether this album is a single track.
        /// </summary>
        [Column(Name = "is_single_track")]
        public bool IsSingleTrack { get; set; }

        /// <summary>
        /// Gets or sets the album tracks.
        /// </summary>
        public List<AlbumTrack> Tracks { get; set; }

        /// <summary>
        /// Gets or sets the album artwork. 
        /// </summary>
        public List<AlbumArtwork> Artwork { get; set; }

        /// <summary>
        /// Gets or sets the album credits.
        /// </summary>
        public List<AlbumCredit> Credits { get; set; }

        /// <summary>
        /// Gets or sets the album artist collaborators.
        /// </summary>
        public List<AlbumCollaborator> Collaborators { get; set; }
    }
}
