# MCBE CLI Launcher
A simple & easy CLI injector for Minecraft: Bedrock Editon meant to aid in development of native mods. 

## Features

- Supports both UWP & GDK builds of Minecraft: Bedrock Edition.

- Integrates well in various pipelines when developing native mods.

## Installation
- [Download](https://github.com/Aetopia/Igneous/releases/latest/download/MCBE.CLI.Launcher.exe) the latest release.
- Start your preferred terminal such as Command Prompt or PowerShell & run `MCBE.CLI.Launcher.exe`.

### Arguments
|Argument|Description|
|-|-|
|`--terminate`|Terminate Minecraft: Bedrock Edition, if its already running.|
|`--initialized`|Wait for Minecraft: Bedrock Edition to fully initialize when launching.|
|`--launch <path>`|Launch & inject the specified dynamic link library into Minecraft: Bedrock Edition.|

### Usage

> [!IMPORTANT]
> - If the game is crashing when loading in a mod then try using the `--initialized` argument.
> - This will have the tool wait the game to be fully initialized before injecting any dynamic link library but might result in delayed injection.

- You can directly invoke the tool via your preferred terminal.

- You can use it in scripts in various pipelines.

    ```cmd
    :: Terminate any running instance of the game.
    MCBE.CLI.Launcher.exe --terminate

    :: Build our native mod using our preferred toolchain.
    Build.cmd

    :: Inject our native mod into a fresh instance of the game.
    MCBE.CLI.Launcher.exe --launch Modification.dll --initialized
    ```

## Build
1. Download the [.NET SDK](https://dotnet.microsoft.com/en-us/download).

2. Run the following command to compile:

    ```cmd
    dotnet publish "src\MCBE.CLI.Launcher.csproj"
    ```