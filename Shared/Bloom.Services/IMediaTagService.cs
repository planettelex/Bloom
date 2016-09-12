using System.Drawing;
using Bloom.Domain.Models;

namespace Bloom.Services
{
    /// <summary>
    /// Service for media tag operations.
    /// </summary>
    public interface IMediaTagService
    {
        /// <summary>
        /// Reads the media tag for the media at the given file path.
        /// </summary>
        /// <param name="filePath">The media's file path.</param>
        MediaTag ReadMediaTag(string filePath);

        /// <summary>
        /// Reads the image embedded in the media at the given file path.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        Image ReadMediaImage(string filePath);

        /// <summary>
        /// Writes the media tag for the media at the given file path.
        /// </summary>
        /// <param name="filePath">The media's file path.</param>
        /// <param name="mediaTag">The media tag.</param>
        void WriteMediaTag(string filePath, MediaTag mediaTag);

        /// <summary>
        /// Writes the image embedded in the media at the given file path.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="mediaImage">The media image.</param>
        void WriteMediaImage(string filePath, Image mediaImage);
    }
}
