using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bloom.Common.ExtensionMethods
{
    /// <summary>
    /// String extension methods.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// The file system reserved characters
        /// </summary>
        public static IEnumerable<char> FilesystemReservedCharacters = new List<char> { '<', '>', ':', '"', '/', '\\', '|', '?', '*' };

        /// <summary>
        /// File system safe replacements for reserved characters.
        /// </summary>
        public static Dictionary<char, char> FilesystemReservedCharacterReplacements = new Dictionary<char, char>
        {
            { '<', '(' },
            { '>', ')' },
            { ':', '-' },
            { '"', '\'' },
            { '/', '-' },
            { '\\', '-' },
            { '|', '-' },
            { '?', '!' },
            { '*', '+' },
        };

        /// <summary>
        /// The maximum string length of a file name.
        /// </summary>
        public const int FileNameLengthMaximum = 260;

        /// <summary>
        /// Determines whether this string is a valid file name.
        /// </summary>
        /// <param name="s">This string.</param>
        public static bool IsValidFileName(this string s)
        {
            if (s.Length > FileNameLengthMaximum)
                return false;

            return !FilesystemReservedCharacters.Any(s.Contains);
        }

        /// <summary>
        /// Returns a copy of this string that is safe to use as a file name.
        /// </summary>
        /// <param name="s">This string.</param>
        public static string AsFileName(this string s)
        {
            var fileName = s.Length > FileNameLengthMaximum ? s.Substring(0, FileNameLengthMaximum) : s;

            if (FilesystemReservedCharacters.Any(fileName.Contains))
                fileName = ReplaceFilesystemReservedCharacters(fileName);

            return fileName;
        }

        /// <summary>
        /// Returns a copy of this string that is safe to use as a folder name.
        /// </summary>
        /// <param name="s">This string.</param>
        public static string AsFolderName(this string s)
        {
            return AsFileName(s);
        }

        /// <summary>
        /// Returns a copy of this string with all non alpha numeric chararcters removed to use as a dictionary key.
        /// </summary>
        /// <param name="s">This string.</param>
        public static string AsKey(this string s)
        {
            var stringBuilder = new StringBuilder();
            foreach (var c in s.Where(Char.IsLetterOrDigit))
                stringBuilder.Append(c);
            
            return stringBuilder.ToString().ToLowerInvariant();
        }

        /// <summary>
        /// Replaces the filesystem reserved characters in this string.
        /// </summary>
        /// <param name="s">This string.</param>
        public static string ReplaceFilesystemReservedCharacters(this string s)
        {
            foreach (var reservedCharacter in FilesystemReservedCharacters)
                s = s.Replace(reservedCharacter, FilesystemReservedCharacterReplacements[reservedCharacter]);

            return s;
        }
    }
}
