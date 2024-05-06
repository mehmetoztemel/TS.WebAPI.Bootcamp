namespace TaskManager.WebAPI.Extensions;

public static class ExtensionMethods
{
    public static string CreateFileName(this string fileName)
    {
        return string.Join("-", DateTime.Now.ToFileTime(), fileName);
    }
}
