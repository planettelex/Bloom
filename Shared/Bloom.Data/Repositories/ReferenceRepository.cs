using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    /// <summary>
    /// Access methods for reference data.
    /// </summary>
    public class ReferenceRepository : IReferenceRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceRepository"/> class.
        /// </summary>
        /// <param name="sourceRepository">The source repository.</param>
        public ReferenceRepository(ISourceRepository sourceRepository)
        {
            _sourceRepository = sourceRepository;
        }
        private readonly ISourceRepository _sourceRepository;

        /// <summary>
        /// Determines whether a reference exists.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="referenceId">The reference identifier.</param>
        public bool ReferenceExists(IDataSource dataSource, Guid referenceId)
        {
            var referenceTable = ReferenceTable(dataSource);
            if (referenceTable == null)
                return false;

            var referenceQuery =
                from reference in referenceTable
                where reference.Id == referenceId
                select reference;

            return referenceQuery.Any();
        }

        /// <summary>
        /// Gets the reference.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="referenceId">The reference identifier.</param>
        public Reference GetReference(IDataSource dataSource, Guid referenceId)
        {
            if (!dataSource.IsConnected())
                return null;

            var sourceTable = SourceTable(dataSource);
            var referenceTable = ReferenceTable(dataSource);
            if (referenceTable == null)
                return null;

            var referenceQuery =
                from r in referenceTable
                from source in sourceTable.Where(s => r.SourceId == s.Id).DefaultIfEmpty()
                where r.Id == referenceId
                select new
                {
                    Reference = r,
                    Source = source
                };

            var result = referenceQuery.SingleOrDefault();

            if (result == null)
                return null;

            var reference = result.Reference;
            if (reference == null)
                return null;

            reference.Source = result.Source;

            return reference;
        }

        /// <summary>
        /// Lists the references for the song.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="song">A song.</param>
        public List<Reference> ListReferences(IDataSource dataSource, Song song)
        {
            if (!dataSource.IsConnected() || song == null)
                return null;

            var sourceTable = SourceTable(dataSource);
            var referenceTable = ReferenceTable(dataSource);
            var songReferenceTable = SongReferenceTable(dataSource);
            if (songReferenceTable == null)
                return null;

            var referencesQuery =
                from sr in songReferenceTable
                join reference in referenceTable on sr.ReferenceId equals reference.Id
                from source in sourceTable.Where(s => reference.SourceId == s.Id).DefaultIfEmpty()
                orderby reference.Url
                where sr.SongId == song.Id
                select new
                {
                    Reference = reference,
                    Source = source
                };

            var results = referencesQuery.ToList();

            if (!results.Any())
                return null;

            var references = new List<Reference>();
            foreach (var result in results)
            {
                var reference = result.Reference;
                reference.Source = result.Source;
                references.Add(reference);
            }

            return references;
        }

        /// <summary>
        /// Lists the references for the album.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="album">An album.</param>
        public List<Reference> ListReferences(IDataSource dataSource, Album album)
        {
            if (!dataSource.IsConnected() || album == null)
                return null;

            var sourceTable = SourceTable(dataSource);
            var referenceTable = ReferenceTable(dataSource);
            var albumReferenceTable = AlbumReferenceTable(dataSource);
            if (albumReferenceTable == null)
                return null;

            var referencesQuery =
                from ar in albumReferenceTable
                join reference in referenceTable on ar.ReferenceId equals reference.Id
                from source in sourceTable.Where(s => reference.SourceId == s.Id).DefaultIfEmpty()
                orderby reference.Url
                where ar.AlbumId == album.Id
                select new
                {
                    Reference = reference,
                    Source = source
                };

            var results = referencesQuery.ToList();

            if (!results.Any())
                return null;

            var references = new List<Reference>();
            foreach (var result in results)
            {
                var reference = result.Reference;
                reference.Source = result.Source;
                references.Add(reference);
            }

            return references;
        }

        /// <summary>
        /// Lists the references for the artist.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="artist">An artist.</param>
        public List<Reference> ListReferences(IDataSource dataSource, Artist artist)
        {
            if (!dataSource.IsConnected() || artist == null)
                return null;

            var sourceTable = SourceTable(dataSource);
            var referenceTable = ReferenceTable(dataSource);
            var artistReferenceTable = ArtistReferenceTable(dataSource);
            if (artistReferenceTable == null)
                return null;

            var referencesQuery =
                from ar in artistReferenceTable
                join reference in referenceTable on ar.ReferenceId equals reference.Id
                from source in sourceTable.Where(s => reference.SourceId == s.Id).DefaultIfEmpty()
                orderby reference.Url
                where ar.ArtistId == artist.Id
                select new
                {
                    Reference = reference,
                    Source = source
                };

            var results = referencesQuery.ToList();

            if (!results.Any())
                return null;

            var references = new List<Reference>();
            foreach (var result in results)
            {
                var reference = result.Reference;
                reference.Source = result.Source;
                references.Add(reference);
            }

            return references;
        }

        /// <summary>
        /// Lists the references for the person.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="person">A person.</param>
        public List<Reference> ListReferences(IDataSource dataSource, Person person)
        {
            if (!dataSource.IsConnected() || person == null)
                return null;

            var sourceTable = SourceTable(dataSource);
            var referenceTable = ReferenceTable(dataSource);
            var personReferenceTable = PersonReferenceTable(dataSource);
            if (personReferenceTable == null)
                return null;

            var referencesQuery =
                from pr in personReferenceTable
                join reference in referenceTable on pr.ReferenceId equals reference.Id
                from source in sourceTable.Where(s => reference.SourceId == s.Id).DefaultIfEmpty()
                orderby reference.Url
                where pr.PersonId == person.Id
                select new
                {
                    Reference = reference,
                    Source = source
                };

            var results = referencesQuery.ToList();

            if (!results.Any())
                return null;

            var references = new List<Reference>();
            foreach (var result in results)
            {
                var reference = result.Reference;
                reference.Source = result.Source;
                references.Add(reference);
            }

            return references;
        }

        /// <summary>
        /// Lists the references for the playlist.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="playlist">A playlist.</param>
        public List<Reference> ListReferences(IDataSource dataSource, Playlist playlist)
        {
            if (!dataSource.IsConnected() || playlist == null)
                return null;

            var sourceTable = SourceTable(dataSource);
            var referenceTable = ReferenceTable(dataSource);
            var playlistReferenceTable = PlaylistReferenceTable(dataSource);
            if (playlistReferenceTable == null)
                return null;

            var referencesQuery =
                from pr in playlistReferenceTable
                join reference in referenceTable on pr.ReferenceId equals reference.Id
                from source in sourceTable.Where(s => reference.SourceId == s.Id).DefaultIfEmpty()
                orderby reference.Url
                where pr.PlaylistId == playlist.Id
                select new
                {
                    Reference = reference,
                    Source = source
                };

            var results = referencesQuery.ToList();

            if (!results.Any())
                return null;

            var references = new List<Reference>();
            foreach (var result in results)
            {
                var reference = result.Reference;
                reference.Source = result.Source;
                references.Add(reference);
            }

            return references;
        }

        /// <summary>
        /// Adds a reference.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="reference">The reference.</param>
        public void AddReference(IDataSource dataSource, Reference reference)
        {
            if (!dataSource.IsConnected())
                return;

            if (reference.SourceId != null && reference.Source != null && !_sourceRepository.SourceExists(dataSource, reference.SourceId.Value))
                _sourceRepository.AddSource(dataSource, reference.Source);

            var referenceTable = ReferenceTable(dataSource);
            if (referenceTable == null)
                return;

            referenceTable.InsertOnSubmit(reference);
            dataSource.Save();
        }

        /// <summary>
        /// Adds a reference to the given song.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="reference">A reference.</param>
        /// <param name="song">The song.</param>
        public void AddReferenceTo(IDataSource dataSource, Reference reference, Song song)
        {
            if (!dataSource.IsConnected())
                return;

            var songReferenceTable = SongReferenceTable(dataSource);
            if (songReferenceTable == null)
                return;

            songReferenceTable.InsertOnSubmit(SongReference.Create(song, reference));
            dataSource.Save();
        }

        /// <summary>
        /// Adds a reference to the given album.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="reference">A reference.</param>
        /// <param name="album">The album.</param>
        public void AddReferenceTo(IDataSource dataSource, Reference reference, Album album)
        {
            if (!dataSource.IsConnected())
                return;

            var albumReferenceTable = AlbumReferenceTable(dataSource);
            if (albumReferenceTable == null)
                return;

            albumReferenceTable.InsertOnSubmit(AlbumReference.Create(album, reference));
            dataSource.Save();
        }

        /// <summary>
        /// Adds a reference to the given artist.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="reference">A reference.</param>
        /// <param name="artist">The artist.</param>
        public void AddReferenceTo(IDataSource dataSource, Reference reference, Artist artist)
        {
            if (!dataSource.IsConnected())
                return;

            var artistReferenceTable = ArtistReferenceTable(dataSource);
            if (artistReferenceTable == null)
                return;

            artistReferenceTable.InsertOnSubmit(ArtistReference.Create(artist, reference));
            dataSource.Save();
        }

        /// <summary>
        /// Adds a reference to the given person.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="reference">A reference.</param>
        /// <param name="person">The person.</param>
        public void AddReferenceTo(IDataSource dataSource, Reference reference, Person person)
        {
            if (!dataSource.IsConnected())
                return;

            var personReferenceTable = PersonReferenceTable(dataSource);
            if (personReferenceTable == null)
                return;

            personReferenceTable.InsertOnSubmit(PersonReference.Create(person, reference));
            dataSource.Save();
        }

        /// <summary>
        /// Adds a reference to the given playlist.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="reference">A reference.</param>
        /// <param name="playlist">The playlist.</param>
        public void AddReferenceTo(IDataSource dataSource, Reference reference, Playlist playlist)
        {
            if (!dataSource.IsConnected())
                return;

            var playlistReferenceTable = PlaylistReferenceTable(dataSource);
            if (playlistReferenceTable == null)
                return;

            playlistReferenceTable.InsertOnSubmit(PlaylistReference.Create(playlist, reference));
            dataSource.Save();
        }

        /// <summary>
        /// Deletes the reference.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="reference">The reference.</param>
        public void DeleteReference(IDataSource dataSource, Reference reference)
        {
            if (!dataSource.IsConnected())
                return;

            var referenceTable = ReferenceTable(dataSource);
            if (referenceTable == null)
                return;

            var songReferenceTable = SongReferenceTable(dataSource);
            var songReferencesQuery =
                from sr in songReferenceTable
                where sr.ReferenceId == reference.Id
                select sr;

            songReferenceTable.DeleteAllOnSubmit(songReferencesQuery.AsEnumerable());
            dataSource.Save();

            var albumReferenceTable = AlbumReferenceTable(dataSource);
            var albumReferencesQuery =
                from ar in albumReferenceTable
                where ar.ReferenceId == reference.Id
                select ar;

            albumReferenceTable.DeleteAllOnSubmit(albumReferencesQuery.AsEnumerable());
            dataSource.Save();

            var playlistReferenceTable = PlaylistReferenceTable(dataSource);
            var playlistReferencesQuery =
                from pr in playlistReferenceTable
                where pr.ReferenceId == reference.Id
                select pr;

            playlistReferenceTable.DeleteAllOnSubmit(playlistReferencesQuery.AsEnumerable());
            dataSource.Save();

            var artistReferenceTable = ArtistReferenceTable(dataSource);
            var artistReferencesQuery =
                from ar in artistReferenceTable
                where ar.ReferenceId == reference.Id
                select ar;

            artistReferenceTable.DeleteAllOnSubmit(artistReferencesQuery.AsEnumerable());
            dataSource.Save();

            var personReferenceTable = PersonReferenceTable(dataSource);
            var personReferencesQuery =
                from pr in personReferenceTable
                where pr.ReferenceId == reference.Id
                select pr;

            personReferenceTable.DeleteAllOnSubmit(personReferencesQuery.AsEnumerable());
            dataSource.Save();

            referenceTable.DeleteOnSubmit(reference);
            dataSource.Save();
        }

        /// <summary>
        /// Deletes the reference from the given song.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="reference">The reference.</param>
        /// <param name="song">The song.</param>
        public void DeleteReferenceFrom(IDataSource dataSource, Reference reference, Song song)
        {
            if (!dataSource.IsConnected())
                return;

            var songReferenceTable = SongReferenceTable(dataSource);
            if (songReferenceTable == null)
                return;

            var songReferenceQuery =
                from sr in songReferenceTable
                where sr.ReferenceId == reference.Id && sr.SongId == song.Id
                select sr;

            var songReference = songReferenceQuery.SingleOrDefault();
            if (songReference == null)
                return;

            songReferenceTable.DeleteOnSubmit(songReference);
            dataSource.Save();
        }

        /// <summary>
        /// Deletes the reference from an album.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="reference">The reference.</param>
        /// <param name="album">The album.</param>
        public void DeleteReferenceFrom(IDataSource dataSource, Reference reference, Album album)
        {
            if (!dataSource.IsConnected())
                return;

            var albumReferenceTable = AlbumReferenceTable(dataSource);
            if (albumReferenceTable == null)
                return;

            var albumReferenceQuery =
                from ar in albumReferenceTable
                where ar.ReferenceId == reference.Id && ar.AlbumId == album.Id
                select ar;

            var albumReference = albumReferenceQuery.SingleOrDefault();
            if (albumReference == null)
                return;

            albumReferenceTable.DeleteOnSubmit(albumReference);
            dataSource.Save();
        }

        /// <summary>
        /// Deletes the reference from an artist.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="reference">The reference.</param>
        /// <param name="artist">The artist.</param>
        public void DeleteReferenceFrom(IDataSource dataSource, Reference reference, Artist artist)
        {
            if (!dataSource.IsConnected())
                return;

            var artistReferenceTable = ArtistReferenceTable(dataSource);
            if (artistReferenceTable == null)
                return;

            var artistReferenceQuery =
                from ar in artistReferenceTable
                where ar.ReferenceId == reference.Id && ar.ArtistId == artist.Id
                select ar;

            var artistReference = artistReferenceQuery.SingleOrDefault();
            if (artistReference == null)
                return;

            artistReferenceTable.DeleteOnSubmit(artistReference);
            dataSource.Save();
        }

        /// <summary>
        /// Deletes the reference from a person.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="reference">The reference.</param>
        /// <param name="person">The person.</param>
        public void DeleteReferenceFrom(IDataSource dataSource, Reference reference, Person person)
        {
            if (!dataSource.IsConnected())
                return;

            var personReferenceTable = PersonReferenceTable(dataSource);
            if (personReferenceTable == null)
                return;

            var personReferenceQuery =
                from pr in personReferenceTable
                where pr.ReferenceId == reference.Id && pr.PersonId == person.Id
                select pr;

            var personReference = personReferenceQuery.SingleOrDefault();
            if (personReference == null)
                return;

            personReferenceTable.DeleteOnSubmit(personReference);
            dataSource.Save();
        }

        /// <summary>
        /// Deletes the reference from a playlist.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="reference">The reference.</param>
        /// <param name="playlist">The playlist.</param>
        public void DeleteReferenceFrom(IDataSource dataSource, Reference reference, Playlist playlist)
        {
            if (!dataSource.IsConnected())
                return;

            var playlistReferenceTable = PlaylistReferenceTable(dataSource);
            if (playlistReferenceTable == null)
                return;

            var playlistReferenceQuery =
                from pr in playlistReferenceTable
                where pr.ReferenceId == reference.Id && pr.PlaylistId == playlist.Id
                select pr;

            var playlistReference = playlistReferenceQuery.SingleOrDefault();
            if (playlistReference == null)
                return;

            playlistReferenceTable.DeleteOnSubmit(playlistReference);
            dataSource.Save();
        }

        #region Tables

        private static Table<Reference> ReferenceTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<Reference>() : null;
        }

        private static IEnumerable<Source> SourceTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<Source>() : null;
        }

        private static Table<SongReference> SongReferenceTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<SongReference>() : null;
        }

        private static Table<AlbumReference> AlbumReferenceTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<AlbumReference>() : null;
        }

        private static Table<ArtistReference> ArtistReferenceTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<ArtistReference>() : null;
        }

        private static Table<PersonReference> PersonReferenceTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<PersonReference>() : null;
        }

        private static Table<PlaylistReference> PlaylistReferenceTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<PlaylistReference>() : null;
        }

        #endregion
    }
}
