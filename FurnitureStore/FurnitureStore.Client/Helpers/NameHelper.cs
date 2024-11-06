public static class NameHelper
{
    public static string GetLastName(string fullName)
    {
        if (string.IsNullOrEmpty(fullName))
            return string.Empty;

        var nameParts = fullName.Trim().Split(' ');
        return nameParts.Length > 0 ? nameParts[^1] : string.Empty;
    }
} 