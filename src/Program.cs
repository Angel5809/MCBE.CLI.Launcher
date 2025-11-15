using System;
using static System.StringComparison;
using MCBE.CLI.Launcher.Core;
using MCBE.CLI.Launcher.Modding;
using System.Reflection;

namespace MCBE.CLI.Launcher;

static class Program
{
    static void Main(string[] args)
    {
        string? launch = null;
        var terminate = false;
        var initialized = false;

        if (args.Length <= 0)
        {
            Console.WriteLine(@$"--terminate - Terminate Minecraft: Bedrock Edition, if its already running.
--initialized - Wait for Minecraft: Bedrock Edition to fully initialize when launching.
--launch <path> - Launch & inject the specified dynamic link library into Minecraft: Bedrock Edition.");
            return;
        }

        for (var index = 0; index < args.Length; index++)
        {
            var arg = args[index];
            if (arg.Equals("--terminate", OrdinalIgnoreCase)) terminate = true;
            else if (arg.Equals("--initialized", OrdinalIgnoreCase)) initialized = true;
            else if (arg.Equals("--launch")) if (index + 1 < args.Length) launch = args[index + 1];
        }

        if (terminate) Minecraft.Current.Terminate();
        if (launch is { }) Injector.Launch(initialized, launch);
        else Minecraft.Current.Launch(initialized);
    }
}