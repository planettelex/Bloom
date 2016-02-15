using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    public class ReferenceRepository : IReferenceRepository
    {
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
                join source in sourceTable on r.SourceId equals source.Id
                where r.Id == referenceId
                select new Reference
                {
                    Id = r.Id,
                    Title = r.Title,
                    Url = r.Url,
                    SourceId = r.SourceId,
                    Source = new Source
                    {
                        Id = source.Id,
                        Name = source.Name,
                        Type = source.Type,
                        WebsiteUrl = source.WebsiteUrl
                    }
                };

            return referenceQuery.SingleOrDefault();
        }

        public List<Reference> ListReferences(IDataSource dataSource, Song song)
        {
            if (!dataSource.IsConnected() || song == null)
                return null;

            var sourceTable = SourceTable(dataSource);
            var referenceTable = ReferenceTable(dataSource);
            var songReferenceTable = SongReferenceTable(dataSource);
            if (songReferenceTable == null)
                return null;

            var referenceQuery =
                from sr in songReferenceTable
                join reference in referenceTable on sr.ReferenceId equals reference.Id
                join source in sourceTable on reference.SourceId equals source.Id
                where sr.SongId == song.Id
                select new Reference
                {
                    Id = reference.Id,
                    Title = reference.Title,
                    Url = reference.Url,
                    SourceId = reference.SourceId,
                    Source = new Source
                    {
                        Id = source.Id,
                        Name = source.Name,
                        Type = source.Type,
                        WebsiteUrl = source.WebsiteUrl
                    }
                };

            return referenceQuery.ToList();
        }

        public List<Reference> ListReferences(IDataSource dataSource, Album album)
        {
            if (!dataSource.IsConnected() || album == null)
                return null;

            var sourceTable = SourceTable(dataSource);
            var referenceTable = ReferenceTable(dataSource);
            var albumReferenceTable = AlbumReferenceTable(dataSource);
            if (albumReferenceTable == null)
                return null;

            var referenceQuery =
                from ar in albumReferenceTable
                join reference in referenceTable on ar.ReferenceId equals reference.Id
                join source in sourceTable on reference.SourceId equals source.Id
                where ar.AlbumId == album.Id
                select new Reference
                {
                    Id = reference.Id,
                    Title = reference.Title,
                    Url = reference.Url,
                    SourceId = reference.SourceId,
                    Source = new Source
                    {
                        Id = source.Id,
                        Name = source.Name,
                        Type = source.Type,
                        WebsiteUrl = source.WebsiteUrl
                    }
                };

            return referenceQuery.ToList();
        }

        public List<Reference> ListReferences(IDataSource dataSource, Artist artist)
        {
            if (!dataSource.IsConnected() || artist == null)
                return null;

            var sourceTable = SourceTable(dataSource);
            var referenceTable = ReferenceTable(dataSource);
            var artistReferenceTable = ArtistReferenceTable(dataSource);
            if (artistReferenceTable == null)
                return null;

            var referenceQuery =
                from ar in artistReferenceTable
                join reference in referenceTable on ar.ReferenceId equals reference.Id
                join source in sourceTable on reference.SourceId equals source.Id
                where ar.ArtistId == artist.Id
                select new Reference
                {
                    Id = reference.Id,
                    Title = reference.Title,
                    Url = reference.Url,
                    SourceId = reference.SourceId,
                    Source = new Source
                    {
                        Id = source.Id,
                        Name = source.Name,
                        Type = source.Type,
                        WebsiteUrl = source.WebsiteUrl
                    }
                };

            return referenceQuery.ToList();
        }

        public List<Reference> ListReferences(IDataSource dataSource, Person person)
        {
            if (!dataSource.IsConnected() || person == null)
                return null;

            var sourceTable = SourceTable(dataSource);
            var referenceTable = ReferenceTable(dataSource);
            var personReferenceTable = PersonReferenceTable(dataSource);
            if (personReferenceTable == null)
                return null;

            var referenceQuery =
                from pr in personReferenceTable
                join reference in referenceTable on pr.ReferenceId equals reference.Id
                join source in sourceTable on reference.SourceId equals source.Id
                where pr.PersonId == person.Id
                select new Reference
                {
                    Id = reference.Id,
                    Title = reference.Title,
                    Url = reference.Url,
                    SourceId = reference.SourceId,
                    Source = new Source
                    {
                        Id = source.Id,
                        Name = source.Name,
                        Type = source.Type,
                        WebsiteUrl = source.WebsiteUrl
                    }
                };

            return referenceQuery.ToList();
        }

        public List<Reference> ListReferences(IDataSource dataSource, Playlist playlist)
        {
            if (!dataSource.IsConnected() || playlist == null)
                return null;

            var sourceTable = SourceTable(dataSource);
            var referenceTable = ReferenceTable(dataSource);
            var playlistReferenceTable = PlaylistReferenceTable(dataSource);
            if (playlistReferenceTable == null)
                return null;

            var referenceQuery =
                from pr in playlistReferenceTable
                join reference in referenceTable on pr.ReferenceId equals reference.Id
                join source in sourceTable on reference.SourceId equals source.Id
                where pr.ReferenceId == reference.Id
                select new Reference
                {
                    Id = reference.Id,
                    Title = reference.Title,
                    Url = reference.Url,
                    SourceId = reference.SourceId,
                    Source = new Source
                    {
                        Id = source.Id,
                        Name = source.Name,
                        Type = source.Type,
                        WebsiteUrl = source.WebsiteUrl
                    }
                };

            return referenceQuery.ToList();
        }

        public void AddReference(IDataSource dataSource, Reference reference)
        {
            if (!dataSource.IsConnected())
                return;

            var referenceTable = ReferenceTable(dataSource);
            if (referenceTable == null)
                return;

            referenceTable.InsertOnSubmit(reference);
        }

        public void AddReferenceTo(IDataSource dataSource, Reference reference, Song song)
        {
            if (!dataSource.IsConnected())
                return;

            var songReferenceTable = SongReferenceTable(dataSource);
            if (songReferenceTable == null)
                return;

            songReferenceTable.InsertOnSubmit(SongReference.Create(song, reference));
        }

        public void AddReferenceTo(IDataSource dataSource, Reference reference, Album album)
        {
            if (!dataSource.IsConnected())
                return;

            var albumReferenceTable = AlbumReferenceTable(dataSource);
            if (albumReferenceTable == null)
                return;

            albumReferenceTable.InsertOnSubmit(AlbumReference.Create(album, reference));
        }

        public void AddReferenceTo(IDataSource dataSource, Reference reference, Artist artist)
        {
            if (!dataSource.IsConnected())
                return;

            var artistReferenceTable = ArtistReferenceTable(dataSource);
            if (artistReferenceTable == null)
                return;

            artistReferenceTable.InsertOnSubmit(ArtistReference.Create(artist, reference));
        }

        public void AddReferenceTo(IDataSource dataSource, Reference reference, Person person)
        {
            if (!dataSource.IsConnected())
                return;

            var personReferenceTable = PersonReferenceTable(dataSource);
            if (personReferenceTable == null)
                return;

            personReferenceTable.InsertOnSubmit(PersonReference.Create(person, reference));
        }

        public void AddReferenceTo(IDataSource dataSource, Reference reference, Playlist playlist)
        {
            if (!dataSource.IsConnected())
                return;

            var playlistReferenceTable = PlaylistReferenceTable(dataSource);
            if (playlistReferenceTable == null)
                return;

            playlistReferenceTable.InsertOnSubmit(PlaylistReference.Create(playlist, reference));
        }

        public void DeleteReference(IDataSource dataSource, Reference reference)
        {
            if (!dataSource.IsConnected())
                return;

            var referenceTable = ReferenceTable(dataSource);
            if (referenceTable == null)
                return;

            referenceTable.DeleteOnSubmit(reference);
        }

        public void DeleteReferenceTo(IDataSource dataSource, Reference reference, Song song)
        {
            if (!dataSource.IsConnected())
                return;

            var songReferenceTable = SongReferenceTable(dataSource);
            if (songReferenceTable == null)
                return;

            songReferenceTable.DeleteOnSubmit(SongReference.Create(song, reference));
        }

        public void DeleteReferenceTo(IDataSource dataSource, Reference reference, Album album)
        {
            if (!dataSource.IsConnected())
                return;

            var albumReferenceTable = AlbumReferenceTable(dataSource);
            if (albumReferenceTable == null)
                return;

            albumReferenceTable.DeleteOnSubmit(AlbumReference.Create(album, reference));
        }

        public void DeleteReferenceTo(IDataSource dataSource, Reference reference, Artist artist)
        {
            if (!dataSource.IsConnected())
                return;

            var artistReferenceTable = ArtistReferenceTable(dataSource);
            if (artistReferenceTable == null)
                return;

            artistReferenceTable.DeleteOnSubmit(ArtistReference.Create(artist, reference));
        }

        public void DeleteReferenceTo(IDataSource dataSource, Reference reference, Person person)
        {
            if (!dataSource.IsConnected())
                return;

            var personReferenceTable = PersonReferenceTable(dataSource);
            if (personReferenceTable == null)
                return;

            personReferenceTable.DeleteOnSubmit(PersonReference.Create(person, reference));
        }

        public void DeleteReferenceTo(IDataSource dataSource, Reference reference, Playlist playlist)
        {
            if (!dataSource.IsConnected())
                return;

            var playlistReferenceTable = PlaylistReferenceTable(dataSource);
            if (playlistReferenceTable == null)
                return;

            playlistReferenceTable.DeleteOnSubmit(PlaylistReference.Create(playlist, reference));
        }

        private static Table<Reference> ReferenceTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<Reference>() : null;
        }

        private static Table<Source> SourceTable(IDataSource dataSource)
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
    }
}
