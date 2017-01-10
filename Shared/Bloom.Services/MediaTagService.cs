using System;
using System.Drawing;
using System.IO;
using System.Linq;
using Bloom.Domain.Models;
using TagLib;
using File = TagLib.File;

namespace Bloom.Services
{
    /// <summary>
    /// Service for media tag operations.
    /// </summary>
    /// <seealso cref="Bloom.Services.IMediaTagService" />
    public class MediaTagService : IMediaTagService
    {
        /// <summary>
        /// Reads the media tag for the media at the given file path.
        /// </summary>
        /// <param name="filePath">The media's file path.</param>
        public MediaTag ReadMediaTag(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
                return null;

            var file = File.Create(filePath);
            if (file == null || file.Tag == null || file.Tag.IsEmpty)
                return null;

            return new MediaTag
            {
                Title = file.Tag.Title,
                AlbumName = file.Tag.Album,
                ArtistName = file.Tag.FirstPerformer,
                AlbumArtist = file.Tag.FirstAlbumArtist,
                GenreName = file.Tag.FirstGenre,
                Comments = file.Tag.Comment,
                TrackNumber = (int?) file.Tag.Track,
                TrackCount = (int?) file.Tag.TrackCount,
                DiscNumber = (int?) file.Tag.Disc,
                DiscCount = (int?) file.Tag.DiscCount,
                Grouping = file.Tag.Grouping,
                Year = (int?) file.Tag.Year,
                Composers = file.Tag.Composers != null && file.Tag.Composers.Any() ? file.Tag.Composers.First() : null,
                Bpm = file.Tag.BeatsPerMinute == 0 ? (double?) null : file.Tag.BeatsPerMinute
            };
        }

        /// <summary>
        /// Reads the image embedded in the media at the given file path.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        public Image ReadMediaImage(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
                return null;

            var file = File.Create(filePath);
            if (file == null || file.Tag == null || file.Tag.IsEmpty || file.Tag.Pictures == null || file.Tag.Pictures.Length == 0)
                return null;

            var imageData = file.Tag.Pictures[0].Data.Data;
            return Image.FromStream(new MemoryStream(imageData));
        }

        /// <summary>
        /// Writes the media tag for the media at the given file path.
        /// </summary>
        /// <param name="filePath">The media's file path.</param>
        /// <param name="mediaTag">The media tag.</param>
        public void WriteMediaTag(string filePath, MediaTag mediaTag)
        {
            if (!System.IO.File.Exists(filePath))
                return;

            var file = File.Create(filePath);
            if (file == null || file.Tag == null)
                return;

            file.Tag.Title = mediaTag.Title;
            file.Tag.Album = mediaTag.AlbumName;
            file.Tag.Performers = mediaTag.ArtistName != null ? new[] { mediaTag.ArtistName } : null;
            file.Tag.AlbumArtists = mediaTag.AlbumArtist != null ? new[] { mediaTag.AlbumArtist } : null;
            file.Tag.Genres = mediaTag.GenreName != null ? new[] { mediaTag.GenreName } : null;
            file.Tag.Comment = mediaTag.Comments;
            file.Tag.Track = mediaTag.TrackNumber.HasValue ? (uint) mediaTag.TrackNumber.Value : 0;
            file.Tag.TrackCount = mediaTag.TrackCount.HasValue ? (uint) mediaTag.TrackCount.Value : 0;
            file.Tag.Disc = mediaTag.DiscNumber.HasValue ? (uint) mediaTag.DiscNumber.Value : 0;
            file.Tag.DiscCount = mediaTag.DiscCount.HasValue ? (uint) mediaTag.DiscCount.Value : 0;
            file.Tag.Grouping = mediaTag.Grouping;
            file.Tag.Year = mediaTag.Year.HasValue ? (uint) mediaTag.Year.Value : 0;
            file.Tag.Composers = mediaTag.Composers != null ? new[] { mediaTag.Composers } : null;
            file.Tag.BeatsPerMinute = mediaTag.Bpm.HasValue ? (uint) Math.Round(mediaTag.Bpm.Value, 0) : 0;

            file.Save();
        }

        /// <summary>
        /// Writes the image embedded in the media at the given file path.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="mediaImage">The media image.</param>
        public void WriteMediaImage(string filePath, Image mediaImage)
        {
            if (!System.IO.File.Exists(filePath))
                return;

            var file = File.Create(filePath);
            if (file == null || file.Tag == null || mediaImage == null)
                return;

            var imageConverter = new ImageConverter();
            var imageBytes = (byte[]) imageConverter.ConvertTo(mediaImage, typeof(byte[]));
            var byteVector = new ByteVector(imageBytes);
            var picture = new Picture(byteVector);
            file.Tag.Pictures = new IPicture[] { picture };

            file.Save();
         }
    }
}
