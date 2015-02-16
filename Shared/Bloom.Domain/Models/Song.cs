using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using Bloom.Domain.Enums;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a song.
    /// </summary>
    [Table(Name = "song")]
    public class Song
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [Column(Name = "id", IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the song's artist identifier.
        /// </summary>
        [Column(Name = "artist_id")]
        public Guid ArtistId { get; set; }

        /// <summary>
        /// Gets or sets the song's artist.
        /// </summary>
        public Artist Artist { get; set; }

        /// <summary>
        /// Gets or sets the song name.
        /// </summary>
        [Column(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the song version.
        /// </summary>
        [Column(Name = "version")]
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the song length, in milliseconds.
        /// </summary>
        [Column(Name = "length")]
        public int Length { get; set; }

        /// <summary>
        /// Gets or sets the song genre identifier.
        /// </summary>
        [Column(Name = "genre_id")]
        public Guid GenreId { get; set; }

        /// <summary>
        /// Gets or sets the song genre.
        /// </summary>
        public Genre Genre { get; set; }

        /// <summary>
        /// Gets or sets the song's beats per minute.
        /// </summary>
        [Column(Name = "bpm")]
        public double? Bpm { get; set; }

        /// <summary>
        /// Gets or sets the song's musical key.
        /// </summary>
        [Column(Name = "key")]
        public MusicalKeys? Key { get; set; }

        /// <summary>
        /// Gets or sets the time signature identifier.
        /// </summary>
        [Column(Name = "time_signature_id")]
        public Guid TimeSignatureId { get; set; }

        /// <summary>
        /// Gets or sets the song's time signature.
        /// </summary>
        public TimeSignature TimeSignature { get; set; }

        /// <summary>
        /// Gets or sets song description.
        /// </summary>
        [Column(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the song's lyrics.
        /// </summary>
        [Column(Name = "lyrics")]
        public string Lyrics { get; set; }

        /// <summary>
        /// Gets or sets whether this song is live.
        /// </summary>
        [Column(Name = "is_live")]
        public bool IsLive { get; set; }

        /// <summary>
        /// Gets or sets whether this song is a cover.
        /// </summary>
        [Column(Name = "is_cover")]
        public bool IsCover { get; set; }

        /// <summary>
        /// Gets or sets whether this song is a remix.
        /// </summary>
        [Column(Name = "is_remix")]
        public bool IsRemix { get; set; }

        /// <summary>
        /// Gets or sets the original song identifier.
        /// </summary>
        [Column(Name = "original_song_id")]
        public Guid OriginalSongId { get; set; }

        /// <summary>
        /// Gets or sets the original song.
        /// </summary>
        public Song OriginalSong { get; set; }

        /// <summary>
        /// Gets or sets whether this song is for a holiday.
        /// </summary>
        [Column(Name = "is_holiday")]
        public bool IsHoliday { get; set; }

        /// <summary>
        /// Gets or sets the holiday identifier.
        /// </summary>
        [Column(Name = "holiday_id")]
        public Guid HolidayId { get; set; }

        /// <summary>
        /// Gets or sets the holiday.
        /// </summary>
        public Holiday Holiday { get; set; }

        /// <summary>
        /// Gets or sets whether this song has explicit content.
        /// </summary>
        [Column(Name = "has_explicit_content")]
        public bool HasExplicitContent { get; set; }

        /// <summary>
        /// Gets or sets the day of week this song is about.
        /// </summary>
        [Column(Name = "about_day_of_week")]
        public DayOfWeek? AboutDayOfWeek { get; set; }

        /// <summary>
        /// Gets or sets the time of year this song is about.
        /// </summary>
        [Column(Name = "about_time_of_year")]
        public TimeOfYear? AboutTimeOfYear { get; set; }

        /// <summary>
        /// Gets or sets the starting time this song is best played at, measured in seconds past midnight.
        /// </summary>
        [Column(Name = "best_played_at_start")]
        public int? BestPlayedAtStart { get; set; }

        /// <summary>
        /// Gets or sets the stopping time this song is best played at, measured in seconds past midnight.
        /// </summary>
        [Column(Name = "best_played_at_stop")]
        public int? BestPlayedAtStop { get; set; }

        /// <summary>
        /// Gets or sets the song's segments.
        /// </summary>
        public List<SongSegment> Segments { get; set; }

        /// <summary>
        /// Gets or sets the recording sessions for the song.
        /// </summary>
        public List<RecordingSession> RecordingSessions { get; set; }

        /// <summary>
        /// Gets or sets the collaborators.
        /// </summary>
        public List<SongCollaborator> Collaborators { get; set; }

        /// <summary>
        /// Gets or sets the song's credits.
        /// </summary>
        public List<SongCredit> Credits { get; set; }

        /// <summary>
        /// Gets or sets the song's references.
        /// </summary>
        public List<SongReference> References { get; set; }

        /// <summary>
        /// Gets or sets the album tracks ths song belongs to.
        /// </summary>
        public List<AlbumTrack> AlbumTracks { get; set; } 
    }
}
