using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    public class PlaylistRepository : IPlaylistRepository
    {
        public Playlist GetPlaylist(IDataSource dataSource, Guid playlistId)
        {
            if (!dataSource.IsConnected())
                return null;

            var personTable = PersonTable(dataSource);
            var playlistTable = PlaylistTable(dataSource);
            if (playlistTable == null)
                return null;

            var playlistQuery =
                from p in playlistTable
                join person in personTable on p.CreatedById equals person.Id
                where p.Id == playlistId
                select new Playlist
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Length = p.Length,
                    CreatedOn = p.CreatedOn,
                    CreatedById = p.CreatedById,
                    CreatedBy = new Person
                    {
                        Id = person.Id,
                        Name = person.Name,
                        BornOn = person.BornOn,
                        DiedOn = person.DiedOn,
                        Twitter = person.Twitter
                    }
                };

            var playlist = playlistQuery.SingleOrDefault();

            if (playlist == null)
                return null;

            var tracksTable = PlaylistTrackTable(dataSource);
            var songTable = SongTable(dataSource);
            var artistTable = ArtistTable(dataSource);
            var genreTable = GenreTable(dataSource);
            var timeSignatureTable = TimeSignatureTable(dataSource);
            var holidayTable = HolidayTable(dataSource);
            var tracksQuery =
                from track in tracksTable
                join song in songTable on track.SongId equals song.Id
                join genre in genreTable on song.GenreId equals genre.Id
                join artist in artistTable on song.ArtistId equals artist.Id
                join holiday in holidayTable on song.HolidayId equals holiday.Id
                join timeSignature in timeSignatureTable on song.TimeSignatureId equals timeSignature.Id
                where track.PlaylistId == playlistId
                orderby track.TrackNumber
                select new PlaylistTrack
                {
                    PlaylistId = playlistId,
                    TrackNumber = track.TrackNumber,
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
                            BeatsPerMeasure = timeSignature.BeatsPerMeasure,
                            BeatLength = timeSignature.BeatLength
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

            playlist.Tracks = tracksQuery.ToList();

            var playlistArtworkTable = PlaylistArtworkTable(dataSource);
            var artworkQuery =
                from artwork in playlistArtworkTable
                where artwork.PlaylistId == playlistId
                select artwork;

            playlist.Artwork = artworkQuery.ToList();

            return playlist;
        }

        public List<Playlist> ListPlaylists(IDataSource dataSource)
        {
            if (!dataSource.IsConnected())
                return null;

            var personTable = PersonTable(dataSource);
            var playlistTable = PlaylistTable(dataSource);
            if (playlistTable == null)
                return null;

            var playlistQuery =
                from p in playlistTable
                join person in personTable on p.CreatedById equals person.Id
                select new Playlist
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Length = p.Length,
                    CreatedOn = p.CreatedOn,
                    CreatedById = p.CreatedById,
                    CreatedBy = new Person
                    {
                        Id = person.Id,
                        Name = person.Name,
                        BornOn = person.BornOn,
                        DiedOn = person.DiedOn,
                        Twitter = person.Twitter
                    }
                };

            return playlistQuery.ToList();
        }

        public void AddPlaylist(IDataSource dataSource, Playlist playlist)
        {
            if (!dataSource.IsConnected())
                return;

            var playlistTable = PlaylistTable(dataSource);
            if (playlistTable == null)
                return;

            playlistTable.InsertOnSubmit(playlist);
        }

        public void AddPlaylistTrack(IDataSource dataSource, PlaylistTrack playlistTrack)
        {
            if (!dataSource.IsConnected())
                return;

            var playlistTrackTable = PlaylistTrackTable(dataSource);
            if (playlistTrackTable == null)
                return;

            playlistTrackTable.InsertOnSubmit(playlistTrack);
        }

        public void AddPlaylistArtwork(IDataSource dataSource, PlaylistArtwork playlistArtwork)
        {
            if (!dataSource.IsConnected())
                return;

            var playlistArtworkTable = PlaylistArtworkTable(dataSource);
            if (playlistArtworkTable == null)
                return;

            playlistArtworkTable.InsertOnSubmit(playlistArtwork);
        }

        public void DeletePlaylist(IDataSource dataSource, Playlist playlist)
        {
            if (!dataSource.IsConnected())
                return;

            var playlistTable = PlaylistTable(dataSource);
            if (playlistTable == null)
                return;

            playlistTable.DeleteOnSubmit(playlist);
        }

        private static Table<Playlist> PlaylistTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<Playlist>() : null;
        }

        private static Table<PlaylistTrack> PlaylistTrackTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<PlaylistTrack>() : null;
        }

        private static Table<PlaylistArtwork> PlaylistArtworkTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<PlaylistArtwork>() : null;
        }

        private static IEnumerable<Person> PersonTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<Person>() : null;
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
    }
}
