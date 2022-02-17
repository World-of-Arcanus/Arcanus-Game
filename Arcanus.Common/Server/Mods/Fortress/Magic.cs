using System;
using System.Collections.Generic;

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

			m.RegisterTimer(UpdateMedicalKitAmmoPack, 0.1);
		}

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
			for (int i = 0; i < 10; i++)
			{
				Item item = inv.RightHand[i];
				if (item != null && item.ItemClass == ItemClass.Block)
				{
					BlockType block = m.GetBlockType(item.BlockId);
					if (block.IsPistol)
					{
						players[playerid].totalAmmo[item.BlockId] = block.AmmoTotal;
					}
				}
			}

			m.NotifyAmmo(playerid, players[playerid].totalAmmo);
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

				m.SendMessageToAll(string.Format("{0} kills {1}", m.GetPlayerName(sourceplayer), m.GetPlayerName(targetplayer)));
			}
			else
			{
				m.SetPlayerHealth(targetplayer, health, m.GetPlayerMaxHealth(targetplayer));

				m.PlaySoundAt((int)m.GetPlayerPositionX(targetplayer),
							  (int)m.GetPlayerPositionY(targetplayer),
							  (int)m.GetPlayerPositionZ(targetplayer), "grunt1.ogg");
			}
		}

		void Die(int player)
		{
			m.PlaySoundAt((int)m.GetPlayerPositionX(player),
						  (int)m.GetPlayerPositionY(player),
						  (int)m.GetPlayerPositionZ(player), "death.ogg");

			players[player].isdead = true;

			m.SetPlayerHealth(player, m.GetPlayerMaxHealth(player), m.GetPlayerMaxHealth(player));
			m.FollowPlayer(player, player, true);
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
