public class ModPicking : ClientMod
{
	public ModPicking()
	{
		unproject = new Unproject();
		tempViewport = new float[4];
		tempRay = new float[4];
		tempRayStartPoint = new float[4];
		fillarea = new DictionaryVector3Float();
		lastAmmoCount = 0;
	}

	public override void OnBeforeNewFrameDraw3d(Game game, float deltaTime)
	{
		if (game.guistate == GuiState.Normal)
		{
			UpdatePicking(game);
		}
	}

	public override void OnMouseUp(Game game, MouseEventArgs args)
	{
		if (game.guistate == GuiState.Normal)
		{
			UpdatePicking(game);
		}
	}

	public override void OnMouseDown(Game game, MouseEventArgs args)
	{
		if (game.guistate == GuiState.Normal)
		{
			UpdatePicking(game);
			UpdateEntityHit(game);
		}
	}

	internal void UpdatePicking(Game game)
	{
		if (game.FollowId() != null)
		{
			game.SelectedBlockPositionX = 0 - 1;
			game.SelectedBlockPositionY = 0 - 1;
			game.SelectedBlockPositionZ = 0 - 1;
			return;
		}
		NextBullet(game, 0);
	}

	internal int lastAmmoCount;

	internal void NextBullet(Game game, int bulletsshot)
	{
		float one = 1;
		bool left = game.mouseLeft;
		bool middle = game.mouseMiddle;
		bool right = game.mouseRight;

		bool IsNextShot = bulletsshot != 0;

		if (!game.leftpressedpicking)
		{
			if (game.mouseleftclick)
			{
				game.leftpressedpicking = true;
			}
			else
			{
				left = false;
			}
		}
		else
		{
			// mouse up
			if (game.mouseleftdeclick)
			{
				if (game.currentAttackedBlock != null)
				{
					int blockX = game.currentAttackedBlock.X;
					int blockY = game.currentAttackedBlock.Z;
					int blockZ = game.currentAttackedBlock.Y;

					// restore the block's health on mouse up
					// blocks can only be destroyed by holding the mouse down
					// this may need to change when we integrate War mode
					game.blockHealth.Remove(blockX, blockZ, blockY);
				}

				game.leftpressedpicking = false;
				left = false;
			}
		}
		if (!left)
		{
			game.currentAttackedBlock = null;
		}

		Packet_Item item = game.d_Inventory.RightHand[game.ActiveMaterial];
		Packet_BlockType itemBlock = game.blocktypes[item.BlockId];
		bool ispistol = (item != null && itemBlock.IsPistol);
		bool ispistolshoot = ispistol && left;
		bool isgrenade = ispistol && itemBlock.PistolType == Packet_PistolTypeEnum.Grenade;
		if (ispistol && isgrenade)
		{
			ispistolshoot = game.mouseleftdeclick;
		}

		// grenade cooking - TODO: fix instant explosion when closing ESC menu
		if (game.mouseleftclick)
		{
			game.grenadecookingstartMilliseconds = game.platform.TimeMillisecondsFromStart();
		}

		float wait = ((one * (game.platform.TimeMillisecondsFromStart() - game.grenadecookingstartMilliseconds)) / 1000);
		if (isgrenade && left)
		{
			if (wait >= game.grenadetime && isgrenade && game.grenadecookingstartMilliseconds != 0)
			{
				ispistolshoot = true;
				game.mouseleftdeclick = true;
			}
			else
			{
				return;
			}
		}
		else
		{
			game.grenadecookingstartMilliseconds = 0;
		}

		if (ispistol && game.mouserightclick && (game.platform.TimeMillisecondsFromStart() - game.lastironsightschangeMilliseconds) >= 500)
		{
			game.IronSights = !game.IronSights;
			game.lastironsightschangeMilliseconds = game.platform.TimeMillisecondsFromStart();
		}

		IntRef pick2count = new IntRef();
		Line3D pick = new Line3D();
		GetPickingLine(game, pick, ispistolshoot);
		BlockPosSide[] pick2 = game.Pick(game.s, pick, pick2count);

		if (left)
		{
			game.handSetAttackDestroy = true;
		}
		else if (right)
		{
			game.handSetAttackBuild = true;
		}

		if (game.overheadcamera && pick2count.value > 0 && left)
		{
			//if not picked any object, and mouse button is pressed, then walk to destination.
			if (game.Follow == null)
			{
				//Only walk to destination when not following someone
				game.playerdestination = Vector3Ref.Create(pick2[0].blockPos[0], pick2[0].blockPos[1] + 1, pick2[0].blockPos[2]);
			}
		}
		bool pickdistanceok = (pick2count.value > 0); //&& (!ispistol);
		if (pickdistanceok)
		{
			if (game.Dist(pick2[0].blockPos[0] + one / 2, pick2[0].blockPos[1] + one / 2, pick2[0].blockPos[2] + one / 2,
				pick.Start[0], pick.Start[1], pick.Start[2]) > CurrentPickDistance(game))
			{
				pickdistanceok = false;
			}
		}
		bool playertileempty = game.IsTileEmptyForPhysics(
				  game.platform.FloatToInt(game.player.position.x),
				  game.platform.FloatToInt(game.player.position.z),
				  game.platform.FloatToInt(game.player.position.y + (one / 2)));
		bool playertileemptyclose = game.IsTileEmptyForPhysicsClose(
				  game.platform.FloatToInt(game.player.position.x),
				  game.platform.FloatToInt(game.player.position.z),
				  game.platform.FloatToInt(game.player.position.y + (one / 2)));
		BlockPosSide pick0 = new BlockPosSide();
		if (pick2count.value > 0 &&
			((pickdistanceok && (playertileempty || (playertileemptyclose)))
			|| game.overheadcamera)
			)
		{
			game.SelectedBlockPositionX = game.platform.FloatToInt(pick2[0].Current()[0]);
			game.SelectedBlockPositionY = game.platform.FloatToInt(pick2[0].Current()[1]);
			game.SelectedBlockPositionZ = game.platform.FloatToInt(pick2[0].Current()[2]);
			pick0 = pick2[0];
		}
		else
		{
			game.SelectedBlockPositionX = -1;
			game.SelectedBlockPositionY = -1;
			game.SelectedBlockPositionZ = -1;
			pick0.blockPos = new float[3];
			pick0.blockPos[0] = -1;
			pick0.blockPos[1] = -1;
			pick0.blockPos[2] = -1;
		}
		PickEntity(game, pick, pick2, pick2count);
		if (game.cameratype == CameraType.Fpp || game.cameratype == CameraType.Tpp)
		{
			int ntileX = game.platform.FloatToInt(pick0.Current()[0]);
			int ntileY = game.platform.FloatToInt(pick0.Current()[1]);
			int ntileZ = game.platform.FloatToInt(pick0.Current()[2]);
			if (game.IsUsableBlock(game.map.GetBlock(ntileX, ntileZ, ntileY)))
			{
				game.currentAttackedBlock = Vector3IntRef.Create(ntileX, ntileZ, ntileY);
			}
		}
		if (game.GetFreeMouse())
		{
			if (pick2count.value > 0)
			{
				OnPick_(pick0);
			}
			return;
		}

		if ((one * (game.platform.TimeMillisecondsFromStart() - lastbuildMilliseconds) / 1000) >= BuildDelay(game)
			|| IsNextShot)
		{
			if (left && game.d_Inventory.RightHand[game.ActiveMaterial] == null)
			{
				game.SendPacketClient(ClientPackets.MonsterHit(game.platform.FloatToInt(2 + game.rnd.NextFloat() * 4)));
			}
			if (left && !fastclicking)
			{
				//TODO: animation
				fastclicking = false;
			}
			if ((left || right || middle) && (!isgrenade))
			{
				lastbuildMilliseconds = game.platform.TimeMillisecondsFromStart();
			}
			if (isgrenade && game.mouseleftdeclick)
			{
				lastbuildMilliseconds = game.platform.TimeMillisecondsFromStart();
			}
			if (game.reloadstartMilliseconds != 0)
			{
				PickingEnd(left, right, middle, ispistol);
				return;
			}
			if (ispistolshoot)
			{
				// only run the shoot logic once per ammo count
				// disabled temporarily for unlimited ammo
				// if (game.LoadedAmmo[item.BlockId] != lastAmmoCount || lastAmmoCount == 0)
				// {
				// 	lastAmmoCount = game.LoadedAmmo[item.BlockId];
				//
				// 	if (game.LoadedAmmo[item.BlockId] <= 0)
				// 	{
				// 		game.AudioPlay("error.ogg");
				//
				// 		PickingEnd(left, right, middle, ispistol);
				// 		return;
				// 	}
				// }
				// else
				// {
				// 	PickingEnd(left, right, middle, ispistol);
				// 	return;
				// }

				float toX = pick.End[0];
				float toY = pick.End[1];
				float toZ = pick.End[2];

				// pick will be 0 when running
				// and so use pick2 instead
				if (toX == 0 && pick2count.value > 0)
				{
					toX = pick2[0].blockPos[0];
					toY = pick2[0].blockPos[1];
					toZ = pick2[0].blockPos[2];
				}

				Packet_ClientShot shot = new Packet_ClientShot();
				shot.FromX = game.SerializeFloat(pick.Start[0]);
				shot.FromY = game.SerializeFloat(pick.Start[1]);
				shot.FromZ = game.SerializeFloat(pick.Start[2]);
				shot.ToX = game.SerializeFloat(toX);
				shot.ToY = game.SerializeFloat(toY);
				shot.ToZ = game.SerializeFloat(toZ);
				shot.HitPlayer = -1;

				for (int i = 0; i < game.entitiesCount; i++)
				{
					if (game.entities[i] == null)
					{
						continue;
					}

					if (game.entities[i].drawModel == null)
					{
						continue;
					}

					Entity p_ = game.entities[i];

					if (p_.networkPosition == null)
					{
						continue;
					}

					if (!p_.networkPosition.PositionLoaded)
					{
						continue;
					}

					float feetposX = p_.position.x;
					float feetposY = p_.position.y;
					float feetposZ = p_.position.z;

					Box3D bodybox = new Box3D();

					float headsize = (p_.drawModel.ModelHeight - p_.drawModel.eyeHeight) * 2; //0.4f;
					float h = p_.drawModel.ModelHeight - headsize;
					float r = one * 35 / 100;

					bodybox.AddPoint(feetposX - r, feetposY + 0, feetposZ - r);
					bodybox.AddPoint(feetposX - r, feetposY + 0, feetposZ + r);
					bodybox.AddPoint(feetposX + r, feetposY + 0, feetposZ - r);
					bodybox.AddPoint(feetposX + r, feetposY + 0, feetposZ + r);

					bodybox.AddPoint(feetposX - r, feetposY + h, feetposZ - r);
					bodybox.AddPoint(feetposX - r, feetposY + h, feetposZ + r);
					bodybox.AddPoint(feetposX + r, feetposY + h, feetposZ - r);
					bodybox.AddPoint(feetposX + r, feetposY + h, feetposZ + r);

					Box3D headbox = new Box3D();

					headbox.AddPoint(feetposX - r, feetposY + h, feetposZ - r);
					headbox.AddPoint(feetposX - r, feetposY + h, feetposZ + r);
					headbox.AddPoint(feetposX + r, feetposY + h, feetposZ - r);
					headbox.AddPoint(feetposX + r, feetposY + h, feetposZ + r);

					headbox.AddPoint(feetposX - r, feetposY + h + headsize, feetposZ - r);
					headbox.AddPoint(feetposX - r, feetposY + h + headsize, feetposZ + r);
					headbox.AddPoint(feetposX + r, feetposY + h + headsize, feetposZ - r);
					headbox.AddPoint(feetposX + r, feetposY + h + headsize, feetposZ + r);

					float localeyeposX = game.EyesPosX();
					float localeyeposY = game.EyesPosY();
					float localeyeposZ = game.EyesPosZ();

					float[] p;

					p = Intersection.CheckLineBoxExact(pick, headbox);

					if (p != null && game.ServerConfig.PvP)
					{
						float blockPosDist = (pick2count.value > 0) ? game.Dist(pick2[0].blockPos[0], pick2[0].blockPos[1], pick2[0].blockPos[2], localeyeposX, localeyeposY, localeyeposZ) : 0;
						float playerDist = game.Dist(p[0], p[1], p[2], localeyeposX, localeyeposY, localeyeposZ);
						float maxDist = CurrentPickDistance(game);

						// no block has been picked or it is behind the player
						// and we have not reached the weapon's max range
						if ((pick2count.value == 0 || blockPosDist > playerDist) && playerDist <= maxDist)
						{
							if (!isgrenade && itemBlock.Animations.HitCount > 0)
							{
								Entity entity = new Entity();
								Sprite sprite = new Sprite();
								sprite.positionX = p[0];
								sprite.positionY = p[1];
								sprite.positionZ = p[2];
								sprite.image = game.platform.StringFormat("{0}.png", itemBlock.Animations.Hit[0]);
								sprite.animationcount = 5;
								sprite.width = 40;
								sprite.height = 40;
								entity.sprite = sprite;
								entity.expires = Expires.Create(1.0f);
								game.EntityAddLocal(entity);
							}
							shot.HitPlayer = i;
							shot.IsHitHead = 1;
						}
					}
					else
					{
						p = Intersection.CheckLineBoxExact(pick, bodybox);

						if (p != null && game.ServerConfig.PvP)
						{
							float blockPosDist = (pick2count.value > 0) ? game.Dist(pick2[0].blockPos[0], pick2[0].blockPos[1], pick2[0].blockPos[2], localeyeposX, localeyeposY, localeyeposZ) : 0;
							float playerDist = game.Dist(p[0], p[1], p[2], localeyeposX, localeyeposY, localeyeposZ);
							float maxDist = CurrentPickDistance(game);

							// no block has been picked or it is behind the player
							// and we have not reached the weapon's max range
							if ((pick2count.value == 0 || blockPosDist > playerDist) && playerDist <= maxDist)
							{
								if (!isgrenade && itemBlock.Animations.HitCount > 0)
								{
									Entity entity = new Entity();
									Sprite sprite = new Sprite();
									sprite.positionX = p[0];
									sprite.positionY = p[1];
									sprite.positionZ = p[2];
									sprite.image = game.platform.StringFormat("{0}.png", itemBlock.Animations.Hit[0]);
									sprite.animationcount = 5;
									sprite.width = 40;
									sprite.height = 40;
									entity.sprite = sprite;
									entity.expires = Expires.Create(1.0f);
									game.EntityAddLocal(entity);
								}
								shot.HitPlayer = i;
								shot.IsHitHead = 0;
							}
						}
					}
				}

				shot.WeaponBlock = item.BlockId;
				// disabled temporarily for unlimited ammo
				// game.LoadedAmmo[item.BlockId] = game.LoadedAmmo[item.BlockId] - 1;
				// game.TotalAmmo[item.BlockId] = game.TotalAmmo[item.BlockId] - 1;

				if (itemBlock.Sounds.ShootCount > 0)
				{
					game.AudioPlay(game.platform.StringFormat("{0}.ogg", itemBlock.Sounds.Shoot[0]));
				}

				float projectilespeed = game.DeserializeFloat(itemBlock.ProjectileSpeedFloat);

				if (itemBlock.PistolType != 1)
				{
					if (projectilespeed > 0) {
						Entity entity = game.CreateBulletEntity(
							itemBlock, pick.Start[0], pick.Start[1], pick.Start[2],
							toX, toY, toZ, projectilespeed
						);

						game.EntityAddLocal(entity);
					}
				}
				else
				{
					float vX = toX - pick.Start[0];
					float vY = toY - pick.Start[1];
					float vZ = toZ - pick.Start[2];
					float vLength = game.Length(vX, vY, vZ);
					vX /= vLength;
					vY /= vLength;
					vZ /= vLength;
					vX *= projectilespeed;
					vY *= projectilespeed;
					vZ *= projectilespeed;
					shot.ExplodesAfter = game.SerializeFloat(game.grenadetime - wait);

					{
						Entity grenadeEntity = new Entity();

						Sprite sprite = new Sprite();
						sprite.image = game.platform.StringFormat("{0}.png", itemBlock.Animations.Shot[0]);
						sprite.width = 14;
						sprite.height = 14;
						sprite.animationcount = 0;
						sprite.positionX = pick.Start[0];
						sprite.positionY = pick.Start[1];
						sprite.positionZ = pick.Start[2];
						grenadeEntity.sprite = sprite;

						Grenade_ projectile = new Grenade_();
						projectile.velocityX = vX;
						projectile.velocityY = vY;
						projectile.velocityZ = vZ;
						projectile.block = item.BlockId;
						projectile.sourcePlayer = game.LocalPlayerId;

						grenadeEntity.expires = Expires.Create(game.grenadetime - wait);

						grenadeEntity.grenade = projectile;
						game.EntityAddLocal(grenadeEntity);
					}
				}
				Packet_Client packet = new Packet_Client();
				packet.Id = Packet_ClientIdEnum.Shot;
				packet.Shot = shot;
				game.SendPacketClient(packet);

				if (itemBlock.Sounds.ShootEndCount > 0)
				{
					game.pistolcycle = game.rnd.Next() % itemBlock.Sounds.ShootEndCount;
					game.AudioPlay(game.platform.StringFormat("{0}.ogg", itemBlock.Sounds.ShootEnd[game.pistolcycle]));
				}

				bulletsshot++;
				if (bulletsshot < game.DeserializeFloat(itemBlock.BulletsPerShotFloat))
				{
					NextBullet(game, bulletsshot);
				}

				// recoil
				game.player.position.rotx -= game.rnd.NextFloat() * game.CurrentRecoil();
				game.player.position.roty += game.rnd.NextFloat() * game.CurrentRecoil() * 2 - game.CurrentRecoil();

				PickingEnd(left, right, middle, ispistol);
				return;
			}
			if (ispistol && right)
			{
				PickingEnd(left, right, middle, ispistol);
				return;
			}
			if (pick2count.value > 0)
			{
				if (middle)
				{
					int blockX = game.platform.FloatToInt(pick0.Current()[0]);
					int blockY = game.platform.FloatToInt(pick0.Current()[1]);
					int blockZ = game.platform.FloatToInt(pick0.Current()[2]);
					if (game.map.IsValidPos(blockX, blockZ, blockY))
					{
						int clonesource = game.map.GetBlock(blockX, blockZ, blockY);
						int clonesource2 = game.d_Data.WhenPlayerPlacesGetsConvertedTo()[clonesource];
						bool gotoDone = false;
						// find this block in another right hand
						for (int i = 0; i < 10; i++)
						{
							if (game.d_Inventory.RightHand[i] != null
								&& game.d_Inventory.RightHand[i].ItemClass == Packet_ItemClassEnum.Block
								&& game.d_Inventory.RightHand[i].BlockId == clonesource2)
							{
								game.ActiveMaterial = i;
								gotoDone = true;
							}
						}
						if (!gotoDone)
						{
							IntRef freehand = game.d_InventoryUtil.FreeHand(game.ActiveMaterial);
							// find this block in the inventory
							for (int i = 0; i < game.d_Inventory.ItemsCount; i++)
							{
								Packet_PositionItem k = game.d_Inventory.Items[i];
								if (k == null)
								{
									continue;
								}
								if (k.Value_.ItemClass == Packet_ItemClassEnum.Block
									&& k.Value_.BlockId == clonesource2)
								{
									// free hand
									if (freehand != null)
									{
										game.WearItem(
											game.InventoryPositionMainArea(k.X, k.Y),
											game.InventoryPositionMaterialSelector(freehand.value));
										break;
									}
									// try to replace the current slot
									// if (game.d_Inventory.RightHand[game.ActiveMaterial] != null
									// 	&& game.d_Inventory.RightHand[game.ActiveMaterial].ItemClass == Packet_ItemClassEnum.Block)
									// {
									// 	game.MoveToInventory(
									// 		game.InventoryPositionMaterialSelector(game.ActiveMaterial));
										game.InventoryClick(
											game.InventoryPositionMainArea(k.X, k.Y));
										game.WearItem(
											game.InventoryPositionMainArea(k.X, k.Y),
											game.InventoryPositionMaterialSelector(game.ActiveMaterial));
									// }
								}
							}
						}
						string[] sound = game.d_Data.CloneSound()[clonesource];
						if (sound != null) // && sound.Length > 0)
						{
							game.AudioPlay(game.platform.StringFormat("{0}.ogg", sound[0])); //TODO: sound cycle
						}
					}
				}
				else if (left || right)
				{
					// the block we selected
					int blockXselected = game.platform.FloatToInt(pick0.Current()[0]);
					int blockYselected = game.platform.FloatToInt(pick0.Current()[1]);
					int blockZselected = game.platform.FloatToInt(pick0.Current()[2]);

					// the block we will add or destroy
					// defaults to the selected block
					int blockX = blockXselected;
					int blockY = blockYselected;
					int blockZ = blockZselected;

					// the side that was selected
					float[] blockSide = pick0.collisionPos;

					// the type of block it is
					int blockType = game.map.GetBlock(blockX, blockZ, blockY);

					// can the block be used
					bool blockIsUsable = false;

					// use / add
					if (right)
					{
						// the block is usable
						if (game.IsUsableBlock(blockType))
						{
							blockIsUsable = true;
						}
						// add a new block
						else
						{
							// add it to the side we selected
							blockX = game.platform.FloatToInt(pick0.Translated()[0]);
							blockY = game.platform.FloatToInt(pick0.Translated()[1]);
							blockZ = game.platform.FloatToInt(pick0.Translated()[2]);

							// get the type of block we are adding (i.e. the block in our hand)
							blockType = ((game.BlockInHand() == null) ? 1 : game.BlockInHand().value);
						}
					}
					// destroy
					else
					{
						// you can not destroy bedrock
						if (blockType == game.d_Data.BlockIdAdminium())
						{
							PickingEnd(left, right, middle, ispistol);
							return;
						}
					}

					// make sure the block is inside the map
					if (game.map.IsValidPos(blockX, blockZ, blockY))
					{
						// make sure the block has all 3 coordinates
						if (!(pick0.blockPos[0] == -1 && pick0.blockPos[1] == -1 && pick0.blockPos[2] == -1))
						{
							// use
							if (right)
							{
								bool blockUsed = false;

								// the block is usable
								if (blockIsUsable)
								{
									// use the attacked block
									// i.e. an attacked block is any usable block
									// that is currently displaying it's info
									// see ModDraw2dMisc.DrawBlockInfo
									if (game.currentAttackedBlock != null)
									{
										blockX = game.currentAttackedBlock.X;
										blockY = game.currentAttackedBlock.Y;
										blockZ = game.currentAttackedBlock.Z;

										// crafting table
										if (blockType == game.d_Data.BlockIdCraftingTable())
										{
											// this will be handled in ModGuiCrafting.OnMouseDown
											// and we must continue past this and run OnPick()
										}
										// mounting a rail
										else if (game.d_Data.IsRailTile(blockType))
										{
											game.player.position.x = blockX + (one / 2);
											game.player.position.y = blockZ + 1;
											game.player.position.z = blockY + (one / 2);

											// disable player movement
											game.stopPlayerMove = true;
											game.controls.SetFreemove(FreemoveLevelEnum.None);

											blockUsed = true;
										}
										// everything else
										else
										{
											game.SendSetBlock(blockX, blockY, blockZ, Packet_BlockSetModeEnum.Use, 0, game.ActiveMaterial);

											blockUsed = true;
										}
									}
								}

								// use the attacked entity
								// TODO: verify the mouse is still on the entity
								if (game.currentlyAttackedEntity != -1)
								{
									if (game.entities[game.currentlyAttackedEntity].usable)
									{
										for (int i = 0; i < game.clientmodsCount; i++)
										{
											if (game.clientmods[i] == null) { continue; }

											OnUseEntityArgs args = new OnUseEntityArgs();
											args.entityId = game.currentlyAttackedEntity;
											game.clientmods[i].OnUseEntity(game, args);
										}

										game.SendPacketClient(ClientPackets.UseEntity(game.currentlyAttackedEntity));

										blockUsed = true;
									}
								}

								// the block was used
								if (blockUsed)
								{
									PickingEnd(left, right, middle, ispistol);
									return;
								}
								// add a new block when the selected block is not used
								else
                                {
									// add it to the side we selected
									blockX = game.platform.FloatToInt(pick0.Translated()[0]);
									blockY = game.platform.FloatToInt(pick0.Translated()[1]);
									blockZ = game.platform.FloatToInt(pick0.Translated()[2]);

									// get the type of block we are adding (i.e. the block in our hand)
									blockType = ((game.BlockInHand() == null) ? 1 : game.BlockInHand().value);
								}
							}
							// attack
							else
                            {
								game.currentAttackedBlock = Vector3IntRef.Create(blockX, blockZ, blockY);

								if (game.blockHealth.ContainsKey(blockX, blockZ, blockY) == false)
								{
									// set the block's health to its strength when it's never been hit before
									// most blocks default to 4 and require four hits when weapon strength is 1
									game.blockHealth.Set(blockX, blockZ, blockY, game.GetCurrentBlockHealth(blockX, blockZ, blockY));
								}

								// TODO: get WeaponAttackStrength() working when we integrate War mode
								game.blockHealth.Set(blockX, blockZ, blockY, game.blockHealth.Get(blockX, blockZ, blockY) - 1); // game.WeaponAttackStrength());

								if (game.GetCurrentBlockHealth(blockX, blockZ, blockY) <= 0)
								{
									if (game.currentAttackedBlock != null)
									{
										game.blockHealth.Remove(blockX, blockZ, blockY);
									}

									game.currentAttackedBlock = null;
								}
								else
                                {
									// the block hasn't been destroyed yet
									PickingEnd(left, right, middle, ispistol);
									return;
								}
							}

							// add or destroy the block
							OnPick(game, blockX, blockZ, blockY, blockXselected, blockZselected, blockYselected, blockSide, right);

							// get the block's sound
							string[] sound = left ? game.d_Data.BreakSound()[blockType] : game.d_Data.BuildSound()[blockType];

							if (sound != null)
							{
								// play the block's sound
								game.AudioPlay(game.platform.StringFormat("{0}.ogg", sound[0]));
							}
						}
					}
				}
			}
		}

		PickingEnd(left, right, middle, ispistol);
	}

	internal float BuildDelay(Game game)
	{
		float default_ = (1f * 95 / 100) * (1 / game.basemovespeed);
		Packet_Item item = game.d_Inventory.RightHand[game.ActiveMaterial];
		if (item == null || item.ItemClass != Packet_ItemClassEnum.Block)
		{
			return default_;
		}
		float delay = game.DeserializeFloat(game.blocktypes[item.BlockId].DelayFloat);
		if (delay == 0)
		{
			return default_;
		}
		return delay;
	}

	//value is original block.
	internal DictionaryVector3Float fillarea;
	internal Vector3IntRef fillstart;
	internal Vector3IntRef fillend;

	internal void OnPick(Game game, int blockposX, int blockposY, int blockposZ, int blockposoldX, int blockposoldY, int blockposoldZ, float[] collisionPos, bool right)
	{
		float xfract = collisionPos[0] - game.MathFloor(collisionPos[0]);
		float zfract = collisionPos[2] - game.MathFloor(collisionPos[2]);
		int activematerial = game.MaterialSlots_(game.ActiveMaterial);
		int railstart = game.d_Data.BlockIdRailstart();
		if (game.d_Data.IsRailTile(activematerial) && activematerial != 179)
		{
			RailDirection dirnew;
			if (activematerial == railstart + RailDirectionFlags.Horizontal ||
				activematerial == railstart + RailDirectionFlags.Vertical)
			{
				dirnew = PickHorizontalVertical(xfract, zfract);
			}
			else
			{
				dirnew = PickCorners(xfract, zfract);
			}
			int dir = game.d_Data.Rail()[game.map.GetBlock(blockposoldX, blockposoldY, blockposoldZ)];
			if (dir != 0)
			{
				blockposX = blockposoldX;
				blockposY = blockposoldY;
				blockposZ = blockposoldZ;
			}
			activematerial = railstart + (dir | DirectionUtils.ToRailDirectionFlags(dirnew));
		}
		int x = game.platform.FloatToInt(blockposX);
		int y = game.platform.FloatToInt(blockposY);
		int z = game.platform.FloatToInt(blockposZ);
		int mode = right ? Packet_BlockSetModeEnum.Create : Packet_BlockSetModeEnum.Destroy;
		{
			if (game.IsAnyPlayerInPos(x, y, z) || activematerial == 151) // Compass
			{
				return;
			}
			Vector3IntRef v = Vector3IntRef.Create(x, y, z);
			Vector3IntRef oldfillstart = fillstart;
			Vector3IntRef oldfillend = fillend;
			if (mode == Packet_BlockSetModeEnum.Create)
			{
				if (game.blocktypes[activematerial].IsTool)
				{
					OnPickUseWithTool(game, blockposX, blockposY, blockposZ);
					return;
				}

				if (activematerial == game.d_Data.BlockIdCuboid())
				{
					ClearFillArea(game);

					if (fillstart != null)
					{
						Vector3IntRef f = fillstart;
						if (!game.IsFillBlock(game.map.GetBlock(f.X, f.Y, f.Z)))
						{
							fillarea.Set(f.X, f.Y, f.Z, game.map.GetBlock(f.X, f.Y, f.Z));
						}
						game.SetBlock(f.X, f.Y, f.Z, game.d_Data.BlockIdFillStart());


						FillFill(game, v, fillstart);
					}
					if (!game.IsFillBlock(game.map.GetBlock(v.X, v.Y, v.Z)))
					{
						fillarea.Set(v.X, v.Y, v.Z, game.map.GetBlock(v.X, v.Y, v.Z));
					}
					game.SetBlock(v.X, v.Y, v.Z, game.d_Data.BlockIdCuboid());
					fillend = v;
					game.RedrawBlock(v.X, v.Y, v.Z);
					return;
				}
				if (activematerial == game.d_Data.BlockIdFillStart())
				{
					ClearFillArea(game);
					if (!game.IsFillBlock(game.map.GetBlock(v.X, v.Y, v.Z)))
					{
						fillarea.Set(v.X, v.Y, v.Z, game.map.GetBlock(v.X, v.Y, v.Z));
					}
					game.SetBlock(v.X, v.Y, v.Z, game.d_Data.BlockIdFillStart());
					fillstart = v;
					fillend = null;
					game.RedrawBlock(v.X, v.Y, v.Z);
					return;
				}
				if (fillarea.ContainsKey(v.X, v.Y, v.Z))// && fillarea[v])
				{
					game.SendFillArea(fillstart.X, fillstart.Y, fillstart.Z, fillend.X, fillend.Y, fillend.Z, activematerial);
					ClearFillArea(game);
					fillstart = null;
					fillend = null;
					return;
				}
			}
			else
			{
				if (game.blocktypes[activematerial].IsTool)
				{
					OnPickUseWithTool(game, blockposX, blockposY, blockposoldZ);
					return;
				}
				//delete fill start
				if (fillstart != null && fillstart.X == v.X && fillstart.Y == v.Y && fillstart.Z == v.Z)
				{
					ClearFillArea(game);
					fillstart = null;
					fillend = null;
					return;
				}
				//delete fill end
				if (fillend != null && fillend.X == v.X && fillend.Y == v.Y && fillend.Z == v.Z)
				{
					ClearFillArea(game);
					fillend = null;
					return;
				}

				// TODO: display particle effects when destroying a block
				// game.particleEffectBlockBreak.StartParticleEffect(x, y, z);
				// return;
			}
			game.SendSetBlockAndUpdateSpeculative(activematerial, x, y, z, mode);
		}
	}

	internal void ClearFillArea(Game game)
	{
		for (int i = 0; i < fillarea.itemsCount; i++)
		{
			Vector3Float k = fillarea.items[i];
			if (k == null)
			{
				continue;
			}
			game.SetBlock(k.x, k.y, k.z, game.platform.FloatToInt(k.value));
			game.RedrawBlock(k.x, k.y, k.z);
		}
		fillarea.Clear();
	}

	internal void FillFill(Game game, Vector3IntRef a_, Vector3IntRef b_)
	{
		int startx = MathCi.MinInt(a_.X, b_.X);
		int endx = MathCi.MaxInt(a_.X, b_.X);
		int starty = MathCi.MinInt(a_.Y, b_.Y);
		int endy = MathCi.MaxInt(a_.Y, b_.Y);
		int startz = MathCi.MinInt(a_.Z, b_.Z);
		int endz = MathCi.MaxInt(a_.Z, b_.Z);
		for (int x = startx; x <= endx; x++)
		{
			for (int y = starty; y <= endy; y++)
			{
				for (int z = startz; z <= endz; z++)
				{
					if (fillarea.Count() > game.fillAreaLimit)
					{
						ClearFillArea(game);
						return;
					}
					if (!game.IsFillBlock(game.map.GetBlock(x, y, z)))
					{
						fillarea.Set(x, y, z, game.map.GetBlock(x, y, z));
						game.SetBlock(x, y, z, game.d_Data.BlockIdFillArea());
						game.RedrawBlock(x, y, z);
					}
				}
			}
		}
	}

	internal void OnPickUseWithTool(Game game, int posX, int posY, int posZ)
	{
		game.SendSetBlock(posX, posY, posZ, Packet_BlockSetModeEnum.UseWithTool, game.d_Inventory.RightHand[game.ActiveMaterial].BlockId, game.ActiveMaterial);
	}

	internal RailDirection PickHorizontalVertical(float xfract, float yfract)
	{
		float x = xfract;
		float y = yfract;
		if (y >= x && y >= (1 - x))
		{
			return RailDirection.Vertical;
		}
		if (y < x && y < (1 - x))
		{
			return RailDirection.Vertical;
		}
		return RailDirection.Horizontal;
	}

	internal RailDirection PickCorners(float xfract, float zfract)
	{
		float half = 0.5f;
		if (xfract < half && zfract < half)
		{
			return RailDirection.UpLeft;
		}
		if (xfract >= half && zfract < half)
		{
			return RailDirection.UpRight;
		}
		if (xfract < half && zfract >= half)
		{
			return RailDirection.DownLeft;
		}
		return RailDirection.DownRight;
	}

	void PickEntity(Game game, Line3D pick, BlockPosSide[] pick2, IntRef pick2count)
	{
		game.SelectedEntityId = -1;
		game.currentlyAttackedEntity = -1;
		float one = 1;
		for (int i = 0; i < game.entitiesCount; i++)
		{
			if (game.entities[i] == null)
			{
				continue;
			}
			if (i == game.LocalPlayerId)
			{
				continue;
			}
			if (game.entities[i].drawModel == null)
			{
				continue;
			}
			Entity p_ = game.entities[i];
			if (p_.networkPosition == null)
			{
				continue;
			}
			if (!p_.networkPosition.PositionLoaded)
			{
				continue;
			}
			if (!p_.usable)
			{
				continue;
			}
			float feetposX = p_.position.x;
			float feetposY = p_.position.y;
			float feetposZ = p_.position.z;

			float dist = game.Dist(feetposX, feetposY, feetposZ, game.player.position.x, game.player.position.y, game.player.position.z);
			if (dist > 5)
			{
				continue;
			}

			//var p = PlayerPositionSpawn;
			Box3D bodybox = new Box3D();
			float h = p_.drawModel.ModelHeight;
			float r = one * 35 / 100;

			bodybox.AddPoint(feetposX - r, feetposY + 0, feetposZ - r);
			bodybox.AddPoint(feetposX - r, feetposY + 0, feetposZ + r);
			bodybox.AddPoint(feetposX + r, feetposY + 0, feetposZ - r);
			bodybox.AddPoint(feetposX + r, feetposY + 0, feetposZ + r);

			bodybox.AddPoint(feetposX - r, feetposY + h, feetposZ - r);
			bodybox.AddPoint(feetposX - r, feetposY + h, feetposZ + r);
			bodybox.AddPoint(feetposX + r, feetposY + h, feetposZ - r);
			bodybox.AddPoint(feetposX + r, feetposY + h, feetposZ + r);

			float[] p;
			float localeyeposX = game.EyesPosX();
			float localeyeposY = game.EyesPosY();
			float localeyeposZ = game.EyesPosZ();
			p = Intersection.CheckLineBoxExact(pick, bodybox);
			if (p != null)
			{
				//do not allow to shoot through terrain
				if (pick2count.value == 0 || (game.Dist(pick2[0].blockPos[0], pick2[0].blockPos[1], pick2[0].blockPos[2], localeyeposX, localeyeposY, localeyeposZ)
					> game.Dist(p[0], p[1], p[2], localeyeposX, localeyeposY, localeyeposZ)))
				{
					game.SelectedEntityId = i;
					if (game.cameratype == CameraType.Fpp || game.cameratype == CameraType.Tpp)
					{
						game.currentlyAttackedEntity = i;
					}
				}
			}
		}
	}

	void UpdateEntityHit(Game game)
	{
		//Only single hit when mouse clicked
		if (game.currentlyAttackedEntity != -1 && game.mouseLeft)
		{
			for (int i = 0; i < game.clientmodsCount; i++)
			{
				if (game.clientmods[i] == null) { continue; }
				OnUseEntityArgs args = new OnUseEntityArgs();
				args.entityId = game.currentlyAttackedEntity;
				game.clientmods[i].OnHitEntity(game, args);
			}
			game.SendPacketClient(ClientPackets.HitEntity(game.currentlyAttackedEntity));
		}
	}

	internal bool fastclicking;
	internal void PickingEnd(bool left, bool right, bool middle, bool ispistol)
	{
		fastclicking = false;
		if ((!(left || right || middle)) && (!ispistol))
		{
			lastbuildMilliseconds = 0;
			fastclicking = true;
		}
	}

	internal int lastbuildMilliseconds;

	internal void OnPick_(BlockPosSide pick0)
	{
		//playerdestination = pick0.pos;
	}

	Unproject unproject;
	float[] tempViewport;
	float[] tempRay;
	float[] tempRayStartPoint;
	public void GetPickingLine(Game game, Line3D retPick, bool ispistolshoot)
	{
		int mouseX;
		int mouseY;

		if (game.cameratype == CameraType.Fpp || game.cameratype == CameraType.Tpp)
		{
			mouseX = game.Width() / 2;
			mouseY = game.Height() / 2;
		}
		else
		{
			mouseX = game.mouseCurrentX;
			mouseY = game.mouseCurrentY;
		}

		PointFloatRef aim = GetAim(game);
		if (ispistolshoot && (aim.X != 0 || aim.Y != 0))
		{
			mouseX += game.platform.FloatToInt(aim.X);
			mouseY += game.platform.FloatToInt(aim.Y);
		}

		tempViewport[0] = 0;
		tempViewport[1] = 0;
		tempViewport[2] = game.Width();
		tempViewport[3] = game.Height();

		unproject.UnProject(mouseX, game.Height() - mouseY, 1, game.mvMatrix.Peek(), game.pMatrix.Peek(), tempViewport, tempRay);
		unproject.UnProject(mouseX, game.Height() - mouseY, 0, game.mvMatrix.Peek(), game.pMatrix.Peek(), tempViewport, tempRayStartPoint);

		float raydirX = (tempRay[0] - tempRayStartPoint[0]);
		float raydirY = (tempRay[1] - tempRayStartPoint[1]);
		float raydirZ = (tempRay[2] - tempRayStartPoint[2]);
		float raydirLength = game.Length(raydirX, raydirY, raydirZ);
		raydirX /= raydirLength;
		raydirY /= raydirLength;
		raydirZ /= raydirLength;

		retPick.Start = new float[3];
		retPick.Start[0] = tempRayStartPoint[0];// +raydirX; //do not pick behind
		retPick.Start[1] = tempRayStartPoint[1];// +raydirY;
		retPick.Start[2] = tempRayStartPoint[2];// +raydirZ;

		float pickDistance1 = CurrentPickDistance(game) * 1; // ((ispistolshoot) ? 100 : 1);
		pickDistance1 += 1;
		retPick.End = new float[3];
		retPick.End[0] = tempRayStartPoint[0] + raydirX * pickDistance1;
		retPick.End[1] = tempRayStartPoint[1] + raydirY * pickDistance1;
		retPick.End[2] = tempRayStartPoint[2] + raydirZ * pickDistance1;
	}

	internal PointFloatRef GetAim(Game game)
	{
		if (game.CurrentAimRadius() <= 1)
		{
			return PointFloatRef.Create(0, 0);
		}
		float half = 0.5f;
		float x;
		float y;
		for (; ; )
		{
			x = (game.rnd.NextFloat() - half) * game.CurrentAimRadius() * 2;
			y = (game.rnd.NextFloat() - half) * game.CurrentAimRadius() * 2;
			float dist1 = game.platform.MathSqrt(x * x + y * y);
			if (dist1 <= game.CurrentAimRadius())
			{
				break;
			}
		}
		return PointFloatRef.Create(x, y);
	}

	float CurrentPickDistance(Game game)
	{
		float pick_distance = game.PICK_DISTANCE;
		IntRef inHand = game.BlockInHand();
		if (inHand != null)
		{
			if (game.blocktypes[inHand.value].PickDistanceWhenUsedFloat > 0)
			{
				// This check ensures that players can select blocks when no value is given
				pick_distance = game.DeserializeFloat(game.blocktypes[inHand.value].PickDistanceWhenUsedFloat);
			}
		}
		if (game.cameratype == CameraType.Tpp)
		{
			pick_distance = game.tppcameradistance + game.PICK_DISTANCE;
		}
		if (game.cameratype == CameraType.Overhead)
		{
			if (game.platform.IsFastSystem())
			{
				pick_distance = 100;
			}
			else
			{
				pick_distance = game.overheadcameradistance * 2;
			}
		}
		return pick_distance;
	}
}
