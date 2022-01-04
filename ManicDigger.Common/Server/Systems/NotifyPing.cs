﻿using System;
using System.Collections.Generic;

namespace ManicDigger.Server
{
	/// <summary>
	/// Sends ping to all clients and disconnects timed-out players
	/// </summary>
	public class ServerSystemNotifyPing : ServerSystem
	{
		public override void Update(Server server, float dt)
		{
			pingtimer.Update(
				delegate
				{
					if (server.exit.GetExit())
					{
						//Instantly return if server wants to exit
						return;
					}
					List<int> keysToDelete = new List<int>();
					foreach (var k in server.clients)
					{
						// Check if client is alive. Detect half-dropped connections.
						if (!k.Value.Ping.Send((int)server.serverUptime.ElapsedMilliseconds)/*&& k.Value.state == ClientStateOnServer.Playing*/)
						{
							if (k.Value.Ping.Timeout((int)server.serverUptime.ElapsedMilliseconds))
							{
								Console.WriteLine(k.Key + ": ping timeout. Disconnecting...");
								keysToDelete.Add(k.Key);
							}
						}
						else
						{
							server.SendPacket(k.Key, ServerPackets.Ping());
						}
					}

					foreach (int key in keysToDelete)
					{
						server.KillPlayer(key);
					}
				}
			);
		}
		Timer pingtimer = new Timer() { INTERVAL = 1, MaxDeltaTime = 5 };
	}
}
