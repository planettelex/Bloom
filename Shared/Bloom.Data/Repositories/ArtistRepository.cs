using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    /// <summary>
    /// Access methods for artist data.
    /// </summary>
    public class ArtistRepository : IArtistRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArtistRepository" /> class.
        /// </summary>
        /// <param name="roleRepository">The role repository.</param>
        /// <param name="photoRespository">The photo respository.</param>
        /// <param name="personRepository">The person repository.</param>
        public ArtistRepository(IRoleRepository roleRepository, IPhotoRespository photoRespository, IPersonRepository personRepository)
        {
            _roleRepository = roleRepository;
            _photoRespository = photoRespository;
            _personRepository = personRepository;
        }
        private readonly IRoleRepository _roleRepository;
        private readonly IPhotoRespository _photoRespository;
        private readonly IPersonRepository _personRepository;

        /// <summary>
        /// Gets the artist.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="artistId">The artist identifier.</param>
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

            var artistMemberTable = ArtistMemberTable(dataSource);
            var membersQuery =
                from member in artistMemberTable
                where member.ArtistId == artistId
                orderby member.Priority
                select member;

            artist.Members = membersQuery.ToList();

            if (artist.Members == null) 
                return artist;
            
            var artistMemberRoleTable = ArtistMemberRoleTable(dataSource);
            var personTable = PersonTable(dataSource);
            var roleTable = RoleTable(dataSource);
            foreach (var artistMember in artist.Members)
            {
                var am = artistMember;
                var personQuery =
                    from person in personTable
                    where person.Id == am.PersonId
                    select person;

                artistMember.Person = personQuery.SingleOrDefault();

                var rolesQuery =
                    from amr in artistMemberRoleTable
                    join role in roleTable on amr.RoleId equals role.Id
                    where amr.ArtistMemberId == am.Id
                    orderby role.Name
                    select role;

                artistMember.Roles = rolesQuery.ToList();
            }

            return artist;
        }

        /// <summary>
        /// Lists the artists.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
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
                from a in artistTable
                from artistPhoto in artistPhotoTable.Where(ap => ap.ArtistId == a.Id && ap.Priority == 1).DefaultIfEmpty()
                from photo in photoTable.Where(p => artistPhoto.PhotoId == p.Id).DefaultIfEmpty()
                orderby a.Name
                select new 
                {
                    Artist = a,
                    ProfileImage = photo
                };

            var results = artistsQuery.ToList();
            var artists = new List<Artist>();
            foreach (var result in results)
            {
                var artist = result.Artist;
                artist.ProfileImage = result.ProfileImage;
                artists.Add(artist);
            }

            return artists;
        }

        /// <summary>
        /// Adds an artist.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="artist">The artist.</param>
        public void AddArtist(IDataSource dataSource, Artist artist)
        {
            if (!dataSource.IsConnected())
                return;

            var artistTable = ArtistTable(dataSource);
            if (artistTable == null)
                return;

            artistTable.InsertOnSubmit(artist);
            dataSource.Save();
        }

        /// <summary>
        /// Adds an artist member.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="member">The artist member.</param>
        public void AddArtistMember(IDataSource dataSource, ArtistMember member)
        {
            if (!dataSource.IsConnected())
                return;

            if (!_personRepository.PersonExists(dataSource, member.PersonId))
                _personRepository.AddPerson(dataSource, member.Person);

            var artistMemberTable = ArtistMemberTable(dataSource);
            if (artistMemberTable == null)
                return;

            artistMemberTable.InsertOnSubmit(member);
            dataSource.Save();
        }

        /// <summary>
        /// Adds an artist member role.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="member">The member.</param>
        /// <param name="role">The role.</param>
        public void AddArtistMemberRole(IDataSource dataSource, ArtistMember member, Role role)
        {
            if (!dataSource.IsConnected())
                return;

            if (!_roleRepository.RoleExists(dataSource, role.Id))
                _roleRepository.AddRole(dataSource, role);

            var artistMemberRoleTable = ArtistMemberRoleTable(dataSource);
            if (artistMemberRoleTable == null)
                return;

            var artistMemberRole = ArtistMemberRole.Create(member, role);

            artistMemberRoleTable.InsertOnSubmit(artistMemberRole);
            dataSource.Save();
        }

        /// <summary>
        /// Adds an artist photo.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="artist">An artist.</param>
        /// <param name="photo">The photo.</param>
        /// <param name="priority">The priority.</param>
        public void AddArtistPhoto(IDataSource dataSource, Artist artist, Photo photo, int priority)
        {
            if (!dataSource.IsConnected() || photo == null)
                return;

            if (!_photoRespository.PhotoExists(dataSource, photo.Id))
                _photoRespository.AddPhoto(dataSource, photo);

            var artistPhotoTable = ArtistPhotoTable(dataSource);
            if (artistPhotoTable == null)
                return;

            var artistPhoto = ArtistPhoto.Create(artist, photo, priority);
            artistPhotoTable.InsertOnSubmit(artistPhoto);
            dataSource.Save();
        }

        /// <summary>
        /// Deletes an artist.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="artist">The artist.</param>
        public void DeleteArtist(IDataSource dataSource, Artist artist)
        {
            if (!dataSource.IsConnected())
                return;

            var artistTable = ArtistTable(dataSource);
            if (artistTable == null)
                return;

            if (artist.Photos != null)
            {
                var artistPhotoTable = ArtistPhotoTable(dataSource);
                foreach (var photo in artist.Photos)
                {
                    var p = photo;
                    var artistPhotoQuery =
                        from ap in artistPhotoTable
                        where ap.ArtistId == artist.Id && ap.PhotoId == p.Id
                        select ap;

                    var artistPhoto = artistPhotoQuery.SingleOrDefault();
                    if (artistPhoto != null)
                        artistPhotoTable.DeleteOnSubmit(artistPhoto);
                }
                dataSource.Save();
            }

            if (artist.Members != null)
            {
                var artistMemberTable = ArtistMemberTable(dataSource);
                var artistMemberRoleTable = ArtistMemberRoleTable(dataSource);
                foreach (var member in artist.Members)
                {
                    var m = member;
                    foreach (var role in m.Roles)
                    {
                        var r = role;
                        var artistMemberRoleQuery =
                            from amr in artistMemberRoleTable
                            where amr.ArtistMemberId == m.Id && amr.RoleId == r.Id
                            select amr;

                        var artistMemberRole = artistMemberRoleQuery.SingleOrDefault();
                        if (artistMemberRole != null)
                            artistMemberRoleTable.DeleteOnSubmit(artistMemberRole);
                    }
                    dataSource.Save();
                    artistMemberTable.DeleteOnSubmit(m);
                }
                dataSource.Save();
            }

            artistTable.DeleteOnSubmit(artist);
            dataSource.Save();
        }

        #region Tables

        private static Table<Artist> ArtistTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<Artist>() : null;
        }

        private static Table<ArtistPhoto> ArtistPhotoTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<ArtistPhoto>() : null;
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

        private static Table<Person> PersonTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<Person>() : null;
        }

        private static IEnumerable<Photo> PhotoTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<Photo>() : null;
        }

        #endregion
    }
}
