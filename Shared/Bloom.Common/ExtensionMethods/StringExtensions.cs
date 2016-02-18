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

            if (FilesystemReservedCharacters.Any(s.Contains))
                return false;

            return true;
        }

        /// <summary>
        /// Gets the file name from.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static string GetFileName(this string s)
        {
            if (!s.Contains("\\"))
                return s;

            var parts = s.Split('\\');
            return parts[parts.Length - 1];
        }

        public static string GetFilePath(this string s)
        {
            if (!s.Contains("\\"))
                return s;

            var parts = s.Split('\\');
            if (parts.Length <= 2)
                return parts[0];

            var path = string.Empty;
            for (var i = 0; i < parts.Length - 1; i++)
                path += parts[i] + "\\";

            return path;
        }
    }
}
