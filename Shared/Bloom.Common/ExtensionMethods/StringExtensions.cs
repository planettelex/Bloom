using System.Collections.Generic;
using System.Linq;

namespace Bloom.Common.ExtensionMethods
{
    public static class StringExtensions
    {
        public static IEnumerable<char> FilesystemReservedCharacters = new List<char> {'<','>',':','"','/','\\','|','?','*'};
        
        public static bool IsValidFilename(this string s)
        {
            if (s.Length > 260)
                return false;

            if (FilesystemReservedCharacters.Any(s.Contains))
                return false;

            return true;
        }
    }
}
