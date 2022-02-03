using System;
using System.Collections.Generic;
using System.IO;

namespace Arcanus.Mods
{
	public class RememberPositions : IMod
	{
		ModManager m;
		string filename;

		public PositionStorage positions;

		public void PreStart(ModManager m) { }

		public void Start(ModManager manager)
		{
			m = manager;
			filename = Path.Combine(m.GetGamePath(), m.GetGameName() + ".positions");

			LoadData();

			m.RegisterOnSave(SaveData);
			m.RegisterOnPlayerJoin(OnJoin);
			m.RegisterOnPlayerLeave(OnLeave);
		}

		public void LoadData()
		{
			positions = new PositionStorage();

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
						int[] pos = positions.StringToPos(linesplit[1]);
						positions.Store(linesplit[0], pos[0], pos[1], pos[2], pos[3], pos[4], pos[5]);
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
				Console.WriteLine("[ERROR] positions could not be read!");
			}
		}

		public void SaveData()
		{
			try
			{
				List<string> lines = new List<string>();
				foreach (UserEntry entry in positions.PlayerPositions)
				{
					lines.Add(string.Format("{0};{1}", entry.Name, entry.Position));
				}

				File.WriteAllLines(filename, lines.ToArray());
			}
			catch
			{
				Console.WriteLine("[ERROR] Could not save last player positions");
			}
		}

		public void OnJoin(int player)
		{
			string name = m.GetPlayerName(player);

			if (positions.IsStoredAt(name) != -1)
			{
				int[] pos = positions.Get(name);

				if (pos != null)
				{
					m.SetPlayerPosition(player, pos[0], pos[1], pos[2]);

					if (pos.Length == 6)
                    {
						m.SetPlayerOrientation(player, pos[3], pos[4], pos[5]);
					}

					// Console.WriteLine("[INFO] Position Restored: {0}({1},{2},{3},{4},{5},{6})", name, pos[0], pos[1], pos[2], pos[3], pos[4], pos[5]);
				}
			}
		}

		public void OnLeave(int player)
		{
			if (m.IsBot(player))
			{
				return; // don't store bot positions
			}

			int x = (int)m.GetPlayerPositionX(player);
			int y = (int)m.GetPlayerPositionY(player);
			int z = (int)m.GetPlayerPositionZ(player);
			int heading = (int)m.GetPlayerHeading(player);
			int pitch = (int)m.GetPlayerPitch(player);
			int stance = (int)m.GetPlayerStance(player);

			if (x > 0 && y > 0 && z > 0)
			{
				// make sure the position is inside the map
				if (x < m.GetMapSizeX() && y < m.GetMapSizeY() && z < m.GetMapSizeZ())
				{
					positions.Store(m.GetPlayerName(player), x, y, z, heading, pitch, stance);
				}
			}
		}
	}

	public class PositionStorage
	{
		public List<UserEntry> PlayerPositions { get; set; }

		public PositionStorage()
		{
			this.PlayerPositions = new List<UserEntry>();
		}

		public int IsStoredAt(string player)
		{
			for (int i = 0; i < PlayerPositions.Count; i++)
			{
				if (player.Equals(PlayerPositions[i].Name, StringComparison.InvariantCultureIgnoreCase))
				{
					return i;
				}
			}

			return -1;
		}

		public void Store(string player, int x, int y, int z, int heading, int pitch, int stance)
		{
			if (IsStoredAt(player) != -1)
			{
				Delete(player);
			}

			UserEntry entry = new UserEntry();
			entry.Name = player;
			entry.Position = PosToString(x, y, z, heading, pitch, stance);

			PlayerPositions.Add(entry);

			// Console.WriteLine("[INFO] Position Saved: {0}({1})", entry.Name, entry.Position);
		}

		public void Delete(string player)
		{
			for (int i = 0; i < PlayerPositions.Count; i++)
			{
				if (player.Equals(PlayerPositions[i].Name, StringComparison.InvariantCultureIgnoreCase))
				{
					PlayerPositions.RemoveAt(i);
					return;
				}
			}
		}

		public int[] Get(string player)
		{
			int index = IsStoredAt(player);

			if (index != -1)
			{
				return StringToPos(PlayerPositions[index].Position);
			}

			return null;
		}

		public string PosToString(int x, int y, int z, int heading, int pitch, int stance)
		{
			return string.Format("{0},{1},{2},{3},{4},{5}", x, y, z, heading, pitch, stance);
		}

		public int[] StringToPos(string position)
		{
			try
			{
				string[] split = position.Split(',');
				int[] retval = new int[6];

				retval[0] = int.Parse(split[0]); // x
				retval[1] = int.Parse(split[1]); // y
				retval[2] = int.Parse(split[2]); // z

				retval[3] = (split.Length > 3) ? int.Parse(split[3]) : 0; // heading
				retval[4] = (split.Length > 4) ? int.Parse(split[4]) : (255 / 2); // pitch (straight ahead)
				retval[5] = (split.Length > 5) ? int.Parse(split[5]) : 0; // stance

				return retval;
			}
			catch
			{
				Console.WriteLine("[ERROR] Could not convert '{0}' to coordinates", position);

				return null;
			}
		}
	}

	public class UserEntry
	{
		public string Name { get; set; }
		public string Position { get; set; }
	}
}
