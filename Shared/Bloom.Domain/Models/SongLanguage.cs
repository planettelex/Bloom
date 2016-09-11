using System;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents an association between a song and a language.
    /// </summary>
    [Table(Name = "song_language")] 
    public class SongLanguage
    {
        /// <summary>
        /// Creates a new song language instance.
        /// </summary>
        /// <param name="song">The song.</param>
        /// <param name="language">The language.</param>
        public static SongLanguage Create(Song song, Language language)
        {
            return new SongLanguage
            {
                SongId = song.Id,
                LanguageId = language.Id
            };
        }

        /// <summary>
        /// Gets or sets the song identifier.
        /// </summary>
        [Column(Name = "song_id", IsPrimaryKey = true)]
        public Guid SongId { get; set; }

        /// <summary>
        /// Gets or sets the language identifier.
        /// </summary>
        [Column(Name = "language_id", IsPrimaryKey = true)]
        public Guid LanguageId { get; set; }
    }
}
