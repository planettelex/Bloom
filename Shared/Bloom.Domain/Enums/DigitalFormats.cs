using System;

namespace Bloom.Domain.Enums
{
    /// <summary>
    /// File formats of digital media.
    /// </summary>
    [Flags]
    public enum DigitalFormats
    {
        Unknown = 0,
        MP3 = 1,
        M4A = 2,
        WMA = 4,
        WAV = 8,
        OGG = 16,
        FLAC = 32
    }
}
