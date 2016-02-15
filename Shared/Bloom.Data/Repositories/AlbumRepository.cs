using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    public class AlbumRepository : IAlbumRepository
    {
        public Album GetAlbum(IDataSource dataSource, Guid albumId)
        {
            if (!dataSource.IsConnected())
                return null;

            var holidayTable = HolidayTable(dataSource);
            var artistTable = ArtistTable(dataSource);
            var albumTable = AlbumTable(dataSource);
            if (albumTable == null)
                return null;

            var albumQuery =
                from a in albumTable
                join artist in artistTable on a.ArtistId equals artist.Id
                join holiday in holidayTable on a.HolidayId equals holiday.Id
                where a.Id == albumId
                select new Album
                {
                    Id = a.Id,
                    Name = a.Name,
                    Edition = a.Edition,
                    ArtistId = a.ArtistId,
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
                    Description = a.Description,
                    Length = a.Length,
                    LengthType = a.LengthType,
                    LinerNotes = a.LinerNotes,
                    IsBootleg = a.IsBootleg,
                    IsLive = a.IsLive,
                    IsCompilation = a.IsCompilation,
                    IsPromotional = a.IsPromotional,
                    IsMixedArtist = a.IsMixedArtist,
                    IsRemix = a.IsRemix,
                    IsSingleTrack = a.IsSingleTrack,
                    IsSoundtrack = a.IsSoundtrack,
                    IsTribute = a.IsTribute,
                    TributeArtistId = a.TributeArtistId,
                    IsHoliday = a.IsHoliday,
                    HolidayId = a.HolidayId,
                    Holiday = a.HolidayId == Guid.Empty ? null : new Holiday
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

            var album = albumQuery.SingleOrDefault();

            if (album == null)
                return null;

            var tracksTable = AlbumTrackTable(dataSource);
            var songTable = SongTable(dataSource);
            var genreTable = GenreTable(dataSource);
            var timeSignatureTable = TimeSignatureTable(dataSource);
            var tracksQuery =
                from track in tracksTable
                join song in songTable on track.SongId equals song.Id
                join genre in genreTable on song.GenreId equals genre.Id
                join artist in artistTable on song.ArtistId equals artist.Id
                join holiday in holidayTable on song.HolidayId equals holiday.Id
                join timeSignature in timeSignatureTable on song.TimeSignatureId equals timeSignature.Id
                where track.AlbumId == albumId
                orderby track.DiscNumber, track.TrackNumber
                select new AlbumTrack
                {
                    Id = track.Id,
                    AlbumId = albumId,
                    DiscNumber = track.DiscNumber,
                    TrackNumber = track.TrackNumber,
                    StartTime = track.StartTime,
                    StopTime = track.StopTime,
                    SongId = track.SongId,
                    Song = new Song
                    {
                        Id = song.Id,
                        Name = song.Name,
                        Version = song.Version,
                        ArtistId = song.ArtistId,
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
                        Description = song.Description,
                        GenreId = song.GenreId,
                        Genre = song.GenreId == Guid.Empty ? null : new Genre
                        {
                            Id = genre.Id,
                            Name = genre.Name,
                            Description = genre.Description,
                            ParentGenreId = genre.ParentGenreId
                        },
                        Length = song.Length,
                        Bpm = song.Bpm,
                        Key = song.Key,
                        TimeSignatureId = song.TimeSignatureId,
                        TimeSignature = song.TimeSignatureId == Guid.Empty ? null : new TimeSignature
                        {
                            Id = timeSignature.Id,
                            Beats = timeSignature.Beats,
                            NoteLength = timeSignature.NoteLength
                        },
                        AboutDayOfWeek = song.AboutDayOfWeek,
                        AboutTimeOfYear = song.AboutTimeOfYear,
                        BestPlayedAtStart = song.BestPlayedAtStart,
                        BestPlayedAtStop = song.BestPlayedAtStop,
                        HasExplicitContent = song.HasExplicitContent,
                        IsRemix = song.IsRemix,
                        IsCover = song.IsCover,
                        IsLive = song.IsLive,
                        OriginalSongId = song.OriginalSongId,
                        IsHoliday = song.IsHoliday,
                        HolidayId = song.HolidayId,
                        Holiday = song.HolidayId == Guid.Empty ? null : new Holiday
                        {
                            Id = holiday.Id,
                            Name = holiday.Name,
                            Description = holiday.Description,
                            StartDay = holiday.StartDay,
                            StartMonth = holiday.StartMonth,
                            EndDay = holiday.EndDay,
                            EndMonth = holiday.EndMonth
                        }
                    }
                };

            album.Tracks = tracksQuery.ToList();

            var albumArtworkTable = AlbumArtworkTable(dataSource);
            var artworkQuery =
                from artwork in albumArtworkTable
                where artwork.AlbumId == albumId
                select artwork;

            album.Artwork = artworkQuery.ToList();

            var collaboratorsTable = AlbumCollaboratorTable(dataSource);
            var collaboratorsQuery =
                from collaborator in collaboratorsTable
                join artist in artistTable on collaborator.ArtistId equals artist.Id
                where collaborator.AlbumId == albumId
                orderby artist.Name
                select new AlbumCollaborator
                {
                    AlbumId = albumId,
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

            album.Collaborators = collaboratorsQuery.ToList();

            var personTable = PersonTable(dataSource);
            var creditsTable = AlbumCreditTable(dataSource);
            var creditsQuery =
                from albumCredit in creditsTable
                join person in personTable on albumCredit.PersonId equals person.Id
                where albumCredit.AlbumId == albumId
                orderby person.Name
                select new AlbumCredit
                {
                    Id = albumCredit.Id,
                    AlbumId = albumId,
                    PersonId = albumCredit.PersonId,
                    Person = new Person
                    {
                        Id = person.Id,
                        Name = person.Name,
                        BornOn = person.BornOn,
                        DiedOn = person.DiedOn,
                        Twitter = person.Twitter
                    }
                };

            album.Credits = creditsQuery.ToList();

            if (album.Credits == null)
                return album;

            var albumCreditRoleTable = AlbumCreditRoleTable(dataSource);
            var roleTable = RoleTable(dataSource);
            foreach (var credit in album.Credits)
            {
                var c = credit;
                var rolesQuery =
                    from acr in albumCreditRoleTable
                    join role in roleTable on acr.RoleId equals role.Id
                    where acr.AlbumCreditId == c.Id
                    orderby role.Name
                    select new Role
                    {
                        Id = role.Id,
                        Name = role.Name
                    };

                credit.Roles = rolesQuery.ToList();
            }

            return album;
        }

        public List<Album> ListAlbums(IDataSource dataSource)
        {
            if (!dataSource.IsConnected())
                return null;

            var holidayTable = HolidayTable(dataSource);
            var artistTable = ArtistTable(dataSource);
            var albumTable = AlbumTable(dataSource);
            if (albumTable == null)
                return null;

            var albumQuery =
                from a in albumTable
                join artist in artistTable on a.ArtistId equals artist.Id
                join holiday in holidayTable on a.HolidayId equals holiday.Id
                select new Album
                {
                    Id = a.Id,
                    Name = a.Name,
                    Edition = a.Edition,
                    ArtistId = a.ArtistId,
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
                    Description = a.Description,
                    Length = a.Length,
                    LengthType = a.LengthType,
                    LinerNotes = a.LinerNotes,
                    IsBootleg = a.IsBootleg,
                    IsLive = a.IsLive,
                    IsCompilation = a.IsCompilation,
                    IsPromotional = a.IsPromotional,
                    IsMixedArtist = a.IsMixedArtist,
                    IsRemix = a.IsRemix,
                    IsSingleTrack = a.IsSingleTrack,
                    IsSoundtrack = a.IsSoundtrack,
                    IsTribute = a.IsTribute,
                    TributeArtistId = a.TributeArtistId,
                    IsHoliday = a.IsHoliday,
                    HolidayId = a.HolidayId,
                    Holiday = a.HolidayId == Guid.Empty ? null : new Holiday
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

            return albumQuery.ToList();
        }

        public void AddAlbum(IDataSource dataSource, Album album)
        {
            if (!dataSource.IsConnected())
                return;

            var albumTable = AlbumTable(dataSource);
            if (albumTable == null)
                return;

            albumTable.InsertOnSubmit(album);
        }

        public void AddAlbumTrack(IDataSource dataSource, AlbumTrack albumTrack)
        {
            if (!dataSource.IsConnected())
                return;

            var albumTrackTable = AlbumTrackTable(dataSource);
            if (albumTrackTable == null)
                return;

            albumTrackTable.InsertOnSubmit(albumTrack);
        }

        public void AddAlbumArtwork(IDataSource dataSource, AlbumArtwork albumArtwork)
        {
            if (!dataSource.IsConnected())
                return;

            var albumArtworkTable = AlbumArtworkTable(dataSource);
            if (albumArtworkTable == null)
                return;

            albumArtworkTable.InsertOnSubmit(albumArtwork);
        }

        public void AddAlbumCollaborator(IDataSource dataSource, AlbumCollaborator albumCollaborator)
        {
            if (!dataSource.IsConnected())
                return;

            var albumCollaboratorTable = AlbumCollaboratorTable(dataSource);
            if (albumCollaboratorTable == null)
                return;

            albumCollaboratorTable.InsertOnSubmit(albumCollaborator);
        }

        public void AddAlbumCredit(IDataSource dataSource, AlbumCredit albumCredit)
        {
            if (!dataSource.IsConnected())
                return;

            var albumCreditTable = AlbumCreditTable(dataSource);
            if (albumCreditTable == null)
                return;

            albumCreditTable.InsertOnSubmit(albumCredit);
        }

        public void DeleteAlbum(IDataSource dataSource, Album album)
        {
            if (!dataSource.IsConnected())
                return;

            var albumTable = AlbumTable(dataSource);
            if (albumTable == null)
                return;

            albumTable.DeleteOnSubmit(album);
        }

        private static Table<Album> AlbumTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<Album>() : null;
        }

        private static IEnumerable<Song> SongTable(IDataSource dataSource)
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

        private static IEnumerable<TimeSignature> TimeSignatureTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<TimeSignature>() : null;
        }

        private static Table<Holiday> HolidayTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<Holiday>() : null;
        }

        private static Table<AlbumTrack> AlbumTrackTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<AlbumTrack>() : null;
        }

        private static Table<AlbumArtwork> AlbumArtworkTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<AlbumArtwork>() : null;
        }

        private static Table<AlbumCollaborator> AlbumCollaboratorTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<AlbumCollaborator>() : null;
        }

        private static Table<AlbumCredit> AlbumCreditTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<AlbumCredit>() : null;
        }

        private static Table<AlbumCreditRole> AlbumCreditRoleTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<AlbumCreditRole>() : null;
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
