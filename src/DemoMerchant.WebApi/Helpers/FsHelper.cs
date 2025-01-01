namespace DemoMerchant.WebApi.Helpers;

public static class FsHelper
{
    public static void EnsureDirectory(params string[] parameters)
    {
        var str = Directory.GetCurrentDirectory();
        foreach (string parameter in parameters)
            str = Path.Combine(str, parameter);
        if (Directory.Exists(str))
            return;
        Directory.CreateDirectory(str);
    }
}