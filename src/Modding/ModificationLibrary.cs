using System.IO;
using static Windows.Win32.PInvoke;
using static Windows.Win32.Foundation.HANDLE;
using static Windows.Win32.System.LibraryLoader.LOAD_LIBRARY_FLAGS;

namespace MCBE.CLI.Launcher.Modding;

internal unsafe sealed class ModificationLibrary
{
    internal readonly string FileName;

    internal readonly bool IsValid, Exists;

    internal ModificationLibrary(string path)
    {
        FileName = Path.GetFullPath(path);
        Exists = File.Exists(FileName) && Path.HasExtension(FileName);
        fixed (char* fileName = FileName) IsValid = Exists && FreeLibrary(LoadLibraryEx(fileName, Null, DONT_RESOLVE_DLL_REFERENCES));
    }

    public static implicit operator ModificationLibrary(string @this) => new(@this);
}