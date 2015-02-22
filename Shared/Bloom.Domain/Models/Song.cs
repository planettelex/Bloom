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
        /// Creates a new song instance.
        /// </summary>
        /// <param name="name">The song name.</param>
        /// <param name="artist">The song artist.</param>
        public static Song Create(string name, Artist artist)
        {
            return new Song
            {
                Id = Guid.NewGuid(),
                Name = name,
                ArtistId = artist.Id,
                Artist = artist
            };
        }

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

        #region AddSegment

        /// <summary>
        /// Creates and adds a segment to this song.
        /// </summary>
        /// <param name="startTime">The segment start time.</param>
        /// <param name="stopTime">The segment stop time.</param>
        /// <returns>A new song segment.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// startTime
        /// or
        /// stopTime
        /// </exception>
        public SongSegment AddSegment(int startTime, int stopTime)
        {
            if (startTime < 0 || startTime > Length)
                throw new ArgumentOutOfRangeException("startTime");

            if (stopTime < 0 || stopTime > Length || stopTime < startTime)
                throw new ArgumentOutOfRangeException("stopTime");

            if (Segments == null)
                Segments = new List<SongSegment>();

            var songSegment = SongSegment.Create(this, startTime, stopTime);
            Segments.Add(songSegment);

            return songSegment;
        }

        #endregion

        /// <summary>
        /// Gets or sets the recording sessions for the song.
        /// </summary>
        public List<RecordingSession> RecordingSessions { get; set; }

        #region AddRecordingSession

        /// <summary>
        /// Creates and adds a recording session to this song.
        /// </summary>
        /// <param name="occurredOn">The date the recording session occurred on.</param>
        /// <returns>A new recording session.</returns>
        public RecordingSession AddRecordingSession(DateTime occurredOn)
        {
            if (RecordingSessions == null)
                RecordingSessions = new List<RecordingSession>();

            var recordingSession = RecordingSession.Create(this, occurredOn);
            RecordingSessions.Add(recordingSession);

            return recordingSession;
        }

        #endregion

        /// <summary>
        /// Gets or sets the collaborators.
        /// </summary>
        public List<SongCollaborator> Collaborators { get; set; }

        #region AddCollaborator

        /// <summary>
        /// Creates and adds an artist collaborator to this song.
        /// </summary>
        /// <param name="artist">The artist.</param>
        /// <returns>A new song collaborator.</returns>
        /// <exception cref="System.ArgumentNullException">artist</exception>
        public SongCollaborator AddCollaborator(Artist artist)
        {
            if (artist == null)
                throw new ArgumentNullException("artist");

            if (Collaborators == null)
                Collaborators = new List<SongCollaborator>();

            var songCollaborator = SongCollaborator.Create(this, artist);
            Collaborators.Add(songCollaborator);

            return songCollaborator;
        }

        #endregion

        /// <summary>
        /// Gets or sets the song's credits.
        /// </summary>
        public List<SongCredit> Credits { get; set; }

        #region AddCredit

        /// <summary>
        /// Creates and adds a credit for this song.
        /// </summary>
        /// <param name="person">The person to credit.</param>
        /// <returns>A new song credit.</returns>
        /// <exception cref="System.ArgumentNullException">person</exception>
        public SongCredit AddCredit(Person person)
        {
            if (person == null)
                throw new ArgumentNullException("person");

            if (Credits == null)
                Credits = new List<SongCredit>();

            var songCredit = SongCredit.Create(this, person);
            Credits.Add(songCredit);

            return songCredit;
        }

        /// <summary>
        /// Creates and adds a credit for this song.
        /// </summary>
        /// <param name="person">The person to credit.</param>
        /// <param name="roles">The person's roles with this song credit.</param>
        /// <returns>A new song credit.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// person
        /// or
        /// roles
        /// </exception>
        public SongCredit AddCredit(Person person, IList<Role> roles)
        {
            if (person == null)
                throw new ArgumentNullException("person");

            if (roles == null)
                throw new ArgumentNullException("roles");

            if (Credits == null)
                Credits = new List<SongCredit>();

            var songCredit = SongCredit.Create(this, person);
            foreach (var role in roles)
                songCredit.AddRole(role);

            Credits.Add(songCredit);

            return songCredit;
        }

        #endregion

        /// <summary>
        /// Gets or sets the song's references.
        /// </summary>
        public List<SongReference> References { get; set; }

        #region AddReference

        /// <summary>
        /// Creates and adds a reference to this song.
        /// </summary>
        /// <param name="reference">The reference.</param>
        /// <returns>A new song reference.</returns>
        /// <exception cref="System.ArgumentNullException">reference</exception>
        public SongReference AddReference(Reference reference)
        {
            if (reference == null)
                throw new ArgumentNullException("reference");

            if (References == null)
                References = new List<SongReference>();

            var songReference = SongReference.Create(this, reference);
            References.Add(songReference);

            return songReference;
        }

        #endregion

        /// <summary>
        /// Gets or sets the song reviews.
        /// </summary>
        public List<SongReview> Reviews { get; set; }

        #region AddReview

        /// <summary>
        /// Creates and adds a review of this song.
        /// </summary>
        /// <param name="review">The review.</param>
        /// <returns>A new song review.</returns>
        /// <exception cref="System.ArgumentNullException">review</exception>
        public SongReview AddReview(Review review)
        {
            if (review == null)
                throw new ArgumentNullException("review");

            if (Reviews == null)
                Reviews = new List<SongReview>();

            var songReview = SongReview.Create(this, review);
            Reviews.Add(songReview);

            return songReview;
        }

        #endregion

        /// <summary>
        /// Gets or sets the song activities.
        /// </summary>
        public List<SongActivity> Activities { get; set; }

        #region AddActivity

        /// <summary>
        /// Creates and adds an activity for this song.
        /// </summary>
        /// <param name="activity">The activity.</param>
        /// <returns>A new song activity.</returns>
        /// <exception cref="System.ArgumentNullException">activity</exception>
        public SongActivity AddActivity(Activity activity)
        {
            if (activity == null)
                throw new ArgumentNullException("activity");

            if (Activities == null)
                Activities = new List<SongActivity>();

            var albumActivity = SongActivity.Create(this, activity);
            Activities.Add(albumActivity);

            return albumActivity;
        }

        #endregion

        /// <summary>
        /// Gets or sets the song moods.
        /// </summary>
        public List<SongMood> Moods { get; set; }

        #region AddMood

        /// <summary>
        /// Creates and adds a mood for this song.
        /// </summary>
        /// <param name="mood">The mood.</param>
        /// <returns>A new song mood.</returns>
        /// <exception cref="System.ArgumentNullException">mood</exception>
        public SongMood AddMood(Mood mood)
        {
            if (mood == null)
                throw new ArgumentNullException("mood");

            if (Moods == null)
                Moods = new List<SongMood>();

            var songMood = SongMood.Create(this, mood);
            Moods.Add(songMood);

            return songMood;
        }

        #endregion

        /// <summary>
        /// Gets or sets the song tags.
        /// </summary>
        public List<SongTag> Tags { get; set; }

        #region AddTag

        /// <summary>
        /// Creates and adds a tag for this song.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <returns>A new song tag.</returns>
        /// <exception cref="System.ArgumentNullException">tag</exception>
        public SongTag AddTag(Tag tag)
        {
            if (tag == null)
                throw new ArgumentNullException("tag");

            if (Tags == null)
                Tags = new List<SongTag>();

            var songTag = SongTag.Create(this, tag);
            Tags.Add(songTag);

            return songTag;
        }

        #endregion

        /// <summary>
        /// Gets or sets the album tracks ths song belongs to.
        /// </summary>
        public List<AlbumTrack> AlbumTracks { get; set; }
    }
}
