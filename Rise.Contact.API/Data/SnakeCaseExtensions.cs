using System.Globalization;
using System.Text.RegularExpressions;

namespace Rise.Contact.API.Data
{
    public static class SnakeCaseExtensions
    {
        public static string ToSnakeCase(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            var prefixingUnderscores = Regex.Match(input, "^_+");
            return prefixingUnderscores + Regex.Replace(input, "([a-z0-9])([A-Z])", "$1_$2").ToLower(CultureInfo.GetCultureInfo("en"));
        }
    }
}