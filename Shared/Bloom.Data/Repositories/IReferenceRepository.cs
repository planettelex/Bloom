using System;
using System.Collections.Generic;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    public interface IReferenceRepository
    {
        Reference GetReference(IDataSource dataSource, Guid referenceId);

        List<Reference> ListReferences(IDataSource dataSource, Song song);

        List<Reference> ListReferences(IDataSource dataSource, Album album);

        List<Reference> ListReferences(IDataSource dataSource, Artist artist);

        List<Reference> ListReferences(IDataSource dataSource, Person person);

        List<Reference> ListReferences(IDataSource dataSource, Playlist person);

        void AddReference(IDataSource dataSource, Reference reference);

        void AddReferenceTo(IDataSource dataSource, Reference reference, Song song);

        void AddReferenceTo(IDataSource dataSource, Reference reference, Album album);

        void AddReferenceTo(IDataSource dataSource, Reference reference, Artist artist);

        void AddReferenceTo(IDataSource dataSource, Reference reference, Person person);

        void AddReferenceTo(IDataSource dataSource, Reference reference, Playlist playlist);

        void DeleteReference(IDataSource dataSource, Reference reference);

        void DeleteReferenceTo(IDataSource dataSource, Reference reference, Song song);

        void DeleteReferenceTo(IDataSource dataSource, Reference reference, Album album);

        void DeleteReferenceTo(IDataSource dataSource, Reference reference, Artist artist);

        void DeleteReferenceTo(IDataSource dataSource, Reference reference, Person person);

        void DeleteReferenceTo(IDataSource dataSource, Reference reference, Playlist playlist);
    }
}
