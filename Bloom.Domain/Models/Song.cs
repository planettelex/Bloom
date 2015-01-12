using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;

namespace Bloom.Domain.Models
{
    public class Song
    {
        public Guid Id { get; set; }

        public Guid ArtistId { get; set; }

        public Artist Artist { get; set; }

        public string Name { get; set; }

        public string Version { get; set; }

        public int Length { get; set; }

        public Guid GenreId { get; set; }

        public Genre Genre { get; set; }

        public double? Bpm { get; set; }

        public MusicalKeys? Key { get; set; }

        public TimeSignature TimeSignature { get; set; }

        public string Description { get; set; }

        public string Lyrics { get; set; }

        public bool IsLive { get; set; }

        public bool IsCover { get; set; }

        public bool IsRemix { get; set; }

        public Guid OriginalSongId { get; set; }

        public Song OriginalSong { get; set; }

        public bool IsHoliday { get; set; }

        public Guid HolidayId { get; set; }

        public Holiday Holiday { get; set; }

        public bool HasExplicitContent { get; set; }

        public DayOfWeek? AboutDayOfWeek { get; set; }

        public TimeOfYear? AboutTimeOfYear { get; set; }

        public int? BestPlayedAtStart { get; set; }

        public int? BestPlayedAtStop { get; set; }

        public List<SongSegment> Segments { get; set; }

        public List<RecordingSession> RecordingSessions { get; set; }

        public List<SongCollaborator> Collaborators { get; set; }

        public List<SongCredit> Credits { get; set; }

        public List<SongReference> References { get; set; } 
    }
}
