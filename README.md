![Arcanus](https://raw.githubusercontent.com/World-of-Arcanus/Arcanus-Game/main/data/local/gui/logo-small.png)
============

Arcanus is a 3D sandbox game similar to Minecraft.

Players can explore a procedurally generated world made of blocks where you can build structures, roleplay and battle each other. Please see the [Getting Started](https://github.com/World-of-Arcanus/Arcanus-Game/wiki#getting-started) guide on how to play the game.

![Screenshot](https://raw.githubusercontent.com/World-of-Arcanus/Arcanus-Game/main/docs/images/screenshot.png)

Features
--------

- Open Source

     The game uses a very permissive ISC License and even the media assets are free to modify as you wish.
     
     If you would like to support the project, then please consider joining my Patreon (link coming soon) where you will get access to early releases, nightly binaries and a community server to play on.

- Kid Friendly

     There is no blood or bad language, and the fighting is kept cartoon-like as much as possible.

- Singleplayer and multiplayer

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

     A simple magic system is in place to allow you to battle other players. More to come soon ...

Future Roadmap
--------------

My long term goals are to add ...

- the ability to customize your character
- wearable items like armor, clothing and magic rings
- a variety of creatures (some can attack or become pets)
- a sundial block that allows you to fast-forward time
- more animations throughout the game (like improving fire)
- an official modding API and related documentation
- portals that link to completely different worlds

OS / Graphic Requirements
-------------------------

Windows 10 or later (client and server)

Intel HD Graphics 620 or better (client only)

Arcanus should work on standard Linux distributions like Debian or Ubuntu (by using Mono), but I haven't tested this yet. I do plan on supporting this eventually. However, it will mostly not work on Mac. Please let me know if you do get it working, but otherwise I don't have any plans to support this.

Build Instructions
-------------------------

- install [Visual Studio 2019](https://visualstudio.microsoft.com/vs/older-downloads/#visual-studio-2019-and-other-products)
- install the [.NET Core 3.1](https://dotnet.microsoft.com/en-us/download/dotnet/3.1) SDK
- install the [.NET Core 3.1](https://dotnet.microsoft.com/en-us/download/dotnet/3.1) ASP.NET Core Runtime
- install the [.NET Core 3.1](https://dotnet.microsoft.com/en-us/download/dotnet/3.1) Desktop Runtime
- clone or download this repository
- open `Arcanus.sln` in the root directory
- this should open **Visual Studio 2019**
- go to **Build > Configuration Manager**
- set **Active Solution Configuration** to **Release** and close
- go to **Build > Build Solution**
- open a **Command Prompt** and cd to the root directory
- type `build.bat` and press enter
- the release for the client and server will be in the `build` directory
