﻿public abstract class Language
{
	internal string OverrideLanguage;

	public string CannotWriteChatLog() { return Get("CannotWriteChatLog"); }
	public string Connecting() { return Get("Connecting"); }
	public string ConnectingProgressKilobytes() { return Get("ConnectingProgressKilobytes"); }
	public string ConnectingProgressPercent() { return Get("ConnectingProgressPercent"); }
	public string DefaultKeys() { return Get("DefaultKeys"); }
	public string Exit() { return Get("Exit"); }
	public string FogDistance() { return Get("FogDistance"); }
	public string FontOption() { return Get("FontOption"); }
	public string FrameRateLagSimulation() { return Get("FrameRateLagSimulation"); }
	public string FrameRateUnlimited() { return Get("FrameRateUnlimited"); }
	public string FrameRateVsync() { return Get("FrameRateVsync"); }
	public string FreemoveNotAllowed() { return Get("FreemoveNotAllowed"); }
	public string GameName() { return Get("GameName"); }
	public string Graphics() { return Get("Graphics"); }
	public string InvalidVersionConnectAnyway() { return Get("InvalidVersionConnectAnyway"); }
	public string KeyBlockInfo() { return Get("KeyBlockInfo"); }
	public string KeyChange() { return Get("KeyChange"); }
	public string KeyChat() { return Get("KeyChat"); }
	public string KeyCraft() { return Get("KeyCraft"); }
	public string KeyFreeMove() { return Get("KeyFreeMove"); }
	public string KeyFullscreen() { return Get("KeyFullscreen"); }
	public string KeyJump() { return Get("KeyJump"); }
	public string KeyMoveBack() { return Get("KeyMoveBack"); }
	public string KeyMoveFoward() { return Get("KeyMoveFoward"); }
	public string KeyMoveLeft() { return Get("KeyMoveLeft"); }
	public string KeyMoveRight() { return Get("KeyMoveRight"); }
	public string KeyMoveSpeed() { return Get("KeyMoveSpeed"); }
	public string KeyPlayersList() { return Get("KeyPlayersList"); }
	public string KeyReloadWeapon() { return Get("KeyReloadWeapon"); }
	public string KeyRespawn() { return Get("KeyRespawn"); }
	public string KeyReverseMinecart() { return Get("KeyReverseMinecart"); }
	public string Keys() { return Get("Keys"); }
	public string KeyScreenshot() { return Get("KeyScreenshot"); }
	public string KeySetSpawnPosition() { return Get("KeySetSpawnPosition"); }
	public string KeyShowMaterialSelector() { return Get("KeyShowMaterialSelector"); }
	public string KeyTeamChat() { return Get("KeyTeamChat"); }
	public string KeyTextEditor() { return Get("KeyTextEditor"); }
	public string KeyThirdPersonCamera() { return Get("KeyThirdPersonCamera"); }
	public string KeyToggleFogDistance() { return Get("KeyToggleFogDistance"); }
	public string KeyUse() { return Get("KeyUse"); }
	public string MoveFree() { return Get("MoveFree"); }
	public string MoveFreeNoclip() { return Get("MoveFreeNoclip"); }
	public string MoveNormal() { return Get("MoveNormal"); }
	public string MoveSpeedNormal() { return Get("MoveSpeedNormal"); }
	public string MoveSpeed() { return Get("MoveSpeed"); }
	public string NoMaterialsForCrafting() { return Get("NoMaterialsForCrafting"); }
	public string Off() { return Get("Off"); }
	public string On() { return Get("On"); }
	public string Options() { return Get("Options"); }
	public string Other() { return Get("Other"); }
	public string PressToUse() { return Get("PressToUse"); }
	public string Respawn() { return Get("Respawn"); }
	public string ReturnToGame() { return Get("ReturnToGame"); }
	public string ReturnToMainMenu() { return Get("ReturnToMainMenu"); }
	public string ReturnToOptionsMenu() { return Get("ReturnToOptionsMenu"); }
	public string ShadowsOption() { return Get("ShadowsOption"); }
	public string SoundOption() { return Get("SoundOption"); }
	public string AutoJumpOption() { return Get("AutoJumpOption"); }
	public string ClientLanguageOption() { return Get("ClientLanguageOption"); }
	public string SpawnPositionSet() { return Get("SpawnPositionSet"); }
	public string SpawnPositionSetTo() { return Get("SpawnPositionSetTo"); }
	public string UseServerTexturesOption() { return Get("UseServerTexturesOption"); }
	public string ViewDistanceOption() { return Get("ViewDistanceOption"); }
	public string OptionSmoothShadows() { return Get("OptionSmoothShadows"); }
	public string OptionFramerate() { return Get("OptionFramerate"); }
	public string OptionResolution() { return Get("OptionResolution"); }
	public string OptionFullscreen() { return Get("OptionFullscreen"); }

	public string ServerCannotWriteLog() { return Get("Server_CannotWriteLogFile"); }
	public string ServerLoadingSavegame() { return Get("Server_LoadingSavegame"); }
	public string ServerCreatingSavegame() { return Get("Server_CreatingSavegame"); }
	public string ServerLoadedSavegame() { return Get("Server_LoadedSavegame"); }
	public string ServerConfigNotFound() { return Get("Server_ConfigNotFound"); }
	public string ServerConfigCorruptBackup() { return Get("Server_ConfigCorruptBackup"); }
	public string ServerConfigCorruptNoBackup() { return Get("Server_ConfigCorruptNoBackup"); }
	public string ServerConfigLoaded() { return Get("Server_ConfigLoaded"); }
	public string ServerClientConfigNotFound() { return Get("Server_ClientConfigNotFound"); }
	public string ServerClientConfigGuestGroupNotFound() { return Get("Server_ClientConfigGuestGroupNotFound"); }
	public string ServerClientConfigRegisteredGroupNotFound() { return Get("Server_ClientConfigRegisteredGroupNotFound"); }
	public string ServerClientConfigLoaded() { return Get("Server_ClientConfigLoaded"); }
	public string ServerInvalidSpawnCoordinates() { return Get("Server_InvalidSpawnCoordinates"); }
	public string ServerProgressDownloadingData() { return Get("Server_ProgressDownloadingData"); }
	public string ServerProgressDownloadingMap() { return Get("Server_ProgressDownloadingMap"); }
	public string ServerProgressGenerating() { return Get("Server_ProgressGenerating"); }
	public string ServerNoChatPrivilege() { return Get("Server_NoChatPrivilege"); }
	public string ServerFillAreaInvalid() { return Get("Server_FillAreaInvalid"); }
	public string ServerFillAreaTooLarge() { return Get("Server_FillAreaTooLarge"); }
	public string ServerNoSpectatorBuild() { return Get("Server_NoSpectatorBuild"); }
	public string ServerNoBuildPrivilege() { return Get("Server_NoBuildPrivilege"); }
	public string ServerNoBuildPermissionHere() { return Get("Server_NoBuildPermissionHere"); }
	public string ServerNoSpectatorUse() { return Get("Server_NoSpectatorUse"); }
	public string ServerNoUsePrivilege() { return Get("Server_NoUsePrivilege"); }
	public string ServerPlayerJoin() { return Get("Server_PlayerJoin"); }
	public string ServerPlayerDisconnect() { return Get("Server_PlayerDisconnect"); }
	public string ServerUsernameBanned() { return Get("Server_UsernameBanned"); }
	public string ServerNoGuests() { return Get("Server_NoGuests"); }
	public string ServerUsernameInvalid() { return Get("Server_UsernameInvalid"); }
	public string ServerPasswordInvalid() { return Get("Server_PasswordInvalid"); }
	public string ServerClientException() { return Get("Server_ClientException"); }
	public string ServerIPBanned() { return Get("Server_IPBanned"); }
	public string ServerTooManyPlayers() { return Get("Server_TooManyPlayers"); }
	public string ServerHTTPServerError() { return Get("Server_HTTPServerError"); }
	public string ServerHTTPServerStarted() { return Get("Server_HTTPServerStarted"); }
	public string ServerHeartbeatSent() { return Get("Server_HeartbeatSent"); }
	public string ServerHeartbeatError() { return Get("Server_HeartbeatError"); }
	public string ServerBanlistLoaded() { return Get("Server_BanlistLoaded"); }
	public string ServerBanlistCorruptNoBackup() { return Get("Server_BanlistCorruptNoBackup"); }
	public string ServerBanlistCorrupt() { return Get("Server_BanlistCorrupt"); }
	public string ServerBanlistNotFound() { return Get("Server_BanlistNotFound"); }
	public string ServerSetupAccept() { return Get("Server_SetupAccept"); }
	public string ServerSetupEnableHTTP() { return Get("Server_SetupEnableHTTP"); }
	public string ServerSetupMaxClients() { return Get("Server_SetupMaxClients"); }
	public string ServerSetupMaxClientsInvalidValue() { return Get("Server_SetupMaxClientsInvalidValue"); }
	public string ServerSetupMaxClientsInvalidInput() { return Get("Server_SetupMaxClientsInvalidInput"); }
	public string ServerSetupPort() { return Get("Server_SetupPort"); }
	public string ServerSetupPortInvalidValue() { return Get("Server_SetupPortInvalidValue"); }
	public string ServerSetupPortInvalidInput() { return Get("Server_SetupPortInvalidInput"); }
	public string ServerSetupWelcomeMessage() { return Get("Server_SetupWelcomeMessage"); }
	public string ServerSetupMOTD() { return Get("Server_SetupMOTD"); }
	public string ServerSetupName() { return Get("Server_SetupName"); }
	public string ServerSetupPublic() { return Get("Server_SetupPublic"); }
	public string ServerSetupQuestion() { return Get("Server_SetupQuestion"); }
	public string ServerSetupFirstStart() { return Get("Server_SetupFirstStart"); }
	public string ServerGameSaved() { return Get("Server_GameSaved"); }
	public string ServerInvalidBackupName() { return Get("Server_InvalidBackupName"); }
	public string ServerMonitorConfigLoaded() { return Get("Server_MonitorConfigLoaded"); }
	public string ServerMonitorConfigNotFound() { return Get("Server_MonitorConfigNotFound"); }
	public string ServerMonitorChatMuted() { return Get("Server_MonitorChatMuted"); }
	public string ServerMonitorChatNotSent() { return Get("Server_MonitorChatNotSent"); }
	public string ServerMonitorBuildingDisabled() { return Get("Server_MonitorBuildingDisabled"); }

	public abstract void LoadTranslations();

	internal void AddEnglish()
	{
		Add("en", "MainMenu_AssetsLoadProgress", "Loading ... {0}%");
		Add("en", "MainMenu_Singleplayer", "Singleplayer");
		Add("en", "MainMenu_Multiplayer", "Multiplayer");
		Add("en", "MainMenu_Quit", "&eQuit");
		Add("en", "MainMenu_ButtonBack", "Back");
		Add("en", "MainMenu_SingleplayerButtonCreate", "Create or Open ...");
		Add("en", "MainMenu_Login", "Login");
		Add("en", "MainMenu_LoginUsername", "Username");
		Add("en", "MainMenu_LoginPassword", "Password");
		Add("en", "MainMenu_LoginRemember", "Remember Me");
		Add("en", "MainMenu_LoginCreateAccount", "Create Account");
		Add("en", "MainMenu_ChoiceYes", "Yes");
		Add("en", "MainMenu_ChoiceNo", "No");
		Add("en", "MainMenu_LoginInvalid", "&4Invalid Username or Password");
		Add("en", "MainMenu_LoginConnecting", "Connecting ...");
		Add("en", "MainMenu_MultiplayerConnect", "Connect");
		Add("en", "MainMenu_MultiplayerConnectIP", "Connect to Server");
		Add("en", "MainMenu_MultiplayerRefresh", "Refresh");
		Add("en", "MainMenu_MultiplayerLoading", "Loading ...");
		Add("en", "MainMenu_MultiplayerLogout", "Logout");
		Add("en", "MainMenu_ConnectToIpConnect", "Connect");
		Add("en", "MainMenu_ConnectToIpIp", "IP");
		Add("en", "MainMenu_ConnectToIpPort", "Port");
		Add("en", "MainMenu_ConnectToIpErrorIp", "&4You must enter a valid address!");
		Add("en", "MainMenu_ConnectToIpErrorPort", "&4You must enter a valid port!");

		Add("en", "CannotWriteChatLog", "Cannot write to chat log file {0}.");
		Add("en", "ChunkUpdates", "Chunk Updates: {0}");
		Add("en", "Connecting", "Connecting ...");
		Add("en", "ConnectingProgressKilobytes", "{0} KB");
		Add("en", "ConnectingProgressPercent", "{0}%");
		Add("en", "DefaultKeys", "Default Keys");
		Add("en", "Exit", "Return to Main Menu");
		Add("en", "FogDistance", "Fog Distance: {0}");
		Add("en", "FontOption", "Font: {0}");
		Add("en", "FrameRateLagSimulation", "Frame Rate: lag simulation.");
		Add("en", "FrameRateUnlimited", "Frame Rate: unlimited.");
		Add("en", "FrameRateVsync", "Frame Rate: vsync.");
		Add("en", "FreemoveNotAllowed", "Flying is not allowed on this server.");
		Add("en", "GameName", "Arcanus");
		Add("en", "Graphics", "Graphics");
		Add("en", "InvalidVersionConnectAnyway", "Invalid Game Version. Local: {0}, Server: {1}. Do you want to connect anyway?");
		Add("en", "KeyBlockInfo", "Block Information");
		Add("en", "KeyChange", "{0}: {1}");
		Add("en", "KeyChat", "Chat");
		Add("en", "KeyCraft", "Craft");
		Add("en", "KeyFreeMove", "Flying");
		Add("en", "KeyFullscreen", "Fullscreen");
		Add("en", "KeyJump", "Jump");
		Add("en", "KeyMoveBack", "Move Back");
		Add("en", "KeyMoveFoward", "Move Foward");
		Add("en", "KeyMoveLeft", "Move Left");
		Add("en", "KeyMoveRight", "Move Right");
		Add("en", "KeyMoveSpeed", "Move Speed");
		Add("en", "KeyPlayersList", "Players List");
		Add("en", "KeyReloadWeapon", "Reload Weapon");
		Add("en", "KeyRespawn", "Respawn");
		Add("en", "KeyReverseMinecart", "Reverse Minecart");
		Add("en", "Keys", "Keys");
		Add("en", "KeyScreenshot", "Screenshot");
		Add("en", "KeySetSpawnPosition", "Set Spawn Position");
		Add("en", "KeyShowMaterialSelector", "Open Inventory");
		Add("en", "KeyTeamChat", "Team Chat");
		Add("en", "KeyTextEditor", "Texteditor");
		Add("en", "KeyThirdPersonCamera", "Third-person Camera");
		Add("en", "KeyToggleFogDistance", "Toggle Fog Distance");
		Add("en", "KeyUse", "Use");
		Add("en", "MoveFree", "Move: Flying");
		Add("en", "MoveFreeNoclip", "Move: Flying, Noclip.");
		Add("en", "MoveNormal", "Move: Normal");
		Add("en", "MoveSpeedNormal", "Move Speed: Normal");
		Add("en", "MoveSpeed", "Move Speed: {0}x");
		Add("en", "NoMaterialsForCrafting", "You must place a material on the crafting table.");
		Add("en", "Off", "OFF");
		Add("en", "On", "ON");
		Add("en", "Options", "Options");
		Add("en", "Other", "Other");
		Add("en", "PressToUse", "(press {0} to use)");
		Add("en", "Respawn", "Respawn");
		Add("en", "ReturnToGame", "Return to Game");
		Add("en", "ReturnToMainMenu", "Back");
		Add("en", "ReturnToOptionsMenu", "Return to Options Menu");
		Add("en", "ShadowsOption", "Shadows: {0}");
		Add("en", "SoundOption", "Sound: {0}");
		Add("en", "AutoJumpOption", "Auto Jump: {0}");
		Add("en", "ClientLanguageOption", "Language: {0}");
		Add("en", "SpawnPositionSet", "Spawn Position Set");
		Add("en", "SpawnPositionSetTo", "Spawn position set to {0}");
		Add("en", "Triangles", "Triangles: {0}");
		Add("en", "UseServerTexturesOption", "Use Server Textures (restart): {0}");
		Add("en", "ViewDistanceOption", "View Distance: {0}");
		Add("en", "OptionSmoothShadows", "Smooth Shadows: {0}");
		Add("en", "OptionFramerate", "Framerate: {0}");
		Add("en", "OptionResolution", "Resolution: {0}");
		Add("en", "OptionFullscreen", "Fullscreen: {0}");
		Add("en", "OptionDarkenSides", "Darken Block Sides: {0}");

		Add("en", "Server_CannotWriteLogFile", "Cannot write to server log file {0}.");
		Add("en", "Server_LoadingSavegame", "Loading Saved Game ...");
		Add("en", "Server_CreatingSavegame", "Creating a New World ...");
		Add("en", "Server_LoadedSavegame", "Saved Game Loaded: ");
		Add("en", "Server_ConfigNotFound", "The server configuration file was not found. Creating a new one ...");
		Add("en", "Server_ConfigCorruptBackup", "The server configuration file is corrupt! A new one will be created. A backup was saved to ServerConfig.txt.old");
		Add("en", "Server_ConfigCorruptNoBackup", "The server configuration file is corrupt! A new one will be created. A backup could NOT be saved.");
		Add("en", "Server_ConfigLoaded", "The server configuration file was loaded.");
		Add("en", "Server_ClientConfigNotFound", "The server client configuration file was not found. Creating a new one ...");
		Add("en", "Server_ClientConfigGuestGroupNotFound", "Default guest group not found!");
		Add("en", "Server_ClientConfigRegisteredGroupNotFound", "Default registered group not found!");
		Add("en", "Server_ClientConfigLoaded", "The server client configuration file was loaded.");
		Add("en", "Server_InvalidSpawnCoordinates", "Invalid default spawn coordinates!");
		Add("en", "Server_ProgressDownloadingData", "Downloading Data ...");
		Add("en", "Server_ProgressGenerating", "Generating World ...");
		Add("en", "Server_ProgressDownloadingMap", "Downloading Map ...");
		Add("en", "Server_NoChatPrivilege", "{0}Insufficient privileges to chat.");
		Add("en", "Server_FillAreaInvalid", "Fillarea is invalid or contains blocks in an area you are not allowed to build in.");
		Add("en", "Server_FillAreaTooLarge", "Fill area is too large.");
		Add("en", "Server_NoSpectatorBuild", "Spectators are not allowed to build.");
		Add("en", "Server_NoBuildPrivilege", "Insufficient privileges to build.");
		Add("en", "Server_NoBuildPermissionHere", "You need permission to build in this section of the world.");
		Add("en", "Server_NoSpectatorUse", "Spectators are not allowed to use blocks.");
		Add("en", "Server_NoUsePrivilege", "Insufficient privileges to use blocks.");
		Add("en", "Server_PlayerJoin", "Player {0} has joined");
		Add("en", "Server_PlayerDisconnect", "Player {0} has left");
		Add("en", "Server_UsernameBanned", "Your username has been banned from this server.{0}");
		Add("en", "Server_NoGuests", "Guests are not allowed on this server. Login or register an account.");
		Add("en", "Server_UsernameInvalid", "Invalid Username (allowed characters: a-z,A-Z,0-9,-,_; max. length: 16)");
		Add("en", "Server_PasswordInvalid", "Invalid Server Password");
		Add("en", "Server_ClientException", "Your client threw an exception at server.");
		Add("en", "Server_IPBanned", "Your IP has been banned from this server.{0}");
		Add("en", "Server_TooManyPlayers", "Too many players! Try to connect later.");
		Add("en", "Server_HTTPServerError", "Cannot start HTTP server on TCP port {0}.");
		Add("en", "Server_HTTPServerStarted", "HTTP server listening on TCP port {0}.");
		Add("en", "Server_HeartbeatSent", "Heartbeat sent.");
		Add("en", "Server_HeartbeatError", "Unable to send heartbeat.");
		Add("en", "Server_BanlistLoaded", "The server banlist was loaded.");
		Add("en", "Server_BanlistCorruptNoBackup", "The server banlist is corrupt! A new one will be created. A backup could NOT be saved.");
		Add("en", "Server_BanlistCorrupt", "The server banlist is corrupt! A new one will be created. A backup was saved to ServerBanlist.txt.old");
		Add("en", "Server_BanlistNotFound", "The server banlist was not found. Creating a new one ...");
		Add("en", "Server_SetupAccept", "y");
		Add("en", "Server_SetupEnableHTTP", "Dou you want to enable the builtin HTTP server? (Y/N)");
		Add("en", "Server_SetupMaxClients", "Enter the maximum number of clients (Default: 16)");
		Add("en", "Server_SetupMaxClientsInvalidValue", "Number may not be negative. Using default (16)");
		Add("en", "Server_SetupMaxClientsInvalidInput", "Invalid input. Using default (16)");
		Add("en", "Server_SetupPort", "Enter the port the server will run on (Default: 25565)");
		Add("en", "Server_SetupPortInvalidValue", "Out of port range. Using default (25565)");
		Add("en", "Server_SetupPortInvalidInput", "Invalid input. Using default (25565)");
		Add("en", "Server_SetupWelcomeMessage", "Enter the welcome message (Default: Welcome to my Arcanus server!)");
		Add("en", "Server_SetupMOTD", "Enter the MOTD (displayed on server list)");
		Add("en", "Server_SetupName", "Enter the server's name (Default: Arcanus Server)");
		Add("en", "Server_SetupPublic", "Do you want the server to be public (visible on the server list)? (Y/N)");
		Add("en", "Server_SetupQuestion", "Would you like to configure the server's settings? (Y/N = Default)");
		Add("en", "Server_SetupFirstStart", "It looks like this is the first time you've started the server.");
		Add("en", "Server_GameSaved", "Game Saved ({0} seconds)");
		Add("en", "Server_InvalidBackupName", "Invalid Backup Filename: ");
		Add("en", "Server_MonitorConfigLoaded", "The server monitor configuration file was loaded.");
		Add("en", "Server_MonitorConfigNotFound", "The server monitor configuration file was not found. Creating a new one ...");
		Add("en", "Server_MonitorChatMuted", "Spam Protection: {0} has been muted for {1} seconds.");
		Add("en", "Server_MonitorChatNotSent", "Spam Protection: Your message has not been sent.");
		Add("en", "Server_MonitorBuildingDisabled", "{0} exceeds set block limit.");
		Add("en", "Server_CommandInvalidArgs", "Invalid arguments. Type /help to see command's usage.");
		Add("en", "Server_CommandInvalidSpawnPosition", "Invalid spawn position.");
		Add("en", "Server_CommandNonexistantPlayer", "{0}Player {1} does not exist.");
		Add("en", "Server_CommandInvalidPosition", "Invalid Position");
		Add("en", "Server_CommandInsufficientPrivileges", "{0}Insufficient privileges to access this command.");
		Add("en", "Server_CommandBackupFailed", "{0}Backup could not be created. Check filename.");
		Add("en", "Server_CommandBackupCreated", "{0}Backup created.");
		Add("en", "Server_CommandException", "Command Exception");
		Add("en", "Server_CommandUnknown", "Unknown Command /");
		Add("en", "Server_CommandPlayerNotFound", "{0}Player {1} not found.");
		Add("en", "Server_CommandPMNoAnswer", "{0}No PM to answer.");
		Add("en", "Server_CommandGroupNotFound", "{0}Group {1} not found.");
		Add("en", "Server_CommandTargetGroupSuperior", "{0}The target group is superior your group.");
		Add("en", "Server_CommandTargetUserSuperior", "{0}Target user is superior or equal.");
		Add("en", "Server_CommandSetGroupTo", "{0}{1} set group of {2} to {3}.");
		Add("en", "Server_CommandOpTargetOffline", "{0}Player {1} is offline. Use /chgrp_offline command.");
		Add("en", "Server_CommandOpTargetOnline", "{0}Player {1} is online. Use /chgrp command.");
		Add("en", "Server_CommandInvalidGroup", "{0}Invalid group.");
		Add("en", "Server_CommandSetOfflineGroupTo", "{0}{1} set group of {2} to {3} (offline).");
		Add("en", "Server_CommandRemoveSuccess", "{0}Client {1} removed from config.");
		Add("en", "Server_CommandRemoveNotFound", "{0}No entry of client {1} found.");
		Add("en", "Server_CommandLoginNoPW", "{0}Group {1} doesn't allow password access.");
		Add("en", "Server_CommandLoginSuccess", "{0}{1} logs in group {2}.");
		Add("en", "Server_CommandLoginInfo", "Type /help see your available privileges.");
		Add("en", "Server_CommandLoginInvalidPassword", "{0}Invalid password.");
		Add("en", "Server_CommandWelcomeChanged", "{0}{1} set new welcome message: {2}");
		Add("en", "Server_CommandKickBanReason", " Reason: ");
		Add("en", "Server_CommandKickMessage", "{0}{1} was kicked by {2}.{3}");
		Add("en", "Server_CommandKickNotification", "You were kicked by an administrator.{0}");
		Add("en", "Server_CommandNonexistantID", "{0}Player ID {1} does not exist.");
		Add("en", "Server_CommandBanMessage", "{0}{1} was permanently banned by {2}.{3}");
		Add("en", "Server_CommandBanNotification", "You were permanently banned by an administrator.{0}");
		Add("en", "Server_CommandIPBanMessage", "{0}{1} was permanently IP banned by {2}.{3}");
		Add("en", "Server_CommandIPBanNotification", "You were permanently IP banned by an administrator.{0}");
		Add("en", "Server_CommandTimeBanMessage", "{0}{1} was banned by {2} for {3} minutes.{4}");
		Add("en", "Server_CommandTimeBanNotification", "You were banned by an administrator for {0} minutes.{1}");
		Add("en", "Server_CommandTimeIPBanMessage", "{0}{1} was IP banned by {2} for {3} minutes.{4}");
		Add("en", "Server_CommandTimeIPBanNotification", "You were IP banned by an administrator for {0} minutes.{1}");
		Add("en", "Server_CommandTimeBanInvalidValue", "Duration must be greater than 0!");
		Add("en", "Server_CommandBanOfflineTargetOnline", "{0}Player {1} is online. Use /ban command.");
		Add("en", "Server_CommandBanOfflineMessage", "{0}{1} (offline) was banned by {2}.{3}");
		Add("en", "Server_CommandUnbanSuccess", "{0}Player {1} unbanned.");
		Add("en", "Server_CommandUnbanIPNotFound", "{0}IP {1} not found.");
		Add("en", "Server_CommandUnbanIPSuccess", "{0}IP {1} unbanned.");
		Add("en", "Server_CommandGiveAll", "{0}Given all blocks to {1}");
		Add("en", "Server_CommandGiveSuccess", "{0}Given {1} {2} to {3}.");
		Add("en", "Server_CommandResetInventorySuccess", "{0}{1}reset inventory of {2}.");
		Add("en", "Server_CommandResetInventoryOfflineSuccess", "{0}{1}reset inventory of {2} (offline).");
		Add("en", "Server_CommandMonstersToggle", "{0} turned monsters {1}.");
		Add("en", "Server_CommandAreaAddIdInUse", "{0}Area ID already in use.");
		Add("en", "Server_CommandAreaAddSuccess", "{0}New area added: {1}");
		Add("en", "Server_CommandAreaDeleteNonexistant", "{0}Area does not exist.");
		Add("en", "Server_CommandAreaDeleteSuccess", "{0}Area deleted.");
		Add("en", "Server_CommandAnnouncementMessage", "{0}Announcement: {1}");
		Add("en", "Server_CommandSetSpawnInvalidCoordinates", "{0}Invalid spawn coordinates.");
		Add("en", "Server_CommandSetSpawnDefaultSuccess", "{0}Default spawn position set to {1},{2},{3}.");
		Add("en", "Server_CommandSetSpawnGroupSuccess", "{0}Spawn position of group {1} set to {2},{3},{4}.");
		Add("en", "Server_CommandSetSpawnPlayerSuccess", "{0}Spawn position of player {1} set to {2},{3},{4}.");
		Add("en", "Server_CommandPrivilegeAddHasAlready", "{0}Player {1} already has privilege {2}.");
		Add("en", "Server_CommandPrivilegeAddSuccess", "{0}New privilege for {1}: {2}");
		Add("en", "Server_CommandPrivilegeRemoveNoPriv", "{0}Player {1} doesn't have privilege {2}.");
		Add("en", "Server_CommandPrivilegeRemoveSuccess", "{0} {1} lost privilege: {2}");
		Add("en", "Server_CommandRestartSuccess", "{0}{1} restarted server.");
		Add("en", "Server_CommandShutdownSuccess", "{0}{1} shut down the server.");
		Add("en", "Server_CommandRestartModsSuccess", "{0}{1} restarted mods.");
		Add("en", "Server_CommandTeleportInvalidCoordinates", "{0}Invalid coordinates.");
		Add("en", "Server_CommandTeleportSuccess", "{0}New Position ({1},{2},{3}).");
		Add("en", "Server_CommandTeleportTargetMessage", "{0}You have been teleported to ({1},{2},{3}) by {4}.");
		Add("en", "Server_CommandTeleportSourceMessage", "{0}You teleported {1} to ({2},{3},{4}).");
		Add("en", "Server_CommandFillLimitDefaultSuccess", "{0}Default fill area limit set to {1}.");
		Add("en", "Server_CommandFillLimitGroupSuccess", "{0}Fill area limit of group {1} set to {2}.");
		Add("en", "Server_CommandFillLimitPlayerSuccess", "{0}Fill area limit of player {1} set to {2}.");
		Add("en", "Server_CommandInvalidType", "Invalid Type");
	}

	public abstract void Add(string language, string id, string translated);
	public abstract void Override(string language, string id, string translated);
	public abstract string Get(string id);
	public abstract string GetUsedLanguage();
	public abstract void NextLanguage();
	public abstract TranslatedString[] AllStrings();
}

public class TranslatedString
{
	internal string id;
	internal string language;
	internal string translated;
}
