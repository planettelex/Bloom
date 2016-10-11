using System;
using System.IO;
using Bloom.Domain.Enums;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a media file.
    /// </summary>
    public class MediaFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MediaFile"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <exception cref="System.IO.FileNotFoundException"></exception>
        public MediaFile(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException(path + " not found.");

            Path = path;
        }

        /// <summary>
        /// Gets or sets the file path.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the metadata tag.
        /// </summary>
        public MediaTag Metadata { get; set; }

        /// <summary>
        /// Gets or sets the file's format.
        /// </summary>
        public DigitalFormats Format { get; set; }

        /// <summary>
        /// Gets or sets the file's duration.
        /// </summary>
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// Gets or sets the file's bitrate in kilobits per second.
        /// </summary>
        public int Bitrate { get; set; }

        /// <summary>
        /// Gets or sets the the number of samples of audio carried per second measured in Hz 
        /// </summary>
        public int SampleRate { get; set; }

        /// <summary>
        /// Gets or sets the file size in bytes.
        /// </summary>
        public long Size { get; set; }
    }
}
