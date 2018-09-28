using System.Linq;

public static class UtilityExtensions
{
    public static string FirstToUpper(this string s)
    {
        return s.First().ToString().ToUpper() + s.Substring(1);
    }
}
