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
        Digital = 1,
        CD = 2,
        Vinyl = 4,
        EightTrack = 8,
        Cassette = 16,
        DVD = 32,
        BluRay = 64
    }
}
