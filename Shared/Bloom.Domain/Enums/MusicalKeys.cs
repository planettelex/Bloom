using System;

namespace Bloom.Domain.Enums
{
    /// <summary>
    /// The key for a piece of music (i.e. A Major, C Sharp Minor).
    /// </summary>
    [Flags]
    public enum MusicalKeys
    {
        None = 0,
        A = 1,
        Ab = 2,
        B = 4,
        Bb = 8,
        C = 16,
        D = 32,
        Db = 64,
        E = 128,
        Eb = 256,
        F = 512,
        G = 1024,
        Gb = 2048
    }
}
