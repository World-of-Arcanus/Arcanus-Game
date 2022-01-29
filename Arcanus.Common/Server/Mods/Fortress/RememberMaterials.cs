using System;
using System.Collections.Generic;
using System.IO;

namespace Arcanus.Mods
{
	public class RememberMaterials : IMod
	{
		ModManager m;

		string filename = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) +
			Path.DirectorySeparatorChar + "UserData" + Path.DirectorySeparatorChar + "StoredMaterials.txt";

		public MaterialStorage materials;

		public void PreStart(ModManager m) { }

		public void Start(ModManager manager)
		{
			m = manager;
			LoadData();

			m.RegisterOnSave(SaveData);
			m.RegisterOnPlayerJoin(OnJoin);
			m.RegisterOnPlayerLeave(OnLeave);
		}

		public void LoadData()
		{
			materials = new MaterialStorage();

			if (!File.Exists(filename))
			{
				return;
			}

			try
			{
				string[] lines = File.ReadAllLines(filename);

				for (int i = 0; i < lines.Length; i++)
				{
					string[] linesplit = lines[i].Split(';');

					try
					{
						Item[] mat = materials.StringToMat(linesplit[1]);
						materials.Store(linesplit[0], mat);
					}
					catch
					{
						// skip the line when reading fails
						Console.WriteLine("[WARNING] Skipping invalid entry on line {0}.", i + 1);
					}
				}
			}
			catch
			{
				Console.WriteLine("[ERROR] StoredMaterials.txt could not be read!");
			}
		}

		public void SaveData()
		{
			try
			{
				List<string> lines = new List<string>();
				foreach (UserEntryMat entry in materials.PlayerMaterials)
				{
					lines.Add(string.Format("{0};{1}", entry.Name, entry.Materials));
				}

				File.WriteAllLines(filename, lines.ToArray());
			}
			catch
			{
				Console.WriteLine("[ERROR] Could not save last player materials");
			}
		}

		public void OnJoin(int player)
		{
			string name = m.GetPlayerName(player);

			if (materials.IsStoredAt(name) != -1)
			{
				Item[] mat = materials.Get(name);

				if (mat != null)
				{
					m.GetInventory(player).RightHand = mat;
					m.NotifyInventory(player);
				}
			}
		}

		public void OnLeave(int player)
		{
			if (m.IsBot(player))
			{
				return; // don't store bot materials
			}

			Item[] mat = m.GetInventory(player).RightHand;
			materials.Store(m.GetPlayerName(player), mat);
		}
	}

	public class MaterialStorage
	{
		public List<UserEntryMat> PlayerMaterials { get; set; }

		public MaterialStorage()
		{
			this.PlayerMaterials = new List<UserEntryMat>();
		}

		public int IsStoredAt(string player)
		{
			for (int i = 0; i < PlayerMaterials.Count; i++)
			{
				if (player.Equals(PlayerMaterials[i].Name, StringComparison.InvariantCultureIgnoreCase))
				{
					return i;
				}
			}

			return -1;
		}

		public void Store(string player, Item[] materials)
		{
			if (IsStoredAt(player) != -1)
			{
				Delete(player);
			}

			UserEntryMat entry = new UserEntryMat();
			entry.Name = player;
			entry.Materials = MatToString(materials);

			PlayerMaterials.Add(entry);

		}

		public void Delete(string player)
		{
			for (int i = 0; i < PlayerMaterials.Count; i++)
			{
				if (player.Equals(PlayerMaterials[i].Name, StringComparison.InvariantCultureIgnoreCase))
				{
					PlayerMaterials.RemoveAt(i);
					return;
				}
			}
		}

		public Item[] Get(string player)
		{
			int index = IsStoredAt(player);

			if (index != -1)
			{
				return StringToMat(PlayerMaterials[index].Materials);
			}

			return null;
		}

		public string MatToString(Item[] materials)
		{
			string retval = "";

			for (int i = 0; i < materials.Length; i++)
			{
				Item mat = materials[i];

				if (mat != null)
                {
					string item = mat.ItemClass.ToString();
					item += "|" + mat.ItemId;
					item += "|" + mat.BlockId;
					item += "|" + mat.BlockCount;

					retval += item;
				}

				if (i < materials.Length - 1)
                {
					retval += ",";
                }
			}

			return retval;
		}

		public Item[] StringToMat(string material)
		{
			try
			{
				string[] split = material.Split(',');
				Item[] retval = new Item[split.Length];

				for (int i = 0; i < split.Length; i++)
                {
					Item item = new Item();

					if (split[i] != null)
                    {
						string[] mat = split[i].Split('|');

						if (mat != null)
						{
							try
                            {
								item.ItemClass = (ItemClass)Enum.Parse(typeof(ItemClass), mat[0]);
							}
							catch(Exception e)
							{
								item.ItemClass = ItemClass.Block;
							}

							item.ItemId = (mat.Length > 1) ? mat[1] : "";
							item.BlockId = (mat.Length > 2) ? int.Parse(mat[2]) : 0;
							item.BlockCount = (mat.Length > 3) ? int.Parse(mat[3]) : 0;
						}
					}

					retval[i] = item;
				}

				return retval;
			}
			catch
			{
				Console.WriteLine("[ERROR] Could not convert '{0}' to materials", material);

				return null;
			}
		}
	}

	public class UserEntryMat
	{
		public string Name { get; set; }
		public string Materials { get; set; }
	}
}
