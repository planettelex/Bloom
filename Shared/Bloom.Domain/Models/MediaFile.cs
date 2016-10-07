using System;
using Bloom.Domain.Enums;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a media file.
    /// </summary>
    public class MediaFile
    {
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
        /// Gets or sets the number of audio channels.
        /// </summary>
        public int Channels { get; set; }

        /// <summary>
        /// Gets or sets the the number of samples of audio carried per second measured in Hz 
        /// </summary>
        public int SampleRate { get; set; }

        /// <summary>
        /// Gets or sets the number of bits per sample.
        /// </summary>
        public int BitsPerSample { get; set; }
    }
}
