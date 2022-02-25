using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace Arcanus.Mods.Fortress
{
	public class Magic : IMod
	{
		public void PreStart(ModManager m)
		{
			m.RequireMod("CoreBlocks");
		}

		public void Start(ModManager manager)
		{
			m = manager;

			m.RegisterOnPlayerJoin(PlayerJoin);
			m.RegisterOnWeaponHit(Hit);
			m.RegisterOnWeaponShot(Shot);
			m.RegisterOnPlayerDeath(OnPlayerDeath);

			m.RegisterTimer(UpdateMedicalKitAmmoPack, 0.1);
			m.RegisterTimer(UpdateRespawnTimer, 1);
		}

		TimeSpan RespawnTime = TimeSpan.FromSeconds(15);
		DateTime CurrentRespawnTime;
		Dictionary<int, Player> players = new Dictionary<int, Player>();

		public class Player
		{
			public int kills;
			public bool isdead;
			public int following = -1;
			public Dictionary<int, int> totalAmmo = new Dictionary<int, int>();
		}

		ModManager m;

		void PlayerJoin(int playerid)
		{
			m.SetPlayerHealth(playerid, 100, 100);

			players[playerid] = new Player();

			Inventory inv = m.GetInventory(playerid);
			foreach (var item in inv.Items.Values)
			{
				if (item != null && item.ItemClass == ItemClass.Block)
				{
					BlockType block = m.GetBlockType(item.BlockId);

					if (block.IsPistol)
					{
						players[playerid].totalAmmo[item.BlockId] = block.AmmoTotal * block.AmmoMagazine;
					}
				}
			}

			m.NotifyAmmo(playerid, players[playerid].totalAmmo);
		}

		void OnPlayerDeath(int player, DeathReason reason, int sourceID)
		{
			string deathMessage = "";
			switch (reason)
			{
				case DeathReason.FallDamage:
					Die(player);
					deathMessage = string.Format("{0}{1} &7died from a fall.", m.GetPlayerName(player));
					break;
				case DeathReason.BlockDamage:
					if (sourceID == m.GetBlockId("Lava"))
					{
						Die(player);
						deathMessage = string.Format("{0}{1} &7thought they could swim in lava.", m.GetPlayerName(player));
					}
					else if (sourceID == m.GetBlockId("Fire"))
					{
						Die(player);
						deathMessage = string.Format("{0}{1} &7thought they could play with fire.", m.GetPlayerName(player));
					}
					else
					{
						Die(player);
						deathMessage = string.Format("{0}{1} &7was defeated by {2}.", m.GetPlayerName(player), m.GetBlockName(sourceID));
					}
					break;
				case DeathReason.Drowning:
					Die(player);
					deathMessage = string.Format("{0}{1} &7tried to breathe under water.", m.GetPlayerName(player));
					break;
				case DeathReason.Explosion:
					//Check if one of the players is dead
					if (players[player].isdead || players[sourceID].isdead)
					{
						break;
					}
					Die(player);
					if (sourceID == player)
					{
						deathMessage = string.Format("{0}{1} &7blew themself up.", m.GetPlayerName(player));
						break;
					}
					players[sourceID].kills = players[sourceID].kills + 1;
					deathMessage = string.Format("{0}{1} &7was blown up by {2}{3}&7.", m.GetPlayerName(player), m.GetPlayerName(sourceID));
					break;
				default:
					Die(player);
					deathMessage = string.Format("{0}{1} &7died.", m.GetPlayerName(player));
					break;
			}
			if (!string.IsNullOrEmpty(deathMessage))
			{
				m.SendMessageToAll(deathMessage);
			}
		}

		void Respawn(int playerid)
		{
			float posx = m.GetPlayerPositionX(playerid);
			float posy = m.GetPlayerPositionY(playerid);
			float posz = m.GetPlayerPositionZ(playerid);
			// posz += 4; // add 4 to spawn in the air
			m.SetPlayerPosition(playerid, posx, posy, posz);
		}

		void Shot(int sourceplayer, int block)
		{
			if (!players[sourceplayer].totalAmmo.ContainsKey(block))
			{
				players[sourceplayer].totalAmmo[block] = 0;
			}

			players[sourceplayer].totalAmmo[block] = players[sourceplayer].totalAmmo[block] - 1;

			m.NotifyAmmo(sourceplayer, players[sourceplayer].totalAmmo);
		}

		void Hit(int sourceplayer, int targetplayer, int block, bool head)
		{
			if (players[targetplayer].isdead || players[sourceplayer].isdead)
			{
				return;
			}

			float x1 = m.GetPlayerPositionX(sourceplayer);
			float y1 = m.GetPlayerPositionY(sourceplayer);
			float z1 = m.GetPlayerPositionZ(sourceplayer);
			float x2 = m.GetPlayerPositionX(targetplayer);
			float y2 = m.GetPlayerPositionY(targetplayer);
			float z2 = m.GetPlayerPositionZ(targetplayer);

			float dx = x1 - x2;
			float dy = y1 - y2;
			float dz = z1 - z2;

			float dist = (float)Math.Sqrt(dx * dx + dy * dy + dz * dz);

			dx = (dx / dist) * 0.1f;
			dy = (dy / dist) * 0.1f;
			dz = (dz / dist) * 0.1f;

			m.SendExplosion(targetplayer, dx, dy, dz, true, m.GetBlockType(block).ExplosionRange, m.GetBlockType(block).ExplosionTime);

			int health = m.GetPlayerHealth(targetplayer);
			int dmghead = 50;
			int dmgbody = 15;

			if (m.GetBlockType(block).DamageHead != 0) { dmghead = (int)m.GetBlockType(block).DamageHead; }
			if (m.GetBlockType(block).DamageBody != 0) { dmgbody = (int)m.GetBlockType(block).DamageBody; }

			health -= head ? dmghead : dmgbody;

			if (health <= 0)
			{
				players[sourceplayer].kills = players[sourceplayer].kills - 2;

				Die(targetplayer);

				m.SendMessageToAll(string.Format("{0} defeats {1}", m.GetPlayerName(sourceplayer), m.GetPlayerName(targetplayer)));
			}
			else
			{
				m.SetPlayerHealth(targetplayer, health, m.GetPlayerMaxHealth(targetplayer));

				HitSound(targetplayer, block, head);
			}
		}

		async Task HitSound(int targetplayer, int block, bool head)
		{
			string hitSound;
			int hitDelay;

			if (head)
			{
				int hitHead = new Random().Next() % m.GetBlockType(block).Sounds.HitHead.Length;
				hitSound = m.GetBlockType(block).Sounds.HitHead[hitHead];
			}
			else
			{
				int hitBody = new Random().Next() % m.GetBlockType(block).Sounds.HitBody.Length;
				hitSound = m.GetBlockType(block).Sounds.HitBody[hitBody];
			}

			// delay the hit sound to make it appear like the hit happens
			// after the shot (even though they occur at the same time)
			switch (m.GetBlockType(block).PistolType)
			{
				case PistolType.Magic:
					hitDelay = 750;
					break;
				default:
					hitDelay = 250;
					break;
			}

			await Task.Delay(hitDelay);

			m.PlaySoundAt((int)m.GetPlayerPositionX(targetplayer),
						  (int)m.GetPlayerPositionY(targetplayer),
						  (int)m.GetPlayerPositionZ(targetplayer),
						  String.Concat(hitSound, ".ogg"));
		}

		void Die(int player)
		{
			m.PlaySoundAt((int)m.GetPlayerPositionX(player),
						  (int)m.GetPlayerPositionY(player),
						  (int)m.GetPlayerPositionZ(player), "death.ogg");

			players[player].isdead = true;

			m.SetPlayerHealth(player, m.GetPlayerMaxHealth(player), m.GetPlayerMaxHealth(player));
			m.FollowPlayer(player, player, true);
			UpdatePlayerModel(player);
		}

		void UpdatePlayerModel(int player)
		{
			string model = "player.txt";

			if (players[player].isdead) {
				model = "playerdead.txt";
			}

			m.SetPlayerModel(player, model, "mineplayer.png");
		}

		void UpdateRespawnTimer()
		{
			int[] allplayers = m.AllPlayers();
			int secondsToRespawn = (int)((CurrentRespawnTime + RespawnTime) - DateTime.UtcNow).TotalSeconds;
			if (secondsToRespawn <= 0)
			{
				for (int i = 0; i < allplayers.Length; i++)
				{
					int p = allplayers[i];
					if (!players.ContainsKey(p))
					{
						//Skip this player as he hasn't joined yet
						continue;
					}
					if (players[p].isdead)
					{
						m.SendMessageToAll(string.Format("{0} respawns", m.GetPlayerName(p)));
						m.SendDialog(p, "RespawnCountdown" + p, null);
						m.FollowPlayer(p, -1, false);
						Respawn(p);
						players[p].isdead = false;
						UpdatePlayerModel(p);
					}
				}
				CurrentRespawnTime = DateTime.UtcNow;
			}
			for (int i = 0; i < allplayers.Length; i++)
			{
				int p = allplayers[i];
				if (!players.ContainsKey(p))
				{
					//Skip this player as he hasn't joined yet
					continue;
				}
				if (players[p].isdead)
				{
					Dialog d = new Dialog();
					d.IsModal = false;
					string text = secondsToRespawn.ToString();
					DialogFont f = new DialogFont("Verdana", 60f, DialogFontStyle.Regular);
					Widget w = Widget.MakeText(text, f, -m.MeasureTextSize(text, f)[0] / 2, -200, Color.Red.ToArgb());
					d.Widgets = new Widget[1];
					d.Widgets[0] = w;
					m.SendDialog(p, "RespawnCountdown" + p, d);
				}
			}
		}

		void UpdateMedicalKitAmmoPack()
		{
			int[] allplayers = m.AllPlayers();

			int medicalkit = m.GetBlockId("MedicalKit");
			int ammopack = m.GetBlockId("AmmoPack");

			foreach (int p in allplayers)
			{
				int px = (int)m.GetPlayerPositionX(p);
				int py = (int)m.GetPlayerPositionY(p);
				int pz = (int)m.GetPlayerPositionZ(p);

				if (m.IsValidPos(px, py, pz))
				{
					int block = m.GetBlock(px, py, pz);

					if (block == medicalkit)
					{
						int health = m.GetPlayerHealth(p);
						int maxhealth = m.GetPlayerMaxHealth(p);

						if (health >= maxhealth)
						{
							continue;
						}

						health += 30;

						if (health > maxhealth)
						{
							health = maxhealth;
						}

						m.SetPlayerHealth(p, health, maxhealth);
						m.SetBlock(px, py, pz, 0);
					}

					if (block == ammopack)
					{
						foreach (var k in new List<int>(players[p].totalAmmo.Keys))
						{
							int ammo = 0;

							if (players[p].totalAmmo.ContainsKey(k))
							{
								ammo = players[p].totalAmmo[k];
							}

							ammo += m.GetBlockType(k).AmmoTotal / 3;

							if (ammo > m.GetBlockType(k).AmmoTotal)
							{
								ammo = m.GetBlockType(k).AmmoTotal;
							}

							players[p].totalAmmo[k] = ammo;
						}

						m.NotifyAmmo(p, players[p].totalAmmo);
						m.SetBlock(px, py, pz, 0);
					}
				}
			}
		}
	}
}
