using Bloom.Domain.Models;

namespace Bloom.Services
{
    /// <summary>
    /// Service for media tag operations.
    /// </summary>
    public interface IMediaTagService
    {
        /// <summary>
        /// Gets the media tag for the media at the given file path.
        /// </summary>
        /// <param name="filePath">The media's file path.</param>
        MediaTag GetMediaTag(string filePath);

        /// <summary>
        /// Writes the media tag for the media at the given file path.
        /// </summary>
        /// <param name="filePath">The media's file path.</param>
        /// <param name="mediaTag">The media tag.</param>
        void WriteMediaTag(string filePath, MediaTag mediaTag);
    }
}
