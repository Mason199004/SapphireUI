using System.Runtime.InteropServices;

namespace SUI.Controls;

/// <summary>
/// Any control which has a platform specific builtin should inherit from this
/// </summary>
public interface IPlatformSpecificControl
{
    OSPlatform RequiredPlatform { get; }
}

public static class AnyPlatform
{
    public static readonly OSPlatform Any = OSPlatform.Create("ANY");
}