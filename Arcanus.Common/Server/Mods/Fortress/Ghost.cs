using System;
using System.Collections.Generic;

namespace Arcanus.Mods
{
	//for debugging
	public class Ghost : IMod
	{
		public void PreStart(ModManager m) { }

		public void Start(ModManager manager)
		{
			m = manager;
			if (enabled)
			{
				m.RegisterOnLoadWorld(OnLoad);
			}
		}

		void OnLoad()
		{
			m.RegisterTimer(f, 0.1);
			ghost = m.AddBot("Ghost");

			List<ModDelegates.PlayerJoin> onplayerjoin = m.GetOnPlayerJoin();

			for (int i = 0; i < onplayerjoin.Count; i++)
			{
				try
				{
					onplayerjoin[i](ghost);
				}
				catch (Exception ex)
				{
					Console.WriteLine("Mod exception: OnPlayerJoin");
					Console.WriteLine(ex.Message);
					Console.WriteLine(ex.StackTrace);
				}
			}
		}

		bool enabled = true;

		ModManager m;
		int ghost;

		class Pos
		{
			public float x;
			public float y;
			public float z;
			public int heading;
			public int pitch;
		}

		List<Pos> history = new List<Pos>();
		void f()
		{
			int[] clients = m.AllPlayers();
			foreach (int p in clients)
			{
				if (p == ghost)
				{
					continue;
				}
				Pos pos = new Pos();
				pos.x = m.GetPlayerPositionX(p);
				pos.y = m.GetPlayerPositionY(p);
				pos.z = m.GetPlayerPositionZ(p);

				pos.heading = m.GetPlayerHeading(p);
				pos.pitch = m.GetPlayerPitch(p);
				history.Add(pos);
			}
			if (history.Count < 20)
			{
				return;
			}
			Pos p1 = history[0];
			history.RemoveAt(0);
			m.SetPlayerPosition(ghost, p1.x - 5, p1.y, p1.z);
			m.SetPlayerOrientation(ghost, -p1.heading, p1.pitch, 0);
		}
	}
}
