using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents an album, a collection of songs.
    /// </summary>
    public class Album
    {
        /// <summary>
        /// Gets or sets the album identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the album artist identifier.
        /// </summary>
        public Guid ArtistId { get; set; }

        /// <summary>
        /// Gets or sets the album artist.
        /// </summary>
        public Artist Artist { get; set; }

        /// <summary>
        /// Gets or sets the album name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the album edition.
        /// </summary>
        public string Edition { get; set; }

        /// <summary>
        /// Gets or sets the album length type.
        /// </summary>
        public LengthType LengthType { get; set; }

        /// <summary>
        /// Gets or sets the album length, in milliseconds.
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// Gets or sets the album description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the album liner notes.
        /// </summary>
        public string LinerNotes { get; set; }

        /// <summary>
        /// Gets or sets whether this album is live.
        /// </summary>
        public bool IsLive { get; set; }

        /// <summary>
        /// Gets or sets whether this is a remix album.
        /// </summary>
        public bool IsRemix { get; set; }

        /// <summary>
        /// Gets or sets whether this is a tribute album.
        /// </summary>
        public bool IsTribute { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the artist this album is a tribute to.
        /// </summary>
        public Guid TributeArtistId { get; set; }

        /// <summary>
        /// Gets or sets the artist this album is a tribute to.
        /// </summary>
        public Artist TributeArtist { get; set; }

        /// <summary>
        /// Gets or sets whether this album is a soundtrack.
        /// </summary>
        public bool IsSoundtrack { get; set; }

        /// <summary>
        /// Gets or sets whether this album is for a holiday.
        /// </summary>
        public bool IsHoliday { get; set; }

        /// <summary>
        /// Gets or sets identifier of the holiday this album is for.
        /// </summary>
        public Guid HolidayId { get; set; }

        /// <summary>
        /// Gets or sets the holiday this album is for.
        /// </summary>
        public Holiday Holiday { get; set; }

        /// <summary>
        /// Gets or sets whether this album is a bootleg.
        /// </summary>
        public bool IsBootleg { get; set; }

        /// <summary>
        /// Gets or sets whether this album is promotional.
        /// </summary>
        public bool IsPromotional { get; set; }

        /// <summary>
        /// Gets or sets whether this album is a compilation.
        /// </summary>
        public bool IsCompilation { get; set; }

        /// <summary>
        /// Gets or sets whether this album contains mixed artists.
        /// </summary>
        public bool IsMixedArtist { get; set; }

        /// <summary>
        /// Gets or sets whether this album is a single track.
        /// </summary>
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
        /// Gets or sets the album releases.
        /// </summary>
        public List<AlbumRelease> Releases { get; set; }

        /// <summary>
        /// Gets or sets the album artist collaborators.
        /// </summary>
        public List<AlbumCollaborator> Collaborators { get; set; }

        /// <summary>
        /// Gets or sets the album references.
        /// </summary>
        public List<AlbumReference> References { get; set; } 

    }
}
