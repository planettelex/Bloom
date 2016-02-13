using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    public class ArtistRepository : IArtistRepository
    {
        public Artist GetArtist(IDataSource dataSource, Guid artistId)
        {
            if (!dataSource.IsConnected())
                return null;

            var artistTable = ArtistTable(dataSource);
            if (artistTable == null)
                return null;

            var artistQuery =
                from a in artistTable
                where a.Id == artistId
                select a;

            var artist = artistQuery.SingleOrDefault();

            if (artist == null)
                return null;

            var photoTable = PhotoTable(dataSource);
            var artistPhotoTable = ArtistPhotoTable(dataSource);
            var photosQuery =
                from ap in artistPhotoTable
                join photo in photoTable on ap.PhotoId equals photo.Id
                where ap.ArtistId == artistId
                orderby ap.Priority
                select photo;

            artist.Photos = photosQuery.ToList();

            var personTable = PersonTable(dataSource);
            var artistMemberTable = ArtistMemberTable(dataSource);
            var membersQuery =
                from member in artistMemberTable
                join person in personTable on member.PersonId equals person.Id
                where member.ArtistId == artistId
                orderby member.Priority
                select new ArtistMember
                {
                    Id = member.Id,
                    ArtistId = artistId,
                    Priority = member.Priority,
                    Started = member.Started,
                    Ended = member.Ended,
                    PersonId = member.PersonId,
                    Person = new Person
                    {
                        Id = person.Id,
                        Name = person.Name,
                        Twitter = person.Twitter,
                        BornOn = person.BornOn,
                        DiedOn = person.DiedOn
                    }
                };

            artist.Members = membersQuery.ToList();

            if (artist.Members == null) 
                return artist;
            
            var artistMemberRoleTable = ArtistMemberRoleTable(dataSource);
            var roleTable = RoleTable(dataSource);
            foreach (var artistMember in artist.Members)
            {
                var am = artistMember;
                var rolesQuery =
                    from amr in artistMemberRoleTable
                    join role in roleTable on amr.RoleId equals role.Id
                    where amr.ArtistMemberId == am.Id
                    orderby role.Name
                    select new Role
                    {
                        Id = role.Id,
                        Name = role.Name
                    };

                artistMember.Roles = rolesQuery.ToList();
            }

            return artist;
        }

        public List<Artist> ListArtists(IDataSource dataSource)
        {
            if (!dataSource.IsConnected())
                return null;

            var artistTable = ArtistTable(dataSource);
            if (artistTable == null)
                return null;

            var photoTable = PhotoTable(dataSource);
            var artistPhotoTable = ArtistPhotoTable(dataSource);
            var artistsQuery =
                from artist in artistTable
                join ap in artistPhotoTable on new { A = artist.Id, B = 1 } equals new { A = ap.ArtistId, B = ap.Priority }
                join photo in photoTable on ap.PhotoId equals photo.Id
                orderby artist.Name
                select new Artist
                {
                    Id = artist.Id,
                    StartedOn = artist.StartedOn,
                    EndedOn = artist.EndedOn,
                    IsSolo = artist.IsSolo,
                    ProfileImage = new Photo
                    {
                        Id = photo.Id,
                        Url = photo.Url,
                        Caption = photo.Caption,
                        TakenOn = photo.TakenOn
                    }
                };

            return artistsQuery.ToList();
        }

        public void AddArtist(IDataSource dataSource, Artist artist)
        {
            if (!dataSource.IsConnected())
                return;

            var artistTable = ArtistTable(dataSource);
            if (artistTable == null)
                return;

            artistTable.InsertOnSubmit(artist);
        }

        public void AddArtistMember(IDataSource dataSource, ArtistMember member)
        {
            if (!dataSource.IsConnected())
                return;

            var artistMemberTable = ArtistMemberTable(dataSource);
            if (artistMemberTable == null)
                return;

            artistMemberTable.InsertOnSubmit(member);
        }

        public void AddArtistPhoto(IDataSource dataSource, Artist artist, Photo photo, int priority)
        {
            if (!dataSource.IsConnected())
                return;

            var artistPhotoTable = ArtistPhotoTable(dataSource);
            if (artistPhotoTable == null)
                return;

            var artistPhoto = ArtistPhoto.Create(artist, photo, priority);
            artistPhotoTable.InsertOnSubmit(artistPhoto);
        }

        public void AddArtistReference(IDataSource dataSource, Artist artist, Reference reference)
        {
            if (!dataSource.IsConnected())
                return;

            var artistReferenceTable = ArtistReferenceTable(dataSource);
            if (artistReferenceTable == null)
                return;

            var artistReference = ArtistReference.Create(artist, reference);
            artistReferenceTable.InsertOnSubmit(artistReference);
        }

        public void DeleteArtist(IDataSource dataSource, Artist artist)
        {
            if (!dataSource.IsConnected())
                return;

            var artistTable = ArtistTable(dataSource);
            if (artistTable == null)
                return;

            artistTable.DeleteOnSubmit(artist);
        }

        private static Table<Artist> ArtistTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<Artist>() : null;
        }

        private static Table<ArtistPhoto> ArtistPhotoTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<ArtistPhoto>() : null;
        }

        private static Table<ArtistReference> ArtistReferenceTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<ArtistReference>() : null;
        }

        private static Table<ArtistMember> ArtistMemberTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<ArtistMember>() : null;
        }

        private static Table<ArtistMemberRole> ArtistMemberRoleTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<ArtistMemberRole>() : null;
        }

        private static Table<Role> RoleTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<Role>() : null;
        }

        private static IEnumerable<Person> PersonTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<Person>() : null;
        }

        private static IEnumerable<Photo> PhotoTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<Photo>() : null;
        }
    }
}
