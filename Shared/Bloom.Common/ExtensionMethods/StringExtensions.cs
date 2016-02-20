using System.Collections.Generic;
using System.Linq;

namespace Bloom.Common.ExtensionMethods
{
    /// <summary>
    /// String extension methods.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// The filesystem reserved characters
        /// </summary>
        public static IEnumerable<char> FilesystemReservedCharacters = new List<char> {'<','>',':','"','/','\\','|','?','*'};

        /// <summary>
        /// Determines whether the given string is a valid file name.
        /// </summary>
        /// <param name="s">The string.</param>
        public static bool IsValidFileName(this string s)
        {
            if (s.Length > 260)
                return false;

            return !FilesystemReservedCharacters.Any(s.Contains);
        }
    }
}
