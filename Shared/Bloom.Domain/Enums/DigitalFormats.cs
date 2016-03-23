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
        M4P = 4,
        WMA = 8,
        WAV = 16,
        OGG = 32,
        FLAC = 64
    }
}
