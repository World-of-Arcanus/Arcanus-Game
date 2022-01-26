﻿public class ModGuiCrafting : ClientMod
{
	public ModGuiCrafting()
	{
		handler = new PacketHandlerCraftingRecipes();
		handler.mod = this;
		fontCraftingGui = new FontCi();
	}
	PacketHandlerCraftingRecipes handler;
	public override void OnNewFrameDraw2d(Game game, float deltaTime)
	{
		if (d_CraftingTableTool == null)
		{
			d_CraftingTableTool = new CraftingTableTool();
			d_CraftingTableTool.d_Map = MapStorage2.Create(game);
			d_CraftingTableTool.d_Data = game.d_Data;
		}
		game.packetHandlers[Packet_ServerIdEnum.CraftingRecipes] = handler;
		if (game.guistate != GuiState.CraftingRecipes)
		{
			return;
		}
		DrawCraftingRecipes(game);
	}

	public override void OnNewFrameFixed(Game game, NewFrameEventArgs args)
	{
		if (game.guistate != GuiState.CraftingRecipes)
		{
			return;
		}
		CraftingMouse(game);
	}

	internal Packet_CraftingRecipe[] d_CraftingRecipes;
	internal int d_CraftingRecipesCount;

	internal int[] currentRecipes;
	internal int currentRecipesCount;

	internal int craftingTableposx;
	internal int craftingTableposy;
	internal int craftingTableposz;
	internal Packet_CraftingRecipe[] craftingrecipes2;
	internal int craftingrecipes2Count;
	internal int[] craftingblocks;
	internal int craftingblocksCount;
	internal int craftingselectedrecipe;
	internal CraftingTableTool d_CraftingTableTool;
	FontCi fontCraftingGui;

	internal void DrawCraftingRecipes(Game game)
	{
		currentRecipes = new int[1024];
		currentRecipesCount = 0;
		for (int i = 0; i < craftingrecipes2Count; i++)
		{
			Packet_CraftingRecipe r = craftingrecipes2[i];
			if (r == null)
			{
				continue;
			}
			bool next = false;
			//can apply recipe?
			for (int k = 0; k < r.IngredientsCount; k++)
			{
				Packet_Ingredient ingredient = r.Ingredients[k];
				if (ingredient == null)
				{
					continue;
				}
				if (craftingblocksFindAllCount(craftingblocks, craftingblocksCount, ingredient.Type) < ingredient.Amount)
				{
					next = true;
					break;
				}
			}
			if (!next)
			{
				currentRecipes[currentRecipesCount++] = i;
			}
		}
		int menustartx = game.xcenter(600);
		int menustarty = game.ycenter(currentRecipesCount * 80);
		if (currentRecipesCount == 0)
		{
			game.Draw2dText(game.language.NoMaterialsForCrafting(), fontCraftingGui, game.xcenter(200), game.ycenter(20), null, false);
			return;
		}
		for (int i = 0; i < currentRecipesCount; i++)
		{
			Packet_CraftingRecipe r = craftingrecipes2[currentRecipes[i]];
			for (int ii = 0; ii < r.IngredientsCount; ii++)
			{
				int xx = menustartx + 20 + ii * 130;
				int yy = menustarty + i * 80;
				game.Draw2dTexture(game.d_TerrainTextures.terrainTexture(), xx, yy, 32, 32, IntRef.Create(game.TextureIdForInventory[r.Ingredients[ii].Type]), game.texturesPacked(), ColorCi.FromArgb(255, 255, 255, 255), false);
				game.Draw2dText(game.platform.StringFormat2("{0} {1}", game.platform.IntToString(r.Ingredients[ii].Amount), game.blocktypes[r.Ingredients[ii].Type].Name), fontCraftingGui, xx + 50, yy,
				   IntRef.Create(i == craftingselectedrecipe ? ColorCi.FromArgb(255, 255, 0, 0) : ColorCi.FromArgb(255, 255, 255, 255)), false);
			}
			{
				int xx = menustartx + 20 + 400;
				int yy = menustarty + i * 80;
				game.Draw2dTexture(game.d_TerrainTextures.terrainTexture(), xx, yy, 32, 32, IntRef.Create(game.TextureIdForInventory[r.Output.Type]), game.texturesPacked(), ColorCi.FromArgb(255, 255, 255, 255), false);
				game.Draw2dText(game.platform.StringFormat2("{0} {1}", game.platform.IntToString(r.Output.Amount), game.blocktypes[r.Output.Type].Name), fontCraftingGui, xx + 50, yy,
				  IntRef.Create(i == craftingselectedrecipe ? ColorCi.FromArgb(255, 255, 0, 0) : ColorCi.FromArgb(255, 255, 255, 255)), false);
			}
		}
	}
	int craftingblocksFindAllCount(int[] craftingblocks_, int craftingblocksCount_, int p)
	{
		int count = 0;
		for (int i = 0; i < craftingblocksCount_; i++)
		{
			if (craftingblocks_[i] == p)
			{
				count++;
			}
		}
		return count;
	}

	internal void CraftingMouse(Game game)
	{
		if (currentRecipes == null)
		{
			return;
		}
		int menustartx = game.xcenter(600);
		int menustarty = game.ycenter(currentRecipesCount * 80);
		if (game.mouseCurrentY >= menustarty && game.mouseCurrentY < menustarty + currentRecipesCount * 80)
		{
			craftingselectedrecipe = (game.mouseCurrentY - menustarty) / 80;
		}
		else
		{
			//craftingselectedrecipe = -1;
		}
		if (game.mouseleftclick)
		{
			if (currentRecipesCount != 0)
			{
				CraftingRecipeSelected(game, craftingTableposx, craftingTableposy, craftingTableposz, IntRef.Create(currentRecipes[craftingselectedrecipe]));
			}
			game.mouseleftclick = false;
			game.GuiStateBackToGame();
		}
	}

	public override void OnMouseDown(Game game, MouseEventArgs args)
	{
		// use the selected block
		if (game.mouseRight)
		{
			// get the block's position
			int blockX = game.SelectedBlockPositionX;
			int blockY = game.SelectedBlockPositionZ;
			int blockZ = game.SelectedBlockPositionY;

			// make sure the block has all 3 coordinates
			if (!(blockX == -1 && blockY == -1 && blockZ == -1))
			{
				// crafting table
				if (game.map.GetBlock(blockX, blockY, blockZ) == game.d_Data.BlockIdCraftingTable())
				{
					// make sure the crafting table isn't open
					if (game.guistate != GuiState.CraftingRecipes)
					{
						// get all the tables that are joined together
						IntRef tableCount = new IntRef();
						Vector3IntRef[] table = d_CraftingTableTool.GetTable(blockX, blockY, blockZ, tableCount);

						// get all the blocks on the tables
						IntRef onTableCount = new IntRef();
						int[] onTable = d_CraftingTableTool.GetOnTable(table, tableCount.value, onTableCount);

						// get the number of blocks on the table that is selected
						IntRef onTableCountSel = new IntRef();
						Vector3IntRef[] tableSel = new Vector3IntRef[1];
						tableSel[0] = Vector3IntRef.Create(table[0].X, table[0].Y, table[0].Z);
						d_CraftingTableTool.GetOnTable(tableSel, 1, onTableCountSel);

						// open the crafting table when ...
						// there is nothing in our hand OR
						// there is a block on the selected table
						if (game.BlockInHand() == null || game.BlockInHand().value == 0 || onTableCountSel.value > 0)
						{
							// draw the list of crafting recipes
							CraftingRecipesStart(game, d_CraftingRecipes, d_CraftingRecipesCount, onTable, onTableCount.value, blockX, blockY, blockZ);

							// stop handling all other mouse down events
							args.SetHandled(true);
						}
					}
				}
			}
		}
	}

	internal void CraftingRecipesStart(Game game, Packet_CraftingRecipe[] recipes, int recipesCount, int[] blocks, int blocksCount, int posx, int posy, int posz)
	{
		craftingrecipes2 = recipes;
		craftingrecipes2Count = recipesCount;
		craftingblocks = blocks;
		craftingblocksCount = blocksCount;
		craftingTableposx = posx;
		craftingTableposy = posy;
		craftingTableposz = posz;
		game.guistate = GuiState.CraftingRecipes;
		game.menustate = new MenuState();
		game.SetFreeMouse(true);
	}

	internal void CraftingRecipeSelected(Game game, int x, int y, int z, IntRef recipe)
	{
		if (recipe == null)
		{
			return;
		}
		game.SendPacketClient(ClientPackets.Craft(x, y, z, recipe.value));
	}
}

public class PacketHandlerCraftingRecipes : ClientPacketHandler
{
	internal ModGuiCrafting mod;
	public override void Handle(Game game, Packet_Server packet)
	{
		mod.d_CraftingRecipes = packet.CraftingRecipes.CraftingRecipes;
		mod.d_CraftingRecipesCount = packet.CraftingRecipes.CraftingRecipesCount;
	}
}

public class CraftingTableTool
{
	internal IMapStorage2 d_Map;
	internal GameData d_Data;
	public int[] GetOnTable(Vector3IntRef[] table, int tableCount, IntRef retCount)
	{
		int[] ontable = new int[2048];
		int ontableCount = 0;
		for (int i = 0; i < tableCount; i++)
		{
			Vector3IntRef v = table[i];
			int t = d_Map.GetBlock(v.X, v.Y, v.Z + 1);
			if (t != 0)
            {
				ontable[ontableCount++] = t;
			}
		}
		retCount.value = ontableCount;
		return ontable;
	}
	const int maxcraftingtablesize = 2000;
	public Vector3IntRef[] GetTable(int posx, int posy, int posz, IntRef retCount)
	{
		Vector3IntRef[] l = new Vector3IntRef[2048];
		int lCount = 0;
		Vector3IntRef[] todo = new Vector3IntRef[2048];
		int todoCount = 0;
		todo[todoCount++] = Vector3IntRef.Create(posx, posy, posz);
		for (; ; )
		{
			if (todoCount == 0 || lCount >= maxcraftingtablesize)
			{
				break;
			}
			Vector3IntRef p = todo[todoCount - 1];
			todoCount--;
			if (Vector3IntRefArrayContains(l, lCount, p))
			{
				continue;
			}
			l[lCount++] = p;
			Vector3IntRef a = Vector3IntRef.Create(p.X + 1, p.Y, p.Z);
			if (d_Map.GetBlock(a.X, a.Y, a.Z) == d_Data.BlockIdCraftingTable())
			{
				todo[todoCount++] = a;
			}
			Vector3IntRef b = Vector3IntRef.Create(p.X - 1, p.Y, p.Z);
			if (d_Map.GetBlock(b.X, b.Y, b.Z) == d_Data.BlockIdCraftingTable())
			{
				todo[todoCount++] = b;
			}
			Vector3IntRef c = Vector3IntRef.Create(p.X, p.Y + 1, p.Z);
			if (d_Map.GetBlock(c.X, c.Y, c.Z) == d_Data.BlockIdCraftingTable())
			{
				todo[todoCount++] = c;
			}
			Vector3IntRef d = Vector3IntRef.Create(p.X, p.Y - 1, p.Z);
			if (d_Map.GetBlock(d.X, d.Y, d.Z) == d_Data.BlockIdCraftingTable())
			{
				todo[todoCount++] = d;
			}
		}
		retCount.value = lCount;
		return l;
	}

	bool Vector3IntRefArrayContains(Vector3IntRef[] l, int lCount, Vector3IntRef p)
	{
		for (int i = 0; i < lCount; i++)
		{
			if (l[i].X == p.X
				&& l[i].Y == p.Y
				&& l[i].Z == p.Z)
			{
				return true;
			}
		}
		return false;
	}
}
