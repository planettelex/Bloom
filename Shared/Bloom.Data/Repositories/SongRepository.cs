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
                join timeSignature in timeSignatureTable on segment.TimeSignatureId equals timeSignature.Id
                where segment.SongId == songId
                orderby segment.StartTime
                select new
                {
                    Segment = segment,
                    TimeSignature = timeSignature
                };

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
                orderby artist.Name
                select new 
                {
                    Collaborator = collaborator,
                    Artist = artist
                };

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
                join artist in artistTable on s.ArtistId equals artist.Id
                join genre in genreTable on s.GenreId equals genre.Id
                join holiday in holidayTable on s.HolidayId equals holiday.Id
                join timeSignature in timeSignatureTable on s.TimeSignatureId equals timeSignature.Id
                select new Song
                {
                    Id = s.Id,
                    Name = s.Name,
                    Version = s.Version,
                    ArtistId = s.ArtistId,
                    Artist = new Artist
                    {
                        Id = artist.Id,
                        Name = artist.Name,
                        IsSolo = artist.IsSolo,
                        Bio = artist.Bio,
                        StartedOn = artist.StartedOn,
                        EndedOn = artist.EndedOn,
                        Twitter = artist.Twitter
                    },
                    Description = s.Description,
                    GenreId = s.GenreId,
                    Genre = s.GenreId == Guid.Empty ? null : new Genre
                    {
                        Id = genre.Id,
                        Name = genre.Name,
                        Description = genre.Description,
                        ParentGenreId = genre.ParentGenreId
                    },
                    Length = s.Length,
                    Bpm = s.Bpm,
                    Key = s.Key,
                    TimeSignatureId = s.TimeSignatureId,
                    TimeSignature = s.TimeSignatureId == Guid.Empty ? null : new TimeSignature
                    {
                        Id = timeSignature.Id,
                        BeatsPerMeasure = timeSignature.BeatsPerMeasure,
                        BeatLength = timeSignature.BeatLength
                    },
                    Lyrics = s.Lyrics,
                    AboutDayOfWeek = s.AboutDayOfWeek,
                    AboutTimeOfYear = s.AboutTimeOfYear,
                    BestPlayedAtStart = s.BestPlayedAtStart,
                    BestPlayedAtStop = s.BestPlayedAtStop,
                    HasExplicitContent = s.HasExplicitContent,
                    IsRemix = s.IsRemix,
                    IsCover = s.IsCover,
                    IsLive = s.IsLive,
                    OriginalSongId = s.OriginalSongId,
                    IsHoliday = s.IsHoliday,
                    HolidayId = s.HolidayId,
                    Holiday = s.HolidayId == Guid.Empty ? null : new Holiday
                    {
                        Id = holiday.Id,
                        Name = holiday.Name,
                        Description = holiday.Description,
                        StartDay = holiday.StartDay,
                        StartMonth = holiday.StartMonth,
                        EndDay = holiday.EndDay,
                        EndMonth = holiday.EndMonth
                    }
                };

            return songsQuery.ToList();
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
        /// Adds a song credit.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="songCredit">The song credit.</param>
        public void AddSongCredit(IDataSource dataSource, SongCredit songCredit)
        {
            if (!dataSource.IsConnected())
                return;

            var songCreditTable = SongCreditTable(dataSource);
            if (songCreditTable == null)
                return;

            songCreditTable.InsertOnSubmit(songCredit);
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

            songTable.DeleteOnSubmit(song);
            dataSource.Save();
        }

        #region Tables

        private static Table<Song> SongTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<Song>() : null;
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
