using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using Bloom.Domain.Enums;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a recording label.
    /// </summary>
    [Table(Name = "label")]
    public class Label
    {
        /// <summary>
        /// Creates a new label instance.
        /// </summary>
        /// <param name="name">The label name.</param>
        public static Label Create(string name)
        {
            return new Label
            {
                Id = Guid.NewGuid(),
                Name = name
            };
        }

        /// <summary>
        /// Gets or sets the label identifier.
        /// </summary>
        [Column(Name = "id", IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the label name.
        /// </summary>
        [Column(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the label's bio.
        /// </summary>
        [Column(Name = "bio")]
        public string Bio { get; set; }

        /// <summary>
        /// Gets or sets the label logo URL.
        /// </summary>
        [Column(Name = "logo_url")]
        public string LogoUrl { get; set; }

        /// <summary>
        /// Gets or sets the date on which the label was founded.
        /// </summary>
        [Column(Name = "founded_on")]
        public DateTime? FoundedOn { get; set; }

        /// <summary>
        /// Gets or sets the date the label was closed.
        /// </summary>
        [Column(Name = "closed_on")]
        public DateTime? ClosedOn { get; set; }

        /// <summary>
        /// Gets or sets the label personel.
        /// </summary>
        public List<LabelPersonel> Personel { get; set; }

        #region AddPersonel

        /// <summary>
        /// Adds personel to this label.
        /// </summary>
        /// <param name="person">The person.</param>
        /// <returns>A new label personel.</returns>
        /// <exception cref="System.ArgumentNullException">person</exception>
        public LabelPersonel AddPersonel(Person person)
        {
            if (person == null)
                throw new ArgumentNullException("person");

            if (Personel == null)
                Personel = new List<LabelPersonel>();

            var labelPersonel = LabelPersonel.Create(this, person);
            Personel.Add(labelPersonel);

            return labelPersonel;
        }

        /// <summary>
        /// Adds personel to this label.
        /// </summary>
        /// <param name="person">The person.</param>
        /// <param name="roles">The person's roles.</param>
        /// <returns>A new label personel.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// person
        /// or
        /// roles</exception>
        public LabelPersonel AddPersonel(Person person, IList<Role> roles)
        {
            if (person == null)
                throw new ArgumentNullException("person");

            if (roles == null)
                throw new ArgumentNullException("roles");

            if (Personel == null)
                Personel = new List<LabelPersonel>();

            var personel = LabelPersonel.Create(this, person);
            foreach (var role in roles)
                personel.AddRole(role);

            Personel.Add(personel);

            return personel;
        }

        #endregion

        /// <summary>
        /// Gets or sets the label's releases.
        /// </summary>
        public List<AlbumRelease> Releases { get; set; }

        #region AddRelease

        /// <summary>
        /// Creates and adds a release of this album.
        /// </summary>
        /// <param name="album">The album.</param>
        /// <param name="releaseDate">The release date.</param>
        /// <returns>A new album release.</returns>
        /// <exception cref="System.ArgumentNullException">album</exception>
        public AlbumRelease AddRelease(Album album, DateTime releaseDate)
        {
            if (album == null)
                throw new ArgumentNullException("album");

            if (Releases == null)
                Releases = new List<AlbumRelease>();

            var albumRelease = AlbumRelease.Create(album, releaseDate);
            albumRelease.LabelId = Id;
            albumRelease.Label = this;

            Releases.Add(albumRelease);
            
            if (album.Releases == null)
                album.Releases = new List<AlbumRelease>();

            album.Releases.Add(albumRelease);

            return albumRelease;
        }

        /// <summary>
        /// Creates and adds a release of this album.
        /// </summary>
        /// <param name="album">The album.</param>
        /// <param name="releaseDate">The release date.</param>
        /// <param name="mediaTypes">The media types.</param>
        /// <returns>A new album release.</returns>
        /// <exception cref="System.ArgumentNullException">album</exception>
        public AlbumRelease AddRelease(Album album, DateTime releaseDate, MediaTypes mediaTypes)
        {
            if (album == null)
                throw new ArgumentNullException("album");

            if (Releases == null)
                Releases = new List<AlbumRelease>();

            var albumRelease = AlbumRelease.Create(album, releaseDate, mediaTypes);
            albumRelease.LabelId = Id;
            albumRelease.Label = this;

            Releases.Add(albumRelease);

            if (album.Releases == null)
                album.Releases = new List<AlbumRelease>();

            album.Releases.Add(albumRelease);

            return albumRelease;
        }

        /// <summary>
        /// Creates and adds a release of this album.
        /// </summary>
        /// <param name="album">The album.</param>
        /// <param name="releaseDate">The release date.</param>
        /// <param name="mediaTypes">The media types.</param>
        /// <param name="digitalFormats">The digital formats.</param>
        /// <returns>A new album release.</returns>
        /// <exception cref="System.ArgumentNullException">album</exception>
        public AlbumRelease AddRelease(Album album, DateTime releaseDate, MediaTypes mediaTypes, DigitalFormats digitalFormats)
        {
            if (album == null)
                throw new ArgumentNullException("album");

            if (Releases == null)
                Releases = new List<AlbumRelease>();

            var albumRelease = AlbumRelease.Create(album, releaseDate, mediaTypes, digitalFormats);
            albumRelease.LabelId = Id;
            albumRelease.Label = this;

            Releases.Add(albumRelease);

            if (album.Releases == null)
                album.Releases = new List<AlbumRelease>();

            album.Releases.Add(albumRelease);

            return albumRelease;
        }

        #endregion
    }
}
