using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    /// <summary>
    /// Access methods for playlist data.
    /// </summary>
    public class PlaylistRepository : IPlaylistRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlaylistRepository"/> class.
        /// </summary>
        /// <param name="personRepository">The person repository.</param>
        public PlaylistRepository(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        private readonly IPersonRepository _personRepository;

        /// <summary>
        /// Gets the playlist.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="playlistId">The playlist identifier.</param>
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
                select new
                {
                    Playlist = p,
                    Person = person
                };

            var result = playlistQuery.SingleOrDefault();

            if (result == null)
                return null;

            var playlist = result.Playlist;
            if (playlist == null)
                return null;

            playlist.CreatedBy = result.Person;

            var tracksTable = PlaylistTrackTable(dataSource);
            var songTable = SongTable(dataSource);
            var artistTable = ArtistTable(dataSource);
            var genreTable = GenreTable(dataSource);
            var timeSignatureTable = TimeSignatureTable(dataSource);
            var holidayTable = HolidayTable(dataSource);
            var tracksQuery =
                from track in tracksTable
                join song in songTable on track.SongId equals song.Id
                from genre in genreTable.Where(g => song.GenreId == g.Id).DefaultIfEmpty()
                from artist in artistTable.Where(a => song.ArtistId == a.Id).DefaultIfEmpty()
                from holiday in holidayTable.Where(h => song.HolidayId == h.Id).DefaultIfEmpty()
                from timeSignature in timeSignatureTable.Where(t => song.TimeSignatureId == t.Id).DefaultIfEmpty()
                where track.PlaylistId == playlistId
                orderby track.TrackNumber
                select new
                {
                    Track = track,
                    Song = song,
                    Genre = genre,
                    Artist = artist,
                    Holiday = holiday,
                    TimeSignature = timeSignature
                };

            var results = tracksQuery.ToList();

            playlist.Tracks = null;
            if (results.Any())
            {
                playlist.Tracks = new List<PlaylistTrack>();
                foreach (var trackResult in results)
                {
                    var track = trackResult.Track;
                    track.Song = trackResult.Song;
                    track.Song.Artist = trackResult.Artist;
                    track.Song.Genre = trackResult.Genre;
                    track.Song.Holiday = trackResult.Holiday;
                    track.Song.TimeSignature = trackResult.TimeSignature;
                    playlist.Tracks.Add(track);
                }
            }

            var playlistArtworkTable = PlaylistArtworkTable(dataSource);
            var artworkQuery =
                from artwork in playlistArtworkTable
                where artwork.PlaylistId == playlistId
                select artwork;

            playlist.Artwork = artworkQuery.ToList();

            return playlist;
        }

        /// <summary>
        /// Lists the playlists.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        public List<Playlist> ListPlaylists(IDataSource dataSource)
        {
            if (!dataSource.IsConnected())
                return null;

            var personTable = PersonTable(dataSource);
            var playlistTable = PlaylistTable(dataSource);
            if (playlistTable == null)
                return null;

            var playlistQuery =
                from playlist in playlistTable
                join person in personTable on playlist.CreatedById equals person.Id
                orderby playlist.CreatedOn descending 
                select new
                {
                    Playlist = playlist,
                    Person = person
                };

            var results = playlistQuery.ToList();

            if (!results.Any()) 
                return null;

            var playlists = new List<Playlist>();
            foreach (var result in results)
            {
                var playlist = result.Playlist;
                playlist.CreatedBy = result.Person;
                playlists.Add(playlist);
            }

            return playlists;
        }

        /// <summary>
        /// Adds the playlist.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="playlist">The playlist.</param>
        public void AddPlaylist(IDataSource dataSource, Playlist playlist)
        {
            if (!dataSource.IsConnected())
                return;

            if (!_personRepository.PersonExists(dataSource, playlist.CreatedBy.Id))
                _personRepository.AddPerson(dataSource, playlist.CreatedBy);

            var playlistTable = PlaylistTable(dataSource);
            if (playlistTable == null)
                return;

            playlistTable.InsertOnSubmit(playlist);
            dataSource.Save();
        }

        /// <summary>
        /// Adds a track to the playlist.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="playlistTrack">The playlist track.</param>
        public void AddPlaylistTrack(IDataSource dataSource, PlaylistTrack playlistTrack)
        {
            if (!dataSource.IsConnected())
                return;

            var playlistTrackTable = PlaylistTrackTable(dataSource);
            if (playlistTrackTable == null)
                return;

            playlistTrackTable.InsertOnSubmit(playlistTrack);
            dataSource.Save();
        }

        /// <summary>
        /// Deletes a playlist track.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="playlistTrack">The playlist track.</param>
        public void DeletePlaylistTrack(IDataSource dataSource, PlaylistTrack playlistTrack)
        {
            if (!dataSource.IsConnected())
                return;

            var playlistTrackTable = PlaylistTrackTable(dataSource);
            if (playlistTrackTable == null)
                return;

            playlistTrackTable.DeleteOnSubmit(playlistTrack);
            dataSource.Save();
        }

        /// <summary>
        /// Adds playlist artwork.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="playlistArtwork">The playlist artwork.</param>
        public void AddPlaylistArtwork(IDataSource dataSource, PlaylistArtwork playlistArtwork)
        {
            if (!dataSource.IsConnected())
                return;

            var playlistArtworkTable = PlaylistArtworkTable(dataSource);
            if (playlistArtworkTable == null)
                return;

            playlistArtworkTable.InsertOnSubmit(playlistArtwork);
            dataSource.Save();
        }

        /// <summary>
        /// Deletes playlist artwork.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="playlistArtwork">The playlist artwork.</param>
        public void DeletePlaylistArtwork(IDataSource dataSource, PlaylistArtwork playlistArtwork)
        {
            if (!dataSource.IsConnected())
                return;

            var playlistArtworkTable = PlaylistArtworkTable(dataSource);
            if (playlistArtworkTable == null)
                return;

            playlistArtworkTable.DeleteOnSubmit(playlistArtwork);
            dataSource.Save();
        }

        /// <summary>
        /// Deletes the playlist.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="playlist">The playlist.</param>
        public void DeletePlaylist(IDataSource dataSource, Playlist playlist)
        {
            if (!dataSource.IsConnected())
                return;

            var playlistTable = PlaylistTable(dataSource);
            if (playlistTable == null)
                return;

            var playlistArtworkTable = PlaylistArtworkTable(dataSource);
            var playlistArtworkQuery =
                from pa in playlistArtworkTable
                where pa.PlaylistId == playlist.Id
                select pa;

            playlistArtworkTable.DeleteAllOnSubmit(playlistArtworkQuery.AsEnumerable());
            dataSource.Save();

            var playlistReferenceTable = PlaylistReferenceTable(dataSource);
            var playlistReferencesQuery =
                from pr in playlistReferenceTable
                where pr.PlaylistId == playlist.Id
                select pr;

            playlistReferenceTable.DeleteAllOnSubmit(playlistReferencesQuery.AsEnumerable());
            dataSource.Save();

            var playlistActivityTable = PlaylistActivityTable(dataSource);
            var playlistActivitiesQuery =
                from pa in playlistActivityTable
                where pa.PlaylistId == playlist.Id
                select pa;

            playlistActivityTable.DeleteAllOnSubmit(playlistActivitiesQuery.AsEnumerable());
            dataSource.Save();

            var playlistMoodTable = PlaylistMoodTable(dataSource);
            var playlistMoodsQuery =
                from pm in playlistMoodTable
                where pm.PlaylistId == playlist.Id
                select pm;

            playlistMoodTable.DeleteAllOnSubmit(playlistMoodsQuery.AsEnumerable());
            dataSource.Save();

            var playlistTagTable = PlaylistTagTable(dataSource);
            var playlistTagsQuery =
                from pt in playlistTagTable
                where pt.PlaylistId == playlist.Id
                select pt;

            playlistTagTable.DeleteAllOnSubmit(playlistTagsQuery.AsEnumerable());
            dataSource.Save();

            var playlistTrackTable = PlaylistTrackTable(dataSource);
            var playlistTrackQuery =
                from pt in playlistTrackTable
                where pt.PlaylistId == playlist.Id
                select pt;

            playlistTrackTable.DeleteAllOnSubmit(playlistTrackQuery.AsEnumerable());
            dataSource.Save();

            playlistTable.DeleteOnSubmit(playlist);
            dataSource.Save();
        }

        #region Tables

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

        private static Table<PlaylistReference> PlaylistReferenceTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<PlaylistReference>() : null;
        }

        private static Table<PlaylistActivity> PlaylistActivityTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<PlaylistActivity>() : null;
        }

        private static Table<PlaylistMood> PlaylistMoodTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<PlaylistMood>() : null;
        }

        private static Table<PlaylistTag> PlaylistTagTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<PlaylistTag>() : null;
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

        #endregion
    }
}
