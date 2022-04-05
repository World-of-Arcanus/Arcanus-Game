using Arcanus.Common;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Arcanus.Server
{
	public class ServerSystemLoadConfig : ServerSystem
	{
		public override void Update(Server server, float dt)
		{
			if (!loaded)
			{
				loaded = true;
				LoadConfig(server);
				server.OnConfigLoaded();
			}
			if (server.configNeedsSaving)
			{
				server.configNeedsSaving = false;
				SaveConfig(server);
			}
		}
		bool loaded;
		public void LoadConfig(Server server)
		{
			string filename = server.GetSaveFilename().Replace(".arcanus", ".server");

			if (!File.Exists(filename))
			{
				Console.WriteLine(server.language.ServerConfigNotFound());
				SaveConfig(server);
				return;
			}
			try
			{
				using (TextReader textReader = new StreamReader(filename))
				{
					XmlSerializer deserializer = new XmlSerializer(typeof(ServerConfig), typeof(ServerConfig).GetNestedTypes());
					server.config = (ServerConfig)deserializer.Deserialize(textReader);
					textReader.Close();
				}
			}
			catch //This if for the original format
			{
				try
				{
					using (Stream s = new MemoryStream(File.ReadAllBytes(filename)))
					{
						server.config = new ServerConfig();
						StreamReader sr = new StreamReader(s);
						XmlDocument d = new XmlDocument();
						d.Load(sr);
						server.config.Format = int.Parse(XmlTool.XmlVal(d, "/ArcanusServerConfig/Format"));
						server.config.Name = XmlTool.XmlVal(d, "/ArcanusServerConfig/Name");
						server.config.Motd = XmlTool.XmlVal(d, "/ArcanusServerConfig/Motd");
						server.config.Port = int.Parse(XmlTool.XmlVal(d, "/ArcanusServerConfig/Port"));
						string maxclients = XmlTool.XmlVal(d, "/ArcanusServerConfig/MaxClients");
						if (maxclients != null)
						{
							server.config.MaxClients = int.Parse(maxclients);
						}
						string key = XmlTool.XmlVal(d, "/ArcanusServerConfig/Key");
						if (key != null)
						{
							server.config.Key = key;
						}
						server.config.IsCreative = Misc.ReadBool(XmlTool.XmlVal(d, "/ArcanusServerConfig/Creative"));
						server.config.Public = Misc.ReadBool(XmlTool.XmlVal(d, "/ArcanusServerConfig/Public"));
						server.config.AllowGuests = Misc.ReadBool(XmlTool.XmlVal(d, "/ArcanusServerConfig/AllowGuests"));
						if (XmlTool.XmlVal(d, "/ArcanusServerConfig/MapSizeX") != null)
						{
							server.config.MapSizeX = int.Parse(XmlTool.XmlVal(d, "/ArcanusServerConfig/MapSizeX"));
							server.config.MapSizeY = int.Parse(XmlTool.XmlVal(d, "/ArcanusServerConfig/MapSizeY"));
							server.config.MapSizeZ = int.Parse(XmlTool.XmlVal(d, "/ArcanusServerConfig/MapSizeZ"));
						}
						server.config.BuildLogging = bool.Parse(XmlTool.XmlVal(d, "/ArcanusServerConfig/BuildLogging"));
						server.config.ServerEventLogging = bool.Parse(XmlTool.XmlVal(d, "/ArcanusServerConfig/ServerEventLogging"));
						server.config.ChatLogging = bool.Parse(XmlTool.XmlVal(d, "/ArcanusServerConfig/ChatLogging"));
						server.config.AllowScripting = bool.Parse(XmlTool.XmlVal(d, "/ArcanusServerConfig/AllowScripting"));
						server.config.ServerMonitor = bool.Parse(XmlTool.XmlVal(d, "/ArcanusServerConfig/ServerMonitor"));
						server.config.ClientConnectionTimeout = int.Parse(XmlTool.XmlVal(d, "/ArcanusServerConfig/ClientConnectionTimeout"));
						server.config.ClientPlayingTimeout = int.Parse(XmlTool.XmlVal(d, "/ArcanusServerConfig/ClientPlayingTimeout"));
					}
					//Save with new version.
					SaveConfig(server);
				}
				catch
				{
					//ServerConfig is really messed up. Backup a copy, then create a new one.
					try
					{
						File.Copy(filename, filename + ".old");
						Console.WriteLine(server.language.ServerConfigCorruptBackup());
					}
					catch
					{
						Console.WriteLine(server.language.ServerConfigCorruptNoBackup());
					}
					server.config = null;
					SaveConfig(server);
				}
			}
			server.language.OverrideLanguage = server.config.ServerLanguage;  //Switch to user-defined language.
			Console.WriteLine(server.language.ServerConfigLoaded());
		}

		public void SaveConfig(Server server)
		{
			string filename = server.GetSaveFilename().Replace(".arcanus", ".server");

			//Verify that we have a directory to place the file into.
			if (!Directory.Exists(GameStorePath.gamepathsaves))
			{
				Directory.CreateDirectory(GameStorePath.gamepathsaves);
			}

			XmlSerializer serializer = new XmlSerializer(typeof(ServerConfig), typeof(ServerConfig).GetNestedTypes());
			TextWriter textWriter = new StreamWriter(filename);

			//Check to see if config has been initialized
			if (server.config == null)
			{
				server.config = new ServerConfig();
				//Set default language to user's locale
				server.config.ServerLanguage = System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
				//Ask for config parameters the first time the server is started
				string line;
				bool wantsconfig = false;
				// Console.WriteLine(server.language.ServerSetupFirstStart());
				Console.WriteLine(server.language.ServerSetupQuestion());
				line = Console.ReadLine();
				if (!string.IsNullOrEmpty(line))
				{
					if (line.Equals(server.language.ServerSetupAccept(), StringComparison.InvariantCultureIgnoreCase))
						wantsconfig = true;
					else
						wantsconfig = false;
				}
				//Only ask these questions if user wants to
				if (wantsconfig)
				{
					// disabled for now
					// Console.WriteLine(server.language.ServerSetupPublic());
					// line = Console.ReadLine();
					// if (!string.IsNullOrEmpty(line))
					// {
					// 	bool choice;
					// 	if (line.Equals(server.language.ServerSetupAccept(), StringComparison.InvariantCultureIgnoreCase))
					// 		choice = true;
					// 	else
					// 		choice = false;
					// 	server.config.Public = choice;
					// }
					Console.WriteLine(server.language.ServerSetupName());
					line = Console.ReadLine();
					if (!string.IsNullOrEmpty(line))
					{
						server.config.Name = line;
					}
					// disabled for now
					// Console.WriteLine(server.language.ServerSetupMOTD());
					// line = Console.ReadLine();
					// if (!string.IsNullOrEmpty(line))
					// {
					// 	server.config.Motd = line;
					// }
					Console.WriteLine(server.language.ServerSetupWelcomeMessage());
					line = Console.ReadLine();
					if (!string.IsNullOrEmpty(line))
					{
						server.config.WelcomeMessage = line;
					}
					Console.WriteLine(server.language.ServerSetupPort());
					line = Console.ReadLine();
					if (!string.IsNullOrEmpty(line))
					{
						int port;
						try
						{
							port = int.Parse(line);
							if (port > 0 && port <= 65565)
							{
								server.config.Port = port;
							}
							else
							{
								Console.WriteLine(server.language.ServerSetupPortInvalidValue());
							}
						}
						catch
						{
							Console.WriteLine(server.language.ServerSetupPortInvalidInput());
						}
					}
					Console.WriteLine(server.language.ServerSetupMaxClients());
					line = Console.ReadLine();
					if (!string.IsNullOrEmpty(line))
					{
						int players;
						try
						{
							players = int.Parse(line);
							if (players > 0)
							{
								server.config.MaxClients = players;
							}
							else
							{
								Console.WriteLine(server.language.ServerSetupMaxClientsInvalidValue());
							}
						}
						catch
						{
							Console.WriteLine(server.language.ServerSetupMaxClientsInvalidInput());
						}
					}
					// disabled for now
					// Console.WriteLine(server.language.ServerSetupEnableHTTP());
					// line = Console.ReadLine();
					// if (!string.IsNullOrEmpty(line))
					// {
					// 	bool choice;
					// 	if (line.Equals(server.language.ServerSetupAccept(), StringComparison.InvariantCultureIgnoreCase))
					// 		choice = true;
					// 	else
					// 		choice = false;
					// 	server.config.EnableHTTPServer = choice;
					// }
				}
			}

			if (server.config.Areas.Count == 0)
			{
				server.config.Areas = ServerConfigMisc.getDefaultAreas();
			}

			if (server.configOverride != null)
			{
				server.config.PvP = server.configOverride.PvP;
				server.config.PvE = server.configOverride.PvE;
			}

			// serialize the ServerConfig class to XML
			serializer.Serialize(textWriter, server.config);
			textWriter.Close();
		}
	}
}
