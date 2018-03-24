using System;
using Bloom.Domain.Enums;

namespace Bloom.Browser.State.Domain.Models
{
    /// <summary>
    /// Represents user import preferences.
    /// </summary>
    public class ImportPreferences
    {
        /// <summary>
        /// Creates import preferences.
        /// </summary>
        public static ImportPreferences Create()
        {
            return new ImportPreferences
            {
                TitleCaseSongTitles = false,
                TitleCaseAlbumTitles = false,
                ComposersAlsoArtistMembers = false,
                MapGroupingTo = new Tuple<TaxonomyType, TaxonomyScope>(TaxonomyType.Tag, TaxonomyScope.Album),
                MapCommentsTo = new Tuple<TextPropertyType, ArtistScope>(TextPropertyType.Description, ArtistScope.Album)
            };
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether to title case song titles.
        /// </summary>
        public bool TitleCaseSongTitles { get; set; }

        /// <summary>
        ///  Gets or sets a value indicating whether to title case album titles.
        /// </summary>
        public bool TitleCaseAlbumTitles { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether composers are also artist members.
        /// </summary>
        public bool ComposersAlsoArtistMembers { get; set; }

        /// <summary>
        /// Gets or sets the taxonomy to map the grouping media tag property to.
        /// </summary>
        public Tuple<TaxonomyType, TaxonomyScope> MapGroupingTo { get; set; }

        /// <summary>
        /// Gets or sets the text property to map the comments media tag property to.
        /// </summary>
        public Tuple<TextPropertyType, ArtistScope> MapCommentsTo { get; set; }
    }
}
