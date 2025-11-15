using System.Runtime.InteropServices;
using System.Security;
using Windows.Win32.Foundation;
using static Windows.Win32.PInvoke;
using static Windows.Win32.System.Threading.PROCESS_ACCESS_RIGHTS;

namespace MCBE.CLI.Launcher.System;

unsafe readonly struct Win32Window
{
    [SuppressUnmanagedCodeSecurity]
    [return: MarshalAs(UnmanagedType.Bool)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    [DllImport("User32", ExactSpelling = true, SetLastError = true)]
    static extern bool EndTask(nint hWnd, [MarshalAs(UnmanagedType.Bool)] bool fShutDown, [MarshalAs(UnmanagedType.Bool)] bool fForce);

    readonly HWND _handle = HWND.Null;

    internal readonly uint ProcessId = 0;

    Win32Window(HWND handle)
    {
        uint processId = 0;
        GetWindowThreadProcessId(handle, &processId);
        _handle = handle; ProcessId = processId;
    }

    internal void Switch() => SwitchToThisWindow(_handle, true);

    internal void Close()
    {
        EndTask(_handle, false, true);
        if (Win32Process.Open(PROCESS_SYNCHRONIZE, ProcessId) is not { } process) return;
        using (process) process.Wait(INFINITE);
    }

    public static implicit operator HWND(Win32Window @this) => @this._handle;

    public static implicit operator Win32Window(HWND @this) => new(@this);
}