using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using Bloom.Domain.Enums;
using Microsoft.Practices.Prism.Mvvm;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents an album.
    /// </summary>
    [Table(Name = "album")]
    public class Album : BindableBase
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
        /// Gets or sets the artist identifier.
        /// </summary>
        [Column(Name = "artist_id")]
        public Guid? ArtistId { get; set; }

        /// <summary>
        /// Gets or sets the artist.
        /// </summary>
        public Artist Artist
        {
            get { return _artist; }
            set
            {
                _artist = value;
                ArtistId = _artist == null ? (Guid?) null : _artist.Id;
            }
        }
        private Artist _artist;

        /// <summary>
        /// Gets or sets the album name.
        /// </summary>
        [Column(Name = "name")]
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        private string _name;

        /// <summary>
        /// Gets or sets an unofficial name for the album (e.g. The White Album).
        /// </summary>
        [Column(Name = "unofficial_name")]
        public string UnofficialName
        {
            get { return _unofficialName; }
            set { SetProperty(ref _unofficialName, value); }
        }
        private string _unofficialName;

        /// <summary>
        /// Gets or sets the album edition.
        /// </summary>
        [Column(Name = "edition")]
        public string Edition
        {
            get { return _edition; }
            set { SetProperty(ref _edition, value); }
        }
        private string _edition;

        /// <summary>
        /// Gets or sets the album length type.
        /// </summary>
        [Column(Name = "length_type")]
        public LengthType LengthType
        {
            get { return _lengthType; }
            set { SetProperty(ref _lengthType, value); }
        }
        private LengthType _lengthType;

        /// <summary>
        /// Gets or sets the album length in milliseconds.
        /// </summary>
        [Column(Name = "length")]
        public int Length
        {
            get { return _length; }
            set { SetProperty(ref _length, value); }
        }
        private int _length;

        /// <summary>
        /// Gets or sets the album description.
        /// </summary>
        [Column(Name = "description")]
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }
        private string _description;

        /// <summary>
        /// Gets or sets the date the album was first released on.
        /// </summary>
        [Column(Name = "first_released_on")]
        public DateTime? FirstReleasedOn
        {
            get { return _firstReleasedOn; }
            set { SetProperty(ref _firstReleasedOn, value); }
        }
        private DateTime? _firstReleasedOn;

        /// <summary>
        /// Gets or sets the album liner notes.
        /// </summary>
        [Column(Name = "liner_notes")]
        public string LinerNotes
        {
            get { return _linerNotes; }
            set { SetProperty(ref _linerNotes, value); }
        }
        private string _linerNotes;

        /// <summary>
        /// Gets or sets whether this album is live.
        /// </summary>
        [Column(Name = "is_live")]
        public bool IsLive
        {
            get { return _isLive; }
            set { SetProperty(ref _isLive, value); }
        }
        private bool _isLive;

        /// <summary>
        /// Gets or sets whether this is a remix album.
        /// </summary>
        [Column(Name = "is_remix")]
        public bool IsRemix
        {
            get { return _isRemix; }
            set { SetProperty(ref _isRemix, value); }
        }
        private bool _isRemix;

        /// <summary>
        /// Gets or sets the identifier of the original album if this is a live version or remix album.
        /// </summary>
        [Column(Name = "original_album_id")]
        public Guid? OriginalAlbumId { get; set; }

        /// <summary>
        /// Gets or sets whether this is a tribute album.
        /// </summary>
        [Column(Name = "is_tribute")]
        public bool IsTribute
        {
            get { return _isTribute; }
            set { SetProperty(ref _isTribute, value); }
        }
        private bool _isTribute;

        /// <summary>
        /// Gets or sets the identifier of the artist this album is a tribute to.
        /// </summary>
        [Column(Name = "tribute_artist_id")]
        public Guid? TributeArtistId 
        { 
            get { return _tributeArtistId; }
            set
            {
                _tributeArtistId = value;
                IsTribute = _tributeArtistId != null;
            } 
        }
        private Guid? _tributeArtistId;

        /// <summary>
        /// Gets or sets whether this album is a soundtrack.
        /// </summary>
        [Column(Name = "is_soundtrack")]
        public bool IsSoundtrack
        {
            get { return _isSoundtrack; }
            set { SetProperty(ref _isSoundtrack, value); }
        }
        private bool _isSoundtrack;

        /// <summary>
        /// Gets or sets whether this album is for a holiday.
        /// </summary>
        [Column(Name = "is_holiday")]
        public bool IsHoliday
        {
            get { return _isHoliday; }
            set { SetProperty(ref _isHoliday, value); }
        }
        private bool _isHoliday;

        /// <summary>
        /// Gets or sets identifier of the holiday this album is for.
        /// </summary>
        [Column(Name = "holiday_id")]
        public Guid? HolidayId { get; set; }

        /// <summary>
        /// Gets or sets the holiday this album is for.
        /// </summary>
        public Holiday Holiday
        {
            get { return _holiday; }
            set
            {
                _holiday = value;
                HolidayId = _holiday == null ? (Guid?) null : _holiday.Id;
                IsHoliday = _holiday != null;
            }
        }
        private Holiday _holiday;

        /// <summary>
        /// Gets or sets whether this album is a bootleg.
        /// </summary>
        [Column(Name = "is_bootleg")]
        public bool IsBootleg
        {
            get { return _isBootleg; }
            set { SetProperty(ref _isBootleg, value); }
        }
        private bool _isBootleg;

        /// <summary>
        /// Gets or sets whether this album is promotional.
        /// </summary>
        [Column(Name = "is_promotional")]
        public bool IsPromotional
        {
            get { return _isPromotional; }
            set { SetProperty(ref _isPromotional, value); }
        }
        private bool _isPromotional;

        /// <summary>
        /// Gets or sets whether this album is a compilation.
        /// </summary>
        [Column(Name = "is_compilation")]
        public bool IsCompilation
        {
            get { return _isCompilation; }
            set { SetProperty(ref _isCompilation, value); }
        }
        private bool _isCompilation;

        /// <summary>
        /// Gets or sets whether this album contains mixed artists.
        /// </summary>
        [Column(Name = "is_mixed_artist")]
        public bool IsMixedArtist
        {
            get { return _isMixedArtist; }
            set { SetProperty(ref _isMixedArtist, value); }
        }
        private bool _isMixedArtist;

        /// <summary>
        /// Gets or sets whether this album is a single track.
        /// </summary>
        [Column(Name = "is_single_track")]
        public bool IsSingleTrack
        {
            get { return _isSingleTrack; }
            set { SetProperty(ref _isSingleTrack, value); }
        }
        private bool _isSingleTrack;

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

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        public override string ToString()
        {
            return Artist == null ? Name : Artist.Name + ": " + Name;
        }
    }
}
