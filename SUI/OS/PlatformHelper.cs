using System.Runtime.InteropServices;

namespace SUI.OS;

public static class PlatformHelper
{
    public static T PSelect<T>(T Win, T Mac, T Nix)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return Win;
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            return Mac;
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            return Nix;
        }
        else
        {
            throw new PlatformNotSupportedException("Unable to complete Platform Select: Unknown platform!");
        }
    }
}