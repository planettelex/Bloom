using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a song.
    /// </summary>
    public class Song
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the song's artist identifier.
        /// </summary>
        public Guid ArtistId { get; set; }

        /// <summary>
        /// Gets or sets the song's artist.
        /// </summary>
        public Artist Artist { get; set; }

        /// <summary>
        /// Gets or sets the song name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the song version.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the song length, in millisecond.
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// Gets or sets the song genre identifier.
        /// </summary>
        public Guid GenreId { get; set; }

        /// <summary>
        /// Gets or sets the song genre.
        /// </summary>
        public Genre Genre { get; set; }

        /// <summary>
        /// Gets or sets the song's beats per minute.
        /// </summary>
        public double? Bpm { get; set; }

        /// <summary>
        /// Gets or sets the song's musical key.
        /// </summary>
        public MusicalKeys? Key { get; set; }

        /// <summary>
        /// Gets or sets the song's time signature.
        /// </summary>
        public TimeSignature TimeSignature { get; set; }

        /// <summary>
        /// Gets or sets song description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the song's lyrics.
        /// </summary>
        public string Lyrics { get; set; }

        /// <summary>
        /// Gets or sets whether this song is live.
        /// </summary>
        public bool IsLive { get; set; }

        /// <summary>
        /// Gets or sets whether this song is a cover.
        /// </summary>
        public bool IsCover { get; set; }

        /// <summary>
        /// Gets or sets whether this song is a remix.
        /// </summary>
        public bool IsRemix { get; set; }

        /// <summary>
        /// Gets or sets the original song identifier.
        /// </summary>
        public Guid OriginalSongId { get; set; }

        /// <summary>
        /// Gets or sets the original song.
        /// </summary>
        public Song OriginalSong { get; set; }

        /// <summary>
        /// Gets or sets whether this song is for a holiday.
        /// </summary>
        public bool IsHoliday { get; set; }

        /// <summary>
        /// Gets or sets the holiday identifier.
        /// </summary>
        public Guid HolidayId { get; set; }

        /// <summary>
        /// Gets or sets the holiday.
        /// </summary>
        public Holiday Holiday { get; set; }

        /// <summary>
        /// Gets or sets whether this song has explicit content.
        /// </summary>
        public bool HasExplicitContent { get; set; }

        /// <summary>
        /// Gets or sets the day of week this song is about.
        /// </summary>
        public DayOfWeek? AboutDayOfWeek { get; set; }

        /// <summary>
        /// Gets or sets the time of year this song is about.
        /// </summary>
        public TimeOfYear? AboutTimeOfYear { get; set; }

        /// <summary>
        /// Gets or sets the starting time this song is best played at.
        /// </summary>
        public int? BestPlayedAtStart { get; set; }

        /// <summary>
        /// Gets or sets the stopping time this song is best played at.
        /// </summary>
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
