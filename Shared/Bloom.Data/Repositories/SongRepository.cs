using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    public class SongRepository : ISongRepository
    {
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
                join artist in artistTable on s.ArtistId equals artist.Id
                join genre in genreTable on s.GenreId equals genre.Id
                join holiday in holidayTable on s.HolidayId equals holiday.Id
                join timeSignature in timeSignatureTable on s.TimeSignatureId equals timeSignature.Id
                where s.Id == songId
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
                        Beats = timeSignature.Beats,
                        NoteLength = timeSignature.NoteLength
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

            var song = songQuery.SingleOrDefault();

            if (song == null)
                return null;

            var segmentsTable = SongSegmentTable(dataSource);
            var segmentsQuery =
                from segment in segmentsTable
                join timeSignature in timeSignatureTable on segment.TimeSignatureId equals timeSignature.Id
                where segment.SongId == songId
                orderby segment.StartTime
                select new SongSegment
                {
                    Id = segment.Id,
                    SongId = songId,
                    StartTime = segment.StartTime,
                    StopTime = segment.StopTime,
                    Bpm = segment.Bpm,
                    Key = segment.Key,
                    TimeSignatureId = segment.TimeSignatureId,
                    TimeSignature = segment.TimeSignatureId == Guid.Empty ? null : new TimeSignature
                    {
                        Id = timeSignature.Id,
                        Beats = timeSignature.Beats,
                        NoteLength = timeSignature.NoteLength
                    }
                };

            song.Segments = segmentsQuery.ToList();

            var collaboratorsTable = SongCollaboratorTable(dataSource);
            var collaboratorsQuery =
                from collaborator in collaboratorsTable
                join artist in artistTable on collaborator.ArtistId equals artist.Id
                where collaborator.SongId == songId
                orderby artist.Name
                select new SongCollaborator
                {
                    SongId = songId,
                    ArtistId = artist.Id,
                    Artist = new Artist
                    {
                        Id = artist.Id,
                        Name = artist.Name,
                        IsSolo = artist.IsSolo,
                        StartedOn = artist.StartedOn,
                        EndedOn = artist.EndedOn,
                        Twitter = artist.Twitter
                    },
                    IsFeatured = collaborator.IsFeatured
                };

            song.Collaborators = collaboratorsQuery.ToList();

            var personTable = PersonTable(dataSource);
            var creditsTable = SongCreditTable(dataSource);
            var creditsQuery =
                from songCredit in creditsTable
                join person in personTable on songCredit.PersonId equals person.Id
                where songCredit.SongId == songId
                orderby person.Name
                select new SongCredit
                {
                    Id = songCredit.Id,
                    SongId = songId,
                    PersonId = songCredit.PersonId,
                    Person = new Person
                    {
                        Id = person.Id,
                        Name = person.Name,
                        BornOn = person.BornOn,
                        DiedOn = person.DiedOn,
                        Twitter = person.Twitter
                    },
                    IsFeatured = songCredit.IsFeatured
                };

            song.Credits = creditsQuery.ToList();

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
                    select new Role
                    {
                        Id = role.Id, 
                        Name = role.Name
                    };

                credit.Roles = rolesQuery.ToList();
            }

            return song;
        }

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
                        Beats = timeSignature.Beats,
                        NoteLength = timeSignature.NoteLength
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

        public void AddSong(IDataSource dataSource, Song song)
        {
            if (!dataSource.IsConnected())
                return;

            var songTable = SongTable(dataSource);
            if (songTable == null)
                return;

            songTable.InsertOnSubmit(song);
        }

        public void AddSongSegment(IDataSource dataSource, SongSegment songSegment)
        {
            if (!dataSource.IsConnected())
                return;

            var songSegmentTable = SongSegmentTable(dataSource);
            if (songSegmentTable == null)
                return;

            songSegmentTable.InsertOnSubmit(songSegment);
        }

        public void AddSongCollaborator(IDataSource dataSource, SongCollaborator songCollaborator)
        {
            if (!dataSource.IsConnected())
                return;

            var songCollaboratorTable = SongCollaboratorTable(dataSource);
            if (songCollaboratorTable == null)
                return;

            songCollaboratorTable.InsertOnSubmit(songCollaborator);
        }

        public void AddSongCredit(IDataSource dataSource, SongCredit songCredit)
        {
            if (!dataSource.IsConnected())
                return;

            var songCreditTable = SongCreditTable(dataSource);
            if (songCreditTable == null)
                return;

            songCreditTable.InsertOnSubmit(songCredit);
        }

        public void DeleteSong(IDataSource dataSource, Song song)
        {
            if (!dataSource.IsConnected())
                return;

            var songTable = SongTable(dataSource);
            if (songTable == null)
                return;

            songTable.DeleteOnSubmit(song);
        }

        private static Table<Song> SongTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<Song>() : null;
        }

        private static Table<Artist> ArtistTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<Artist>() : null;
        }

        private static Table<Genre> GenreTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<Genre>() : null;
        }

        private static Table<Holiday> HolidayTable(IDataSource dataSource)
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
    }
}
