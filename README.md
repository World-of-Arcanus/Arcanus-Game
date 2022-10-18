![Arcanus](https://raw.githubusercontent.com/World-of-Arcanus/Arcanus-Game/main/data/local/gui/logo-small.png)
============

Arcanus is a 3D sandbox game similar to Minecraft.

Players can explore a procedurally generated world made of blocks where you can build structures, roleplay and battle each other. Please see the [Getting Started](https://github.com/World-of-Arcanus/Arcanus-Game/wiki#getting-started) guide on how to play the game.

![Screenshot](https://raw.githubusercontent.com/World-of-Arcanus/Arcanus-Game/main/docs/images/screenshot.png)

Features
--------

- Open Source

     The game uses a very permissive ISC License and even the media assets are free to modify as you wish.

- Kid Friendly

     There is no blood or bad language, and the fighting is kept cartoon-like as much as possible.

- Singleplayer and Multiplayer

     You can run your own server and have friends connect to it remotely, or just play in a world of your own creation. 

- Customizable Worlds (work in progress)

     There are many settings that can be enabled or disabled to customize your world ...

     - PvP - players can attack each other (on/off)
     - PvE - creatures and/or the environment can attack players (on/off)
     - Ability to add/remove permissions for who can modify your world
     - Ability to choose a starting block set, and you can create your own blocks/sets too
     - Ability to adjust the daylight cycle (normal, always day, always night, always twilight, pitch black)
     - Ability to enable mods, UI themes and music/sound sets

- Portals (work in progress)

     There are no doors in Arcanus, and instead you can use portals to give access to your structures.
     
     You can lock out other players and/or creatures or even link them to other portals across your world.

- Magic System (work in progress)

     A simple magic system is in place to allow you to battle other players.

Future Roadmap
--------------

My long term goals are to add ...

- new blocks for stairs and chests (work in progress)
- the ability to customize your character
- wearable items like armor, clothing and magic rings
- a variety of creatures (some can attack or become pets)
- a sundial block that allows you to fast-forward time
- more animations throughout the game (like improving fire)
- an official modding API and related documentation
- portals that link to completely different worlds

Client Requirements
-------------------------

- Windows 10 x64 or later
- Intel HD Graphics 620 or better

You will also need to have the following [.NET 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) runtimes installed.

- ASP.NET Core Runtime
- .NET Desktop Runtime

The client should work on common Linux x64 distributions like Ubuntu or Debian (by using Mono), but I haven't tested this yet. I do plan on supporting this eventually. However, it will most likely not work on Mac. Please let me know if you do get it working, but otherwise I don't have any plans to support this.

Server Requirements
-------------------------

- Windows 10 x64 or later or
- Windows Server 2016 x64 or later

You will also need to have the following [.NET 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) runtimes installed.

- ASP.NET Core Runtime

Please see the **Build Instructions** for what packages to install for Linux x64.

The recommended hardware is 4GB memory and 2 CPUs, but you should be able to run the server on lower specs.

Build Instructions
-------------------------

- install [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)
- install the [.NET 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) SDK
- install the [.NET 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) ASP.NET Core Runtime
- install the [.NET 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) .NET Desktop Runtime
- clone or download this repository
- open `Arcanus.sln` in the root directory
- this should open **Visual Studio 2022**
- go to **Build > Configuration Manager**
- set **Active Solution Configuration** to **Release** and close
- go to **Build > Build Solution** and wait until it is successful
- open a **Command Prompt** and cd to the root directory
- type `build.bat` and press enter
- the release for the client and server will be in the `build` directory

The server can also be built to run on Linux ...

- right-click **Arcanus.Server** and choose **Publish**
- click the **Publish** button and wait until it is successful
- open a **Command Prompt** and cd to the root directory
- type `build-linux.bat` and press enter
- the release for the server will be in the `build-linux` directory
- upload the contents of this directory to your server
- run the following commands ...

**These instructions assume you are running Ubuntu 20.04.**

**Please change the paths and/or commands to match your distribution!**

- `wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb`
- `sudo dpkg -i packages-microsoft-prod.deb`
- `sudo apt update`
- `sudo apt install apt-transport-https`
- `sudo apt install aspnetcore-runtime-6.0
- `sudo apt install libenet7`
- `sudo ufw allow 25565`
- cd to the directory you uploaded to
- `chmod u+x ArcanusServer`
- `./ArcanusServer` to run it
