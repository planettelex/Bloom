namespace Bloom.Common
{
    public class RegExPattern
    {
        /// <summary>
        /// ^\p{L}+$
        /// </summary>
        public const string Alphabetic = @"^\p{L}+$";

        /// <summary>
        /// ^[\p{L}\s]*$
        /// </summary>
        public const string AlphabeticWithWhitespace = @"^[\p{L}\s]*$";

        /// <summary>
        /// ^[\p{L}\d]+$
        /// </summary>
        public const string AlphaNumeric = @"^[\p{L}\d]+$";

        /// <summary>
        /// ^[\p{L}\d\s]*$
        /// </summary>
        public const string AlphaNumericWithWhitespace = @"^[\p{L}\d\s]*$";

        /// <summary>
        /// ^[\p{L}\d_]*$
        /// </summary>
        public const string AlphaNumericWithUnderscore = @"^[\p{L}\d_]*$";

        /// <summary>
        /// ^[\d]+$
        /// </summary>
        public const string Numeric = @"^[\d]+$";

        /// <summary>
        /// ^[\d\s]*$
        /// </summary>
        public const string NumericWithWhitespace = @"^[\d\s]*$";

        /// <summary>
        /// ^\{?[a-fA-F0-9]{8}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{12}\}?$
        /// </summary>
        public const string Guid = @"^\{?[a-fA-F0-9]{8}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{12}\}?$";
    }
}
