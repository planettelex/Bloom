using Bloom.Domain.Models;

namespace Bloom.Services
{
    /// <summary>
    /// Service for media tag operations.
    /// </summary>
    /// <seealso cref="Bloom.Services.IMediaTagService" />
    public class MediaTagService : IMediaTagService
    {
        /// <summary>
        /// Gets the media tag for the media at the given file path.
        /// </summary>
        /// <param name="filePath">The media's file path.</param>
        /// <returns></returns>
        public MediaTag GetMediaTag(string filePath)
        {
            // TODO: Implement
            return null;
        }

        /// <summary>
        /// Writes the media tag for the media at the given file path.
        /// </summary>
        /// <param name="filePath">The media's file path.</param>
        /// <param name="mediaTag">The media tag.</param>
        public void WriteMediaTag(string filePath, MediaTag mediaTag)
        {
            // TODO: Implement
        }
    }
}
