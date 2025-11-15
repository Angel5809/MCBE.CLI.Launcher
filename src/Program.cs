using System;
using static System.StringComparison;
using MCBE.CLI.Launcher.Core;
using MCBE.CLI.Launcher.Modding;
using System.Reflection;

namespace MCBE.CLI.Launcher;

static class Program
{
    static Program() => AppDomain.CurrentDomain.UnhandledException += (sender, args) => { Console.WriteLine(args.ExceptionObject); Environment.Exit(0); };

    static void Main(string[] args)
    {
        string? path = null;
        bool launch = false, terminate = false, initialized = false;

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

            if (arg.Equals("--terminate", OrdinalIgnoreCase))
                terminate = true;

            else if (arg.Equals("--initialized", OrdinalIgnoreCase))
                initialized = true;

            else if (arg.Equals("--launch", OrdinalIgnoreCase))
            {
                launch = true;
                if (index + 1 < args.Length) path = args[index + 1];
            }
        }

        if (terminate)
            Minecraft.Current.Terminate();

        if (launch && path is { })
        {
            if (Injector.Launch(initialized, path) is not { } processId) return;
            Environment.ExitCode = (int)processId;
            return;
        }

        else if (launch)
        {
            if (Minecraft.Current.Launch(initialized) is not { } processId) return;
            Environment.ExitCode = (int)processId;
            return;
        }
    }
}