using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bloom.Common.Properties;

namespace Bloom.Common.ExtensionMethods
{
    /// <summary>
    /// String extension methods.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// The positions of a word in a string.
        /// </summary>
        private enum WordPosition { First, Middle, Last }

        /// <summary>
        /// The strong word separators
        /// </summary>
        public static IEnumerable<char> StrongWordSeparators = new List<char> { ',', '.', '?', '!', '(', ')', '{', '}', '[', ']', '<', '>', '/', '&' };

        /// <summary>
        /// The weak word separators
        /// </summary>
        public static IEnumerable<char> WeakWordSeparators = new List<char> { ' ' };

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
        /// Gets a list of words to lowercase if in a title.
        /// </summary>
        public static IEnumerable<string> TitleWordsToLower => _toLower ?? (_toLower = Resources.TitleWordsToLower.Split(',', ' '));
        private static string[] _toLower;

        /// <summary>
        /// Uppercases the first letter of this string.
        /// </summary>
        /// <param name="s">This string.</param>
        public static string UppercaseFirstLetter(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return string.Empty;

            return char.ToUpper(s[0]) + s.Substring(1);
        }

        /// <summary>
        /// Cases a word in a string.
        /// </summary>
        /// <param name="wordToCase">The word to case.</param>
        /// <param name="wordPosition">The word's position.</param>
        /// <param name="preceedingSeparator">The word's preceeding separator.</param>
        private static string CaseWord(string wordToCase, WordPosition wordPosition, char preceedingSeparator)
        {
            // If the word is in our list to lowercase in the middle of the title and not after a strong separator it should be lowercased.
            if (TitleWordsToLower.Contains(wordToCase, StringComparer.OrdinalIgnoreCase) && wordPosition == WordPosition.Middle && WeakWordSeparators.Contains(preceedingSeparator))
                return wordToCase.ToLower();

            // The default casing uppercases the first letter and lowercases the rest.
            return UppercaseFirstLetter(wordToCase.ToLower());
        }

        /// <summary>
        /// Replaces a string at a given index.
        /// </summary>
        /// <param name="toReplace">To string to do the replacement on.</param>
        /// <param name="removeStartIndex">The start index of the remove.</param>
        /// <param name="removeCount">The number of characters to remove.</param>
        /// <param name="toInsert">The string to insert.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">toReplaceAt</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// removeStartIndex
        /// or
        /// removeCount
        /// </exception>
        private static string ReplaceAt(string toReplace, int removeStartIndex, int removeCount, string toInsert)
        {
            if (toReplace == null)
                throw new ArgumentNullException(nameof(toReplace));
            if (removeStartIndex >= toReplace.Length)
                throw new ArgumentOutOfRangeException(nameof(removeStartIndex));
            if (removeStartIndex + removeCount >= toReplace.Length)
                throw new ArgumentOutOfRangeException(nameof(removeCount));

            var removed = toReplace.Remove(removeStartIndex, removeCount);

            return removed.Insert(removeStartIndex, toInsert);
        }

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
        /// Returns this string in title case.
        /// </summary>
        /// <param name="s">This string.</param>
        public static string TitleCase(this string s)
        {
            if (s == null)
                return null;

            var stringBuilder = new StringBuilder();
            var currentWord = string.Empty;
            var lastWord = string.Empty;
            var lastSeparator = '\0';
            var wordCount = 0;
            var separators = WeakWordSeparators.Concat(StrongWordSeparators).ToList();

            foreach (var c in s)
            {
                if (separators.Contains(c)) // The current character is a separator.
                {
                    if (currentWord.Length > 0)
                    {
                        var position = wordCount == 0 ? WordPosition.First : WordPosition.Middle;
                        stringBuilder.Append(CaseWord(currentWord, position, lastSeparator));
                        lastWord = currentWord;
                        currentWord = string.Empty;
                        lastSeparator = '\0';
                        wordCount++;
                    }
                    stringBuilder.Append(c);

                    // Set lastSeparator to the current character, unless it is a space AND the last separator is a strong separator.
                    // This is so case word will work correctly after strong and space separators happen in succession.
                    if (!(StrongWordSeparators.Contains(lastSeparator) && char.IsWhiteSpace(c)))
                        lastSeparator = c;
                }
                else // The current character is not a separator.
                    currentWord += c;
            }

            if (currentWord.Length > 0) // Add the last word.
                stringBuilder.Append(CaseWord(currentWord, WordPosition.Last, lastSeparator));
            else // Add the last word when the last character was a separator.
            {
                var title = stringBuilder.ToString();
                var lastWordIndex = title.LastIndexOf(lastWord, StringComparison.OrdinalIgnoreCase);
                var toInsert = CaseWord(lastWord, WordPosition.Last, '\0');

                return ReplaceAt(title, lastWordIndex, lastWord.Length, toInsert);
            }
            return stringBuilder.ToString();
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

            return fileName.Trim();
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
