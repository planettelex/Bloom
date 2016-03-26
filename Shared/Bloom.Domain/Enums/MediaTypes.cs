using System;

namespace Bloom.Domain.Enums
{
    /// <summary>
    /// Types of media.
    /// </summary>
    [Flags]
    public enum MediaTypes
    {
        None = 0,
        Vinyl = 1,
        ReelToReel = 2,
        FourTrack = 4,
        EightTrack = 8,
        Cassette = 16,
        DAT = 32,
        CD = 64,
        DVD = 128,
        BluRay = 256,
        Digital = 512
    }
}
