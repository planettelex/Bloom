using System.Collections.Generic;
using System.Linq;

namespace Bloom.Common.ExtensionMethods
{
    public static class StringExtensions
    {
        public static IEnumerable<char> FilesystemReservedCharacters = new List<char> {'<','>',':','"','/','\\','|','?','*'};
        
        public static bool IsValidFileName(this string s)
        {
            if (s.Length > 260)
                return false;

            if (FilesystemReservedCharacters.Any(s.Contains))
                return false;

            return true;
        }

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
