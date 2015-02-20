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
        /// Gets or sets the artist this album is a tribute to.
        /// </summary>
        public Artist TributeArtist { get; set; }

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

        #region AddTrack

        /// <summary>
        /// Creates and adds a track to this album.
        /// </summary>
        /// <param name="song">The song.</param>
        /// <param name="trackNumber">The track number.</param>
        /// <returns>A new album track.</returns>
        /// <exception cref="System.ArgumentNullException">song</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">trackNumber</exception>
        public AlbumTrack AddTrack(Song song, int trackNumber)
        {
            if (song == null)
                throw new ArgumentNullException("song");

            if (trackNumber < 1)
                throw new ArgumentOutOfRangeException("trackNumber");

            if (Tracks == null)
                Tracks = new List<AlbumTrack>();

            var albumTrack = AlbumTrack.Create(this, song, trackNumber);
            Tracks.Add(albumTrack);

            return albumTrack;
        }

        /// <summary>
        /// Creates and adds a track to this album.
        /// </summary>
        /// <param name="song">The song.</param>
        /// <param name="trackNumber">The track number.</param>
        /// <param name="discNumber">The disc number.</param>
        /// <returns>A new album track.</returns>
        /// <exception cref="System.ArgumentNullException">song</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// trackNumber
        /// or
        /// discNumber
        /// </exception>
        public AlbumTrack AddTrack(Song song, int trackNumber, int discNumber)
        {
            if (song == null)
                throw new ArgumentNullException("song");

            if (trackNumber < 1)
                throw new ArgumentOutOfRangeException("trackNumber");

            if (discNumber < 1)
                throw new ArgumentOutOfRangeException("discNumber");

            if (Tracks == null)
                Tracks = new List<AlbumTrack>();

            var albumTrack = AlbumTrack.Create(this, song, trackNumber, discNumber);
            Tracks.Add(albumTrack);

            return albumTrack;
        }

        #endregion

        /// <summary>
        /// Gets or sets the album artwork.
        /// </summary>
        public List<AlbumArtwork> Artwork { get; set; }

        #region AddArtwork

        /// <summary>
        /// Creates and adds artwork to this album.
        /// </summary>
        /// <param name="url">The artwork URL.</param>
        /// <returns>A new album artwork.</returns>
        /// <exception cref="System.ArgumentNullException">url</exception>
        public AlbumArtwork AddArtwork(string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException("url");

            if (Artwork == null)
                Artwork = new List<AlbumArtwork>();

            var highestPriority = Artwork.Any() ? Artwork.Max(art => art.Priority) : 0;
            var nextPriority = highestPriority + 1;
            var albumArtwork = AlbumArtwork.Create(this, url, nextPriority);
            Artwork.Add(albumArtwork);

            return albumArtwork;
        }

        #endregion

        /// <summary>
        /// Gets or sets the album credits.
        /// </summary>
        public List<AlbumCredit> Credits { get; set; }

        #region AddCredit

        /// <summary>
        /// Creates and adds a credit for this album.
        /// </summary>
        /// <param name="person">The person to credit.</param>
        /// <returns>A new album credit.</returns>
        /// <exception cref="System.ArgumentNullException">person</exception>
        public AlbumCredit AddCredit(Person person)
        {
            if (person == null)
                throw new ArgumentNullException("person");

            if (Credits == null)
                Credits = new List<AlbumCredit>();

            var albumCredit = AlbumCredit.Create(this, person);
            Credits.Add(albumCredit);

            return albumCredit;
        }

        /// <summary>
        /// Creates and adds a credit for this album.
        /// </summary>
        /// <param name="person">The person to credit.</param>
        /// <param name="roles">The person's roles with this album credit.</param>
        /// <returns>A new album credit.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// person
        /// or
        /// roles
        /// </exception>
        public AlbumCredit AddCredit(Person person, IList<Role> roles)
        {
            if (person == null)
                throw new ArgumentNullException("person");

            if (roles == null)
                throw new ArgumentNullException("roles");

            if (Credits == null)
                Credits = new List<AlbumCredit>();

            var albumCredit = AlbumCredit.Create(this, person);
            foreach (var role in roles)
                albumCredit.AddRole(role);

            Credits.Add(albumCredit);

            return albumCredit;
        }

        #endregion

        /// <summary>
        /// Gets or sets the album releases.
        /// </summary>
        public List<AlbumRelease> Releases { get; set; }

        #region AddRelease

        /// <summary>
        /// Creates and adds a release of this album.
        /// </summary>
        /// <param name="releaseDate">The release date.</param>
        /// <returns>A new album release.</returns>
        public AlbumRelease AddRelease(DateTime releaseDate)
        {
            if (Releases == null)
                Releases = new List<AlbumRelease>();

            var albumRelease = AlbumRelease.Create(this, releaseDate);
            Releases.Add(albumRelease);

            return albumRelease;
        }

        /// <summary>
        /// Creates and adds a release of this album.
        /// </summary>
        /// <param name="releaseDate">The release date.</param>
        /// <param name="mediaTypes">The media types.</param>
        /// <returns>A new album release.</returns>
        public AlbumRelease AddRelease(DateTime releaseDate, MediaTypes mediaTypes)
        {
            if (Releases == null)
                Releases = new List<AlbumRelease>();

            var albumRelease = AlbumRelease.Create(this, releaseDate, mediaTypes);
            Releases.Add(albumRelease);

            return albumRelease;
        }

        /// <summary>
        /// Creates and adds a release of this album.
        /// </summary>
        /// <param name="releaseDate">The release date.</param>
        /// <param name="mediaTypes">The media types.</param>
        /// <param name="digitalFormats">The digital formats.</param>
        /// <returns>A new album release.</returns>
        public AlbumRelease AddRelease(DateTime releaseDate, MediaTypes mediaTypes, DigitalFormats digitalFormats)
        {
            if (Releases == null)
                Releases = new List<AlbumRelease>();

            var albumRelease = AlbumRelease.Create(this, releaseDate, mediaTypes, digitalFormats);
            Releases.Add(albumRelease);

            return albumRelease;
        }

        /// <summary>
        /// Creates and adds a release of this album.
        /// </summary>
        /// <param name="label">The release label.</param>
        /// <param name="releaseDate">The release date.</param>
        /// <param name="mediaTypes">The media types.</param>
        /// <param name="digitalFormats">The digital formats.</param>
        /// <param name="catalogNumber">The catalog number.</param>
        /// <returns>A new album release.</returns>
        /// <exception cref="System.ArgumentNullException">label</exception>
        public AlbumRelease AddRelease(Label label, DateTime releaseDate, MediaTypes mediaTypes, DigitalFormats digitalFormats, string catalogNumber)
        {
            if (label == null)
                throw new ArgumentNullException("label");

            if (Releases == null)
                Releases = new List<AlbumRelease>();

            var albumRelease = AlbumRelease.Create(this, label, releaseDate, mediaTypes, digitalFormats, catalogNumber);
            Releases.Add(albumRelease);

            return albumRelease;
        }

        #endregion

        /// <summary>
        /// Gets or sets the album artist collaborators.
        /// </summary>
        public List<AlbumCollaborator> Collaborators { get; set; }

        #region AddCollaborator

        /// <summary>
        /// Creates and adds an artist collaborator to this album.
        /// </summary>
        /// <param name="artist">The artist.</param>
        /// <returns>A new album collaborator.</returns>
        /// <exception cref="System.ArgumentNullException">artist</exception>
        public AlbumCollaborator AddCollaborator(Artist artist)
        {
            if (artist == null)
                throw new ArgumentNullException("artist");

            if (Collaborators == null)
                Collaborators = new List<AlbumCollaborator>();

            var albumCollaborator = AlbumCollaborator.Create(this, artist);
            Collaborators.Add(albumCollaborator);

            return albumCollaborator;
        }

        #endregion

        /// <summary>
        /// Gets or sets the album references.
        /// </summary>
        public List<AlbumReference> References { get; set; }

        #region AddReference

        /// <summary>
        /// Creates and adds a reference to this album.
        /// </summary>
        /// <param name="reference">The reference.</param>
        /// <returns>A new album reference.</returns>
        /// <exception cref="System.ArgumentNullException">reference</exception>
        public AlbumReference AddReference(Reference reference)
        {
            if (reference == null)
                throw new ArgumentNullException("reference");

            if (References == null)
                References = new List<AlbumReference>();

            var albumReference = AlbumReference.Create(this, reference);
            References.Add(albumReference);

            return albumReference;
        }

        #endregion

        /// <summary>
        /// Gets or sets the album reviews.
        /// </summary>
        public List<AlbumReview> Reviews { get; set; }

        #region AddReview

        /// <summary>
        /// Creates and adds a review of this album.
        /// </summary>
        /// <param name="review">The review.</param>
        /// <returns>A new album review.</returns>
        /// <exception cref="System.ArgumentNullException">review</exception>
        public AlbumReview AddReview(Review review)
        {
            if (review == null)
                throw new ArgumentNullException("review");

            if (Reviews == null)
                Reviews = new List<AlbumReview>();

            var albumReview = AlbumReview.Create(this, review);
            Reviews.Add(albumReview);

            return albumReview;
        }

        #endregion
    }
}
