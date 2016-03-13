using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    /// <summary>
    /// Access methods for song data.
    /// </summary>
    public class SongRepository : ISongRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SongRepository"/> class.
        /// </summary>
        /// <param name="roleRepository">The role repository.</param>
        /// <param name="personRepository">The person repository.</param>
        public SongRepository(IRoleRepository roleRepository, IPersonRepository personRepository)
        {
            _roleRepository = roleRepository;
            _personRepository = personRepository;
        }
        private readonly IRoleRepository _roleRepository;
        private readonly IPersonRepository _personRepository;

        /// <summary>
        /// Gets the song.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="songId">The song identifier.</param>
        public Song GetSong(IDataSource dataSource, Guid songId)
        {
            if (!dataSource.IsConnected())
                return null;

            var genreTable = GenreTable(dataSource);
            var holidayTable = HolidayTable(dataSource);
            var artistTable = ArtistTable(dataSource);
            var timeSignatureTable = TimeSignatureTable(dataSource);
            var songTable = SongTable(dataSource);
            if (songTable == null)
                return null;

            var songQuery =
                from s in songTable
                from artist in artistTable.Where(a => s.ArtistId == a.Id).DefaultIfEmpty()
                from genre in genreTable.Where(g => s.GenreId == g.Id).DefaultIfEmpty()
                from holiday in holidayTable.Where(h => s.HolidayId == h.Id).DefaultIfEmpty()
                from timeSignature in timeSignatureTable.Where(t => s.TimeSignatureId == t.Id).DefaultIfEmpty()
                where s.Id == songId
                select new
                {
                    Song = s,
                    Artist = artist,
                    Genre = genre,
                    Holiday = holiday,
                    TimeSignature = timeSignature
                };

            var result = songQuery.SingleOrDefault();

            if (result == null)
                return null;

            var song = result.Song;
            if (song == null)
                return null;

            song.Artist = result.Artist;
            song.Genre = result.Genre;
            song.Holiday = result.Holiday;
            song.TimeSignature = result.TimeSignature;

            var segmentsTable = SongSegmentTable(dataSource);
            var segmentsQuery =
                from segment in segmentsTable
                from timeSignature in timeSignatureTable.Where(t => segment.TimeSignatureId == t.Id).DefaultIfEmpty()
                where segment.SongId == songId
                orderby segment.StartTime
                select new
                {
                    Segment = segment,
                    TimeSignature = timeSignature
                };

            song.Segments = null;
            var segmentsResults = segmentsQuery.ToList();
            if (segmentsResults.Any())
            {
                song.Segments = new List<SongSegment>();
                foreach (var segmentResult in segmentsResults)
                {
                    var songSegment = segmentResult.Segment;
                    songSegment.TimeSignature = segmentResult.TimeSignature;
                    song.Segments.Add(songSegment);
                }
            }

            var collaboratorsTable = SongCollaboratorTable(dataSource);
            var collaboratorsQuery =
                from collaborator in collaboratorsTable
                join artist in artistTable on collaborator.ArtistId equals artist.Id
                where collaborator.SongId == songId
                orderby collaborator.IsFeatured descending, artist.Name
                select new 
                {
                    Collaborator = collaborator,
                    Artist = artist
                };

            song.Collaborators = null;
            var collaboratorsResults = collaboratorsQuery.ToList();
            if (collaboratorsResults.Any())
            {
                song.Collaborators = new List<SongCollaborator>();
                foreach (var collaboratorResult in collaboratorsResults)
                {
                    var songCollaborator = collaboratorResult.Collaborator;
                    songCollaborator.Artist = collaboratorResult.Artist;
                    song.Collaborators.Add(songCollaborator);
                }
            }

            var personTable = PersonTable(dataSource);
            var creditsTable = SongCreditTable(dataSource);
            var creditsQuery =
                from songCredit in creditsTable
                join person in personTable on songCredit.PersonId equals person.Id
                where songCredit.SongId == songId
                orderby person.Name
                select new
                {
                    Credit = songCredit,
                    Person = person
                };

            song.Credits = null;
            var creditsResults = creditsQuery.ToList();
            if (creditsResults.Any())
            {
                song.Credits = new List<SongCredit>();
                foreach (var creditResult in creditsResults)
                {
                    var songCredit = creditResult.Credit;
                    songCredit.Person = creditResult.Person;
                    song.Credits.Add(songCredit);
                }
            }

            if (song.Credits == null)
                return song;

            var songCreditRoleTable = SongCreditRoleTable(dataSource);
            var roleTable = RoleTable(dataSource);
            foreach (var credit in song.Credits)
            {
                var c = credit;
                var rolesQuery =
                    from scr in songCreditRoleTable
                    join role in roleTable on scr.RoleId equals role.Id
                    where scr.SongCreditId == c.Id
                    orderby role.Name
                    select role;

                credit.Roles = rolesQuery.ToList();
            }

            return song;
        }

        /// <summary>
        /// Gets the recording session.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="recordingSessionId">The recording session identifier.</param>
        public RecordingSession GetRecordingSession(IDataSource dataSource, Guid recordingSessionId)
        {
            if (!dataSource.IsConnected())
                return null;

            var recordingSessionTable = RecordingSessionTable(dataSource);
            if (recordingSessionTable == null)
                return null;

            var recordingSessionQuery =
                from rs in recordingSessionTable
                where rs.Id == recordingSessionId
                select rs;

            return recordingSessionQuery.SingleOrDefault();
        }

        /// <summary>
        /// Lists the songs.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        public List<Song> ListSongs(IDataSource dataSource)
        {
            if (!dataSource.IsConnected())
                return null;

            var genreTable = GenreTable(dataSource);
            var holidayTable = HolidayTable(dataSource);
            var artistTable = ArtistTable(dataSource);
            var timeSignatureTable = TimeSignatureTable(dataSource);
            var songTable = SongTable(dataSource);
            if (songTable == null)
                return null;

            var songsQuery =
                from s in songTable
                from artist in artistTable.Where(a => s.ArtistId == a.Id).DefaultIfEmpty()
                from genre in genreTable.Where(g => s.GenreId == g.Id).DefaultIfEmpty()
                from holiday in holidayTable.Where(h => s.HolidayId == h.Id).DefaultIfEmpty()
                from timeSignature in timeSignatureTable.Where(t => s.TimeSignatureId == t.Id).DefaultIfEmpty()
                orderby artist.Name, s.Name, s.Version, s.IsLive, s.IsCover, s.IsRemix 
                select new
                {
                    Song = s,
                    Artist = artist,
                    Genre = genre,
                    Holiday = holiday,
                    TimeSignature = timeSignature
                };

            var results = songsQuery.ToList();

            if (!results.Any()) 
                return null;

            var songs = new List<Song>();
            foreach (var result in results)
            {
                var song = result.Song;
                song.Artist = result.Artist;
                song.Genre = result.Genre;
                song.Holiday = result.Holiday;
                song.TimeSignature = result.TimeSignature;
                songs.Add(song);
            }

            return songs;
        }

        /// <summary>
        /// Lists the song's recording sessions.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="songId">The song identifier.</param>
        public List<RecordingSession> ListRecordingSessions(IDataSource dataSource, Guid songId)
        {
            if (!dataSource.IsConnected())
                return null;

            var recordingSessionTable = RecordingSessionTable(dataSource);
            if (recordingSessionTable == null)
                return null;

            var recordingSessionsQuery =
                from rs in recordingSessionTable
                where rs.SongId == songId
                orderby rs.OccurredOn
                select rs;

            return recordingSessionsQuery.ToList();
        }

        /// <summary>
        /// Adds the song.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="song">The song.</param>
        public void AddSong(IDataSource dataSource, Song song)
        {
            if (!dataSource.IsConnected())
                return;

            var songTable = SongTable(dataSource);
            if (songTable == null)
                return;

            songTable.InsertOnSubmit(song);
            dataSource.Save();
        }

        /// <summary>
        /// Adds a song segment.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="songSegment">The song segment.</param>
        public void AddSongSegment(IDataSource dataSource, SongSegment songSegment)
        {
            if (!dataSource.IsConnected())
                return;

            var songSegmentTable = SongSegmentTable(dataSource);
            if (songSegmentTable == null)
                return;

            songSegmentTable.InsertOnSubmit(songSegment);
            dataSource.Save();
        }

        /// <summary>
        /// Deletes a song segment.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="songSegment">The song segment.</param>
        public void DeleteSongSegment(IDataSource dataSource, SongSegment songSegment)
        {
            if (!dataSource.IsConnected())
                return;

            var songSegmentTable = SongSegmentTable(dataSource);
            if (songSegmentTable == null)
                return;

            songSegmentTable.DeleteOnSubmit(songSegment);
            dataSource.Save();
        }

        /// <summary>
        /// Adds a song collaborator.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="songCollaborator">The song collaborator.</param>
        public void AddSongCollaborator(IDataSource dataSource, SongCollaborator songCollaborator)
        {
            if (!dataSource.IsConnected())
                return;

            var songCollaboratorTable = SongCollaboratorTable(dataSource);
            if (songCollaboratorTable == null)
                return;

            songCollaboratorTable.InsertOnSubmit(songCollaborator);
            dataSource.Save();
        }

        /// <summary>
        /// Deletes a song collaborator.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="songCollaborator">The song collaborator.</param>
        public void DeleteSongCollaborator(IDataSource dataSource, SongCollaborator songCollaborator)
        {
            if (!dataSource.IsConnected())
                return;

            var songCollaboratorTable = SongCollaboratorTable(dataSource);
            if (songCollaboratorTable == null)
                return;

            songCollaboratorTable.DeleteOnSubmit(songCollaborator);
            dataSource.Save();
        }

        /// <summary>
        /// Adds a song credit.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="songCredit">The song credit.</param>
        public void AddSongCredit(IDataSource dataSource, SongCredit songCredit)
        {
            if (!dataSource.IsConnected())
                return;

            if (!_personRepository.PersonExists(dataSource, songCredit.PersonId))
                _personRepository.AddPerson(dataSource, songCredit.Person);

            var songCreditTable = SongCreditTable(dataSource);
            if (songCreditTable == null)
                return;

            songCreditTable.InsertOnSubmit(songCredit);
            dataSource.Save();
        }

        /// <summary>
        /// Deletes a song credit.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="songCredit">The song credit.</param>
        public void DeleteSongCredit(IDataSource dataSource, SongCredit songCredit)
        {
            if (!dataSource.IsConnected())
                return;

            var songCreditTable = SongCreditTable(dataSource);
            if (songCreditTable == null)
                return;

            if (songCredit.Roles != null && songCredit.Roles.Any())
                foreach (var role in songCredit.Roles)
                    DeleteSongCreditRole(dataSource, songCredit, role);

            songCreditTable.DeleteOnSubmit(songCredit);
            dataSource.Save();
        }

        /// <summary>
        /// Adds a song credit role.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="songCredit">The song credit.</param>
        /// <param name="role">A role.</param>
        public void AddSongCreditRole(IDataSource dataSource, SongCredit songCredit, Role role)
        {
            if (!dataSource.IsConnected())
                return;

            if (!_roleRepository.RoleExists(dataSource, role.Id))
                _roleRepository.AddRole(dataSource, role);

            var songCreditRoleTable = SongCreditRoleTable(dataSource);
            if (songCreditRoleTable == null)
                return;

            var songCreditRole = SongCreditRole.Create(songCredit, role);

            songCreditRoleTable.InsertOnSubmit(songCreditRole);
            dataSource.Save();
        }

        /// <summary>
        /// Deletes a song credit role.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="songCredit">The song credit.</param>
        /// <param name="role">A role.</param>
        public void DeleteSongCreditRole(IDataSource dataSource, SongCredit songCredit, Role role)
        {
            if (!dataSource.IsConnected())
                return;

            var songCreditRoleTable = SongCreditRoleTable(dataSource);
            if (songCreditRoleTable == null)
                return;

            var songCreditRoleQuery =
                from scr in songCreditRoleTable
                where scr.SongCreditId == songCredit.Id && scr.RoleId == role.Id
                select scr;

            var songCreditRole = songCreditRoleQuery.SingleOrDefault();
            if (songCreditRole == null)
                return;

            songCreditRoleTable.DeleteOnSubmit(songCreditRole);
            dataSource.Save();
        }

        /// <summary>
        /// Adds a recording session.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="recordingSession">The recording session.</param>
        public void AddRecordingSession(IDataSource dataSource, RecordingSession recordingSession)
        {
            if (!dataSource.IsConnected())
                return;

            var recordingSessionTable = RecordingSessionTable(dataSource);
            if (recordingSessionTable == null)
                return;

            recordingSessionTable.InsertOnSubmit(recordingSession);
            dataSource.Save();
        }

        /// <summary>
        /// Deletes a recording session.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="recordingSession">The recording session.</param>
        public void DeleteRecordingSession(IDataSource dataSource, RecordingSession recordingSession)
        {
            if (!dataSource.IsConnected())
                return;

            var recordingSessionTable = RecordingSessionTable(dataSource);
            if (recordingSessionTable == null)
                return;

            recordingSessionTable.DeleteOnSubmit(recordingSession);
            dataSource.Save();
        }

        /// <summary>
        /// Deletes the song.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="song">The song.</param>
        public void DeleteSong(IDataSource dataSource, Song song)
        {
            if (!dataSource.IsConnected())
                return;

            var songTable = SongTable(dataSource);
            if (songTable == null)
                return;

            var recordingSessionTable = RecordingSessionTable(dataSource);
            var recordingSessionsQuery =
                from rs in recordingSessionTable
                where rs.SongId == song.Id
                select rs;

            recordingSessionTable.DeleteAllOnSubmit(recordingSessionsQuery.AsEnumerable());
            dataSource.Save();

            var songReferenceTable = SongReferenceTable(dataSource);
            var songReferencesQuery =
                from sr in songReferenceTable
                where sr.SongId == song.Id
                select sr;

            songReferenceTable.DeleteAllOnSubmit(songReferencesQuery.AsEnumerable());
            dataSource.Save();

            var songActivityTable = SongActivityTable(dataSource);
            var songActivitiesQuery =
                from sa in songActivityTable
                where sa.SongId == song.Id
                select sa;

            songActivityTable.DeleteAllOnSubmit(songActivitiesQuery.AsEnumerable());
            dataSource.Save();

            var songMoodTable = SongMoodTable(dataSource);
            var songMoodsQuery =
                from sm in songMoodTable
                where sm.SongId == song.Id
                select sm;

            songMoodTable.DeleteAllOnSubmit(songMoodsQuery.AsEnumerable());
            dataSource.Save();

            var songTagTable = SongTagTable(dataSource);
            var songTagsQuery =
                from st in songTagTable
                where st.SongId == song.Id
                select st;

            songTagTable.DeleteAllOnSubmit(songTagsQuery.AsEnumerable());
            dataSource.Save();

            var songReviewTable = SongReviewTable(dataSource);
            var songReviewsQuery =
                from sr in songReviewTable
                where sr.SongId == song.Id
                select sr;

            songReviewTable.DeleteAllOnSubmit(songReviewsQuery.AsEnumerable());
            dataSource.Save();

            var songCollaboratorTable = SongCollaboratorTable(dataSource);
            var songCollaboratorsQuery =
                from sc in songCollaboratorTable
                where sc.SongId == song.Id
                select sc;

            songCollaboratorTable.DeleteAllOnSubmit(songCollaboratorsQuery.AsEnumerable());
            dataSource.Save();

            var songSegmentTable = SongSegmentTable(dataSource);
            var songSegmentsQuery =
                from sc in songSegmentTable
                where sc.SongId == song.Id
                select sc;

            songSegmentTable.DeleteAllOnSubmit(songSegmentsQuery.AsEnumerable());
            dataSource.Save();

            var songCreditTable = SongCreditTable(dataSource);
            var songCreditsQuery =
                from sc in songCreditTable
                where sc.SongId == song.Id
                select sc;

            var credits = songCreditsQuery.ToList();
            foreach (var credit in credits)
            {
                var c = credit;
                var songCreditRoleTable = SongCreditRoleTable(dataSource);
                var songCreditRolesQuery =
                    from scr in songCreditRoleTable
                    where scr.SongCreditId == c.Id
                    select scr;

                songCreditRoleTable.DeleteAllOnSubmit(songCreditRolesQuery.AsEnumerable());
                dataSource.Save();

                songCreditTable.DeleteOnSubmit(credit);
                dataSource.Save();
            }

            songTable.DeleteOnSubmit(song);
            dataSource.Save();
        }

        #region Tables

        private static Table<Song> SongTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<Song>() : null;
        }

        private static Table<RecordingSession> RecordingSessionTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<RecordingSession>() : null;
        }

        private static Table<Artist> ArtistTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<Artist>() : null;
        }

        private static IEnumerable<Genre> GenreTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<Genre>() : null;
        }

        private static IEnumerable<Holiday> HolidayTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<Holiday>() : null;
        }

        private static Table<TimeSignature> TimeSignatureTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<TimeSignature>() : null;
        }

        private static Table<SongSegment> SongSegmentTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<SongSegment>() : null;
        }

        private static Table<SongCollaborator> SongCollaboratorTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<SongCollaborator>() : null;
        }

        private static Table<SongCredit> SongCreditTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<SongCredit>() : null;
        }

        private static Table<SongCreditRole> SongCreditRoleTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<SongCreditRole>() : null;
        }

        private static Table<SongReference> SongReferenceTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<SongReference>() : null;
        }

        private static Table<SongActivity> SongActivityTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<SongActivity>() : null;
        }

        private static Table<SongMood> SongMoodTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<SongMood>() : null;
        }

        private static Table<SongTag> SongTagTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<SongTag>() : null;
        }

        private static Table<SongReview> SongReviewTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<SongReview>() : null;
        }

        private static Table<Role> RoleTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<Role>() : null;
        }

        private static IEnumerable<Person> PersonTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<Person>() : null;
        }

        #endregion
    }
}
