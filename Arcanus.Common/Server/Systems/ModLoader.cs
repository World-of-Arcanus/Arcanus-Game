using Arcanus.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
// using System.CodeDom.Compiler;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Reflection;
using System.Runtime.Loader;
using System.IO;

namespace Arcanus.Server
{
	public class ServerSystemModLoader : ServerSystem
	{
		public ServerSystemModLoader()
		{
			// jintEngine.DisableSecurity();
			// jintEngine.AllowClr = true;
		}

		bool started;
		public override void Update(Server server, float dt)
		{
			if (!started)
			{
				started = true;
				LoadMods(server, false);
			}
		}

		public override bool OnCommand(Server server, int sourceClientId, string command, string argument)
		{
			if (command == "mods")
			{
				RestartMods(server, sourceClientId);
				return true;
			}
			return false;
		}

		public bool RestartMods(Server server, int sourceClientId)
		{
			if (!server.PlayerHasPrivilege(sourceClientId, ServerClientMisc.Privilege.restart))
			{
				server.SendMessage(sourceClientId, string.Format(server.language.Get("Server_CommandInsufficientPrivileges"), server.colorError));
				return false;
			}
			server.SendMessageToAll(string.Format(server.language.Get("Server_CommandRestartModsSuccess"), server.colorImportant, server.GetClient(sourceClientId).ColoredPlayername(server.colorImportant)));
			server.ServerEventLog(string.Format("{0} restarts mods.", server.GetClient(sourceClientId).playername));

			server.modEventHandlers = new ModEventHandlers();
			for (int i = 0; i < server.systemsCount; i++)
			{
				if (server.systems[i] == null) { continue; }
				server.systems[i].OnRestart(server);
			}

			LoadMods(server, true);
			return true;
		}

		void LoadMods(Server server, bool restart)
		{
			server.modManager = new ModManager1();
			var m = server.modManager;
			m.Start(server);
			var scritps = GetScriptSources(server);
			CompileScripts(scritps, restart);
			Start(m, m.required);
		}

		Dictionary<string, string> GetScriptSources(Server server)
		{
			string AppRoot = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
			string[] modpaths = new[] { Path.Combine(AppRoot, Path.Combine(Path.Combine(Path.Combine(Path.Combine(Path.Combine(Path.Combine("..", ".."), ".."), ".."), "Arcanus.Common"), "Server"), "Mods")), Path.Combine(AppRoot, "mods")};

			for (int i = 0; i < modpaths.Length; i++)
			{
				if (File.Exists(Path.Combine(modpaths[i], "current.txt")))
				{
					server.gameMode = File.ReadAllText(Path.Combine(modpaths[i], "current.txt")).Trim();
				}
				else if (Directory.Exists(modpaths[i]))
				{
					try
					{
						File.WriteAllText(Path.Combine(modpaths[i], "current.txt"), server.gameMode);
					}
					catch
					{
					}
				}
				modpaths[i] = Path.Combine(modpaths[i], server.gameMode);
			}
			Dictionary<string, string> scripts = new Dictionary<string, string>();
			foreach (string modpath in modpaths)
			{
				if (!Directory.Exists(modpath))
				{
					continue;
				}
				server.ModPaths.Add(modpath);
				string[] files = Directory.GetFiles(modpath);
				foreach (string s in files)
				{
					if (!GameStorePath.IsValidName(Path.GetFileNameWithoutExtension(s)))
					{
						continue;
					}
					if (!(Path.GetExtension(s).Equals(".cs", StringComparison.InvariantCultureIgnoreCase)
						|| Path.GetExtension(s).Equals(".js", StringComparison.InvariantCultureIgnoreCase)))
					{
						continue;
					}
					string scripttext = File.ReadAllText(s);
					string filename = new FileInfo(s).Name;
					scripts[filename] = scripttext;
				}
			}
			return scripts;
		}

		Jint.Engine jintEngine = new Jint.Engine();
		Dictionary<string, string> javascriptScripts = new Dictionary<string, string>();
		public void CompileScripts(Dictionary<string, string> scripts, bool restart)
		{
			//Use a local temp folder
			DirectoryInfo dirTemp = new DirectoryInfo(Path.Combine(new FileInfo(GetType().Assembly.Location).DirectoryName, "modsdebug"));

			//Prepare temp directory
			if (!dirTemp.Exists)
			{
				Directory.CreateDirectory(dirTemp.FullName);
			}
			else
			{
				try
				{
					//Clear temp files
					foreach (FileInfo f in dirTemp.GetFiles())
					{
						f.Delete();
					}
				}
				catch (Exception ex)
				{
					//meh, maybe next time
				}
			}

			Dictionary<string, string> csharpScripts = new Dictionary<string, string>();
			foreach (var k in scripts)
			{
				if (k.Key.EndsWith(".js"))
				{
					javascriptScripts[k.Key] = k.Value;
				}
				else
				{
					csharpScripts[k.Key] = k.Value;
				}
			}
			if (restart)
			{
				// javascript only
				return;
			}

			SyntaxTree[] csharpScriptsValues = new SyntaxTree[csharpScripts.Values.Count];
			int i = 0;
			foreach (var k in csharpScripts)
			{
				csharpScriptsValues[i++] = CSharpSyntaxTree.ParseText(k.Value);
			}

			string AppCore = Path.GetDirectoryName(System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory());
			string AppRoot = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);

			var compilation = CSharpCompilation.Create("mods")
				.WithOptions(
					new CSharpCompilationOptions(
						OutputKind.DynamicallyLinkedLibrary,
						allowUnsafe: true
					)
				)
				.AddReferences(
					MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
					MetadataReference.CreateFromFile(typeof(Color).Assembly.Location),
					MetadataReference.CreateFromFile(typeof(Component).Assembly.Location),
					MetadataReference.CreateFromFile(typeof(Console).Assembly.Location),
					MetadataReference.CreateFromFile(typeof(File).Assembly.Location),
					MetadataReference.CreateFromFile(typeof(Process).Assembly.Location),
					MetadataReference.CreateFromFile(typeof(Stack<Vector3i[]>).Assembly.Location),
					MetadataReference.CreateFromFile(typeof(Stopwatch).Assembly.Location),
					MetadataReference.CreateFromFile(typeof(Uri).Assembly.Location),
					MetadataReference.CreateFromFile(Path.Combine(AppCore, "System.dll")),
					MetadataReference.CreateFromFile(Path.Combine(AppCore, "System.Drawing.dll")),
					MetadataReference.CreateFromFile(Path.Combine(AppCore, "System.Runtime.dll")),
					MetadataReference.CreateFromFile(Path.Combine(AppCore, "System.Xml.dll")),
					MetadataReference.CreateFromFile(Path.Combine(AppCore, "mscorlib.dll")),
					MetadataReference.CreateFromFile(Path.Combine(AppCore, "netstandard.dll")),
					MetadataReference.CreateFromFile(Path.Combine(AppRoot, "Arcanus.ScriptingApi.dll")),
					MetadataReference.CreateFromFile(Path.Combine(AppRoot, "LibNoise.dll")),
					MetadataReference.CreateFromFile(Path.Combine(AppRoot, "protobuf-net.dll"))
				)
				.AddSyntaxTrees(
					csharpScriptsValues
				);

// #if !DEBUG
			using (var memoryStream = new MemoryStream())
			{
				var emitResult = compilation.Emit(memoryStream);

				if (emitResult.Success)
				{
					memoryStream.Seek(0, SeekOrigin.Begin);

					var context = new CollectibleAssemblyLoadContext();
					var assembly = context.LoadFromStream(memoryStream);

					foreach (Type t in assembly.GetTypes())
					{
						if (typeof(IMod).IsAssignableFrom(t))
						{
							mods[t.Name] = (IMod) assembly.CreateInstance(t.FullName);
							Console.WriteLine("Loaded mod: {0}", t.Name);
						}
					}
				}
			}
/*
#else
			string fileName = Path.Combine(AppRoot, "Mods.dll");
			var emitResult = compilation.Emit(fileName);

			if (emitResult.Success)
			{
				var context = new CollectibleAssemblyLoadContext();
				var assembly = context.LoadFromAssemblyPath(fileName);

				foreach (Type t in assembly.GetTypes())
				{
					if (typeof(IMod).IsAssignableFrom(t))
					{
						mods[t.Name] = (IMod) assembly.CreateInstance(t.FullName);
						Console.WriteLine("Loaded mod: {0}", t.Name);
					}
				}
			}
#endif
*/
		}

		Dictionary<string, IMod> mods = new Dictionary<string, IMod>();
		Dictionary<string, string[]> modRequirements = new Dictionary<string, string[]>();
		Dictionary<string, bool> loaded = new Dictionary<string, bool>();

		public void Start(ModManager m, List<string> currentRequires)
		{
			/*
            foreach (var mod in mods)
            {
                mod.Start(m);
            }
            */

			modRequirements.Clear();
			loaded.Clear();

			foreach (var k in mods)
			{
				k.Value.PreStart(m);
				modRequirements[k.Key] = currentRequires.ToArray();
				currentRequires.Clear();
			}
			foreach (var k in mods)
			{
				StartMod(k.Key, k.Value, m);
			}

			StartJsMods(m);
		}

		void StartJsMods(ModManager m)
		{
			jintEngine.SetValue("m", m);
			// TODO: javascript mod requirements
			foreach (var k in javascriptScripts)
			{
				try
				{
					jintEngine.Execute(k.Value);
					Console.WriteLine("Loaded mod: {0}", k.Key);
				}
				catch
				{
					Console.WriteLine("Error in mod: {0}", k.Key);
				}
			}
		}

		void StartMod(string name, IMod mod, ModManager m)
		{
			if (loaded.ContainsKey(name))
			{
				return;
			}
			if (modRequirements.ContainsKey(name))
			{
				foreach (string required_name in modRequirements[name])
				{
					if (!mods.ContainsKey(required_name))
					{
						Console.WriteLine(string.Format("[Mod error] Can't load mod {0} because its dependency {1} couldn't be loaded.", name, required_name));
					}
					StartMod(required_name, mods[required_name], m);
				}
			}
			mod.Start(m);
			loaded[name] = true;
		}
	}

	public class CollectibleAssemblyLoadContext : AssemblyLoadContext
	{
		public CollectibleAssemblyLoadContext() : base(isCollectible: true)
		{ }

		protected override Assembly Load(AssemblyName assemblyName)
		{
			return null;
		}
	}
}
