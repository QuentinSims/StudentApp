using System.Text.RegularExpressions;

namespace Student.Shared.Utilities
{
    public class StringUtils
    {
        public static string FormatName(string Name)
        {
            Regex r = new Regex(
    @"(?<=[A-Z])(?=[A-Z][a-z])|(?<=[^A-Z])(?=[A-Z])|(?<=[A-Za-z])(?=[^A-Za-z])"
  );
            return r.Replace(Name, " ");
        }
    }
}
