﻿using System;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates an artist with a photo.
    /// </summary>
    [Table(Name = "artist_photo")]
    public class ArtistPhoto
    {
        /// <summary>
        /// Creates a new artist photo instance.
        /// </summary>
        /// <param name="artist">The artist.</param>
        /// <param name="photo">The photo.</param>
        /// <param name="priority">The order priority.</param>
        /// <returns></returns>
        public static ArtistPhoto Create(Artist artist, Photo photo, int priority)
        {
            return new ArtistPhoto
            {
                ArtistId = artist.Id,
                PhotoId = photo.Id,
                Photo = photo,
                Priority = priority
            };
        }

        /// <summary>
        /// Gets or sets the artist identifier.
        /// </summary>
        [Column(Name = "artist_id", IsPrimaryKey = true)]
        public Guid ArtistId { get; set; }

        /// <summary>
        /// Gets or sets the photo identifier.
        /// </summary>
        [Column(Name = "photo_id", IsPrimaryKey = true)]
        public Guid PhotoId { get; set; }

        /// <summary>
        /// Gets or sets the photo.
        /// </summary>
        public Photo Photo { get; set; }

        /// <summary>
        /// Gets or sets the order priority of this artist photo.
        /// </summary>
        [Column(Name = "priority")]
        public int Priority { get; set; }
    }
}
