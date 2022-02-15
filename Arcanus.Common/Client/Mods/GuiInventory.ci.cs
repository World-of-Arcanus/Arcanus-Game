﻿public class ModGuiInventory : ClientMod
{
	public ModGuiInventory()
	{
		// indexed by enum WearPlace
		wearPlaceStart = new PointRef[5];
		{
			// new Point(282,85), // LeftHand,
			wearPlaceStart[0] = PointRef.Create(34, 100);  // RightHand,
			wearPlaceStart[1] = PointRef.Create(74, 100);  // MainArmor,
			wearPlaceStart[2] = PointRef.Create(194, 100); // Boots,
			wearPlaceStart[3] = PointRef.Create(114, 100); // Helmet,
			wearPlaceStart[4] = PointRef.Create(154, 100); // Gauntlet,
		}

		// indexed by enum WearPlace
		wearPlaceCells = new PointRef[5];
		{
			// new Point(2,4), // LeftHand,
			wearPlaceCells[0] = PointRef.Create(1, 1); // RightHand,
			wearPlaceCells[1] = PointRef.Create(1, 1); // MainArmor,
			wearPlaceCells[2] = PointRef.Create(1, 1); // Boots,
			wearPlaceCells[3] = PointRef.Create(1, 1); // Helmet,
			wearPlaceCells[4] = PointRef.Create(1, 1); // Gauntlet,
		}

		CellCountInPageX = 6;
		CellCountInPageY = 3;
		CellCountTotalX = 6;
		CellCountTotalY = 3 * 10;
		CellDrawSize = 79;

		InventoryType = 1;
		InventoryPage = 1;
		InventoryPageTotal = 1;
	}

	internal Game game;
	internal GameDataItemsClient dataItems;
	internal InventoryUtilClient inventoryUtil;
	internal IInventoryController controller;

	internal int CellDrawSize;

	public int InventoryStartX() { return game.Width() / 2 - 549 / 2; }
	public int InventoryStartY() { return game.Height() / 2 - 600 / 2; }
	public int CellsStartX() { return 34 + InventoryStartX(); }
	public int CellsStartY() { return 180 + InventoryStartY(); }
	int MaterialSelectorStartX() { return game.platform.FloatToInt(MaterialSelectorBackgroundStartX() + 17 * game.Scale()); }
	int MaterialSelectorStartY() { return game.platform.FloatToInt(MaterialSelectorBackgroundStartY() + 26 * game.Scale()); }
	int MaterialSelectorBackgroundStartX() { return game.platform.FloatToInt(game.Width() / 2 - (512 / 2) * game.Scale()); }
	int MaterialSelectorBackgroundStartY() { return game.platform.FloatToInt(game.Height() - 99 * game.Scale()); }
	int CellCountInPageX;
	int CellCountInPageY;
	int CellCountTotalX;
	int CellCountTotalY;

	public int ActiveMaterialCellSize() { return game.platform.FloatToInt(48 * game.Scale()); }

	public override void OnKeyPress(Game game_, KeyPressEventArgs args)
	{
		if (game.guistate != GuiState.Inventory)
		{
			return;
		}
		int keyChar = args.GetKeyChar();
		if (keyChar == 49) { game.ActiveMaterial = 0; }
		if (keyChar == 50) { game.ActiveMaterial = 1; }
		if (keyChar == 51) { game.ActiveMaterial = 2; }
		if (keyChar == 52) { game.ActiveMaterial = 3; }
		if (keyChar == 53) { game.ActiveMaterial = 4; }
		if (keyChar == 54) { game.ActiveMaterial = 5; }
		if (keyChar == 55) { game.ActiveMaterial = 6; }
		if (keyChar == 56) { game.ActiveMaterial = 7; }
		if (keyChar == 57) { game.ActiveMaterial = 8; }
		if (keyChar == 48) { game.ActiveMaterial = 9; }

		if (keyChar >= 48 && keyChar <= 57)
		{
			game.SendPacketClient(ClientPackets.ActiveMaterialSlot(game.ActiveMaterial));
		}
	}

	public override void OnMouseDown(Game game_, MouseEventArgs args)
	{
		if (game.guistate != GuiState.Inventory)
		{
			return;
		}

		PointRef scaledMouse = PointRef.Create(args.GetX(), args.GetY());

		if (args.GetButton() == MouseButtonEnum.Left)
		{
			// page prev click
			if (GetMouseOverForPagePrev(scaledMouse))
			{
				InventoryPage--;
			}

			// page next click
			if (GetMouseOverForPageNext(scaledMouse))
			{
				InventoryPage++;
			}

			// inventory click

			// get the item the mouse is currently over
			Packet_Item itemForInventory = GetMouseOverForInventory(scaledMouse);

			if (itemForInventory != null)
			{
				// get the item's inventory position
				Packet_PositionItem pos = null;

				for (int i = 0; i < game.d_Inventory.ItemsCount; i++)
				{
					if (game.d_Inventory.Items[i].Value_ == itemForInventory)
                    {
						pos = game.d_Inventory.Items[i];
					}
				}

				if (pos != null)
                {
					// send the position to the server
					Packet_InventoryPosition p = new Packet_InventoryPosition();
					p.Type = Packet_InventoryPositionTypeEnum.MainArea;
					p.AreaX = pos.GetX();
					p.AreaY = pos.GetY();

					controller.InventoryClick(p);

					args.SetHandled(true);

					return;
				}
			}

			// materials click
			if (SelectedMaterialSelectorSlot(scaledMouse) != null)
			{
				game.ActiveMaterial = SelectedMaterialSelectorSlot(scaledMouse).value;

				Packet_InventoryPosition p = new Packet_InventoryPosition();
				p.Type = Packet_InventoryPositionTypeEnum.MaterialSelector;
				p.MaterialId = game.ActiveMaterial;

				controller.InventoryClick(p);

				args.SetHandled(true);

				return;
			}
		}

		if (game.guistate == GuiState.Inventory)
		{
			args.SetHandled(true);
			return;
		}

		// drop items on ground
		// if (scaledMouse.X < CellsStartX() && scaledMouse.Y < MaterialSelectorStartY())
		// {
		//     int posx = game.SelectedBlockPositionX;
		//     int posy = game.SelectedBlockPositionY;
		//     int posz = game.SelectedBlockPositionZ;
		//     Packet_InventoryPosition p = new Packet_InventoryPosition();
		//     {
		//         p.Type = Packet_InventoryPositionTypeEnum.Ground;
		//         p.GroundPositionX = posx;
		//         p.GroundPositionY = posy;
		//         p.GroundPositionZ = posz;
		//     }
		//     controller.InventoryClick(p);
		// }

		if (SelectedWearPlace(scaledMouse) != null)
		{
			Packet_InventoryPosition p = new Packet_InventoryPosition();
			p.Type = Packet_InventoryPositionTypeEnum.WearPlace;
			p.WearPlace = (SelectedWearPlace(scaledMouse).value);
			p.ActiveMaterial = game.ActiveMaterial;

			controller.InventoryClick(p);

			args.SetHandled(true);

			return;
		}

		game.GuiStateBackToGame();

		return;
	}

	public override void OnTouchStart(Game game_, TouchEventArgs e)
	{
		MouseEventArgs args = new MouseEventArgs();
		args.SetX(e.GetX());
		args.SetY(e.GetY());
		OnMouseDown(game_, args);
		e.SetHandled(args.GetHandled());
	}

	public bool IsMouseOverCells()
	{
		return SelectedCellOrScrollbar(game.mouseCurrentX, game.mouseCurrentY);
	}

	public override void OnMouseUp(Game game_, MouseEventArgs args)
	{
		if (game != null && game.guistate != GuiState.Inventory)
		{
			return;
		}
	}

	IntRef SelectedMaterialSelectorSlot(PointRef scaledMouse)
	{
		if (scaledMouse.X >= MaterialSelectorStartX() && scaledMouse.Y >= MaterialSelectorStartY()
			&& scaledMouse.X < MaterialSelectorStartX() + 10 * ActiveMaterialCellSize()
			&& scaledMouse.Y < MaterialSelectorStartY() + 10 * ActiveMaterialCellSize())
		{
			return IntRef.Create((scaledMouse.X - MaterialSelectorStartX()) / ActiveMaterialCellSize());
		}
		return null;
	}

	bool SelectedCellOrScrollbar(int scaledMouseX, int scaledMouseY)
	{
		if (scaledMouseX < CellsStartX() || scaledMouseY < CellsStartY()
			|| scaledMouseX > CellsStartX() + (CellCountInPageX + 1) * CellDrawSize
			|| scaledMouseY > CellsStartY() + CellCountInPageY * CellDrawSize)
		{
			return false;
		}

		return true;
	}

	public int InventoryType;
	public int InventoryPage;
	public int InventoryPageTotal;
	public Packet_Item[] InventoryItems;
	public InventoryItemPos[] InventoryItemsPos;

	public override void OnNewFrameDraw2d(Game game_, float deltaTime)
	{
		game = game_;

		if (dataItems == null)
		{
			dataItems = new GameDataItemsClient();
			dataItems.game = game_;
			controller = ClientInventoryController.Create(game_);
			inventoryUtil = game.d_InventoryUtil;
		}

		if (game.guistate == GuiState.MapLoading)
		{
			return;
		}

		InventoryItems = new Packet_Item[game.d_Inventory.ItemsCount];
		InventoryItemsPos = new InventoryItemPos[game.d_Inventory.ItemsCount];

		DrawMaterialSelector();

		if (game.guistate != GuiState.Inventory)
		{
			return;
		}

		PointRef scaledMouse = PointRef.Create(game.mouseCurrentX, game.mouseCurrentY);

		game.Draw2dBitmapFile("inventory.png", InventoryStartX(), InventoryStartY(), 1024, 1024);

		// sort by the order they got added to the inventory (instead of by id)
		Packet_Item[] sorted = new Packet_Item[game.d_Inventory.ItemsCount];

		for (int i = 0; i < game.d_Inventory.ItemsCount; i++)
		{
			Packet_Item item = game.d_Inventory.Items[i].Value_;
			sorted[game.blocktypes[item.BlockId].Sort] = item;
		}

		int b = 0; // blocks on page
		int t = 0; // blocks on tab

		// get the items for the current tab and page
		for (int i = 0; i < game.d_Inventory.ItemsCount; i++)
		{
			Packet_Item item = sorted[i];

			if (item == null)
			{
				continue;
			}

			// filter by the current tab
			if (game.blocktypes[item.BlockId].InventoryType == InventoryType)
			{
				// filter by the current page
				float pageFloat = (t + 1 + 0.0f) / (CellCountInPageX * CellCountInPageY);
				int page = game.platform.FloatToIntCeiling(pageFloat);

				if (page == InventoryPage)
				{
					InventoryItems[b] = item;
					b++;
				}

				InventoryPageTotal = page;
				t++;
			}
		}

		// display inventory
		for (int i = 0; i < game.d_Inventory.ItemsCount; i++)
		{
			Packet_Item item = InventoryItems[i];

			if (item == null)
			{
				continue;
			}

			int col = i % CellCountInPageX;
			int row = game.platform.FloatToIntCeiling(i / CellCountInPageX);

			int padding = 4;
			int blkSize = CellDrawSize - (padding * 2);

			// start + border + padding
			int xOffset = 1 + col + padding;
			int yOffset = 1 + row + padding;

			int xMin = CellsStartX() + xOffset + (col * CellDrawSize);
			int xMax = xMin + CellDrawSize;

			int yMin = CellsStartY() + yOffset + (row * CellDrawSize);
			int yMax = yMin + CellDrawSize;

			DrawItem(xMin, yMin, item, blkSize, blkSize);

			InventoryItemsPos[i] = InventoryItemPos.Create(xMin, xMax, yMin, yMax);
		}

		if (InventoryPage < 1)
        {
			InventoryPage = InventoryPageTotal;
		}
        else if (InventoryPage > InventoryPageTotal)
        {
			InventoryPage = 1;
		}

		// display paging
		DrawPagingText();

		// display wearables
		// DrawItem(wearPlaceStart[WearPlace_.RightHand].X + InventoryStartX(), wearPlaceStart[WearPlace_.RightHand].Y + InventoryStartY(), game.d_Inventory.RightHand[game.ActiveMaterial], 0, 0);
		// DrawItem(wearPlaceStart[WearPlace_.MainArmor].X + InventoryStartX(), wearPlaceStart[WearPlace_.MainArmor].Y + InventoryStartY(), game.d_Inventory.MainArmor, 0, 0);
		// DrawItem(wearPlaceStart[WearPlace_.Boots].X + InventoryStartX(), wearPlaceStart[WearPlace_.Boots].Y + InventoryStartY(), game.d_Inventory.Boots, 0, 0);
		// DrawItem(wearPlaceStart[WearPlace_.Helmet].X + InventoryStartX(), wearPlaceStart[WearPlace_.Helmet].Y + InventoryStartY(), game.d_Inventory.Helmet, 0, 0);
		// DrawItem(wearPlaceStart[WearPlace_.Gauntlet].X + InventoryStartX(), wearPlaceStart[WearPlace_.Gauntlet].Y + InventoryStartY(), game.d_Inventory.Gauntlet, 0, 0);

		// display materials
		DrawMaterialSelector();

		bool canClick = false;

		// tab paging
		bool pagePrev = GetMouseOverForPagePrev(scaledMouse);
		bool pageNext = GetMouseOverForPageNext(scaledMouse);

		if (pagePrev || pageNext)
		{
			canClick = true;
		}

		// info for inventory
		Packet_Item itemForInventory = GetMouseOverForInventory(scaledMouse);

		if (itemForInventory != null)
		{
			DrawItemInfo(scaledMouse.X, scaledMouse.Y, itemForInventory);
			canClick = true;
		}

		// info for wearables
		if (SelectedWearPlace(scaledMouse) != null)
		{
			int selected = SelectedWearPlace(scaledMouse).value;

			Packet_Item itemAtWearPlace = inventoryUtil.ItemAtWearPlace(selected, game.ActiveMaterial);

			if (itemAtWearPlace != null)
			{
				DrawItemInfo(scaledMouse.X, scaledMouse.Y, itemAtWearPlace);
				canClick = true;
			}
		}

		// info for materials
		if (SelectedMaterialSelectorSlot(scaledMouse) != null)
		{
			int selected = SelectedMaterialSelectorSlot(scaledMouse).value;

			Packet_Item item = game.d_Inventory.RightHand[selected];

			if (item != null)
			{
				DrawItemInfo(scaledMouse.X, scaledMouse.Y, item);
				canClick = true;
			}
		}

		if (canClick)
        {
			game.platform.SetWindowCursor(0, 0, 32, 32,
				game.uiRenderer.GetFile("mousecursor-click.png"),
				game.uiRenderer.GetFileLength("mousecursor-click.png")
			);
		}
        else
        {
			game.platform.SetWindowCursor(0, 0, 32, 32,
				game.uiRenderer.GetFile("mousecursor.png"),
				game.uiRenderer.GetFileLength("mousecursor.png")
			);
		}
	}

	public void DrawMaterialSelector()
	{
		game.Draw2dBitmapFile("materials.png", MaterialSelectorBackgroundStartX(), MaterialSelectorBackgroundStartY(), game.platform.FloatToInt(1024 * game.Scale()), game.platform.FloatToInt(128 * game.Scale()));
		int materialSelectorStartX_ = MaterialSelectorStartX();
		int materialSelectorStartY_ = MaterialSelectorStartY();
		for (int i = 0; i < 10; i++)
		{
			Packet_Item item = game.d_Inventory.RightHand[i];
			if (item != null)
			{
				DrawItem(materialSelectorStartX_ + 4 + (i * ActiveMaterialCellSize()), materialSelectorStartY_ + 4,
					item, ActiveMaterialCellSize() - 8, ActiveMaterialCellSize() - 8);
			}
		}
		game.Draw2dBitmapFile("activematerial.png",
			MaterialSelectorStartX() + ActiveMaterialCellSize() * game.ActiveMaterial,
			MaterialSelectorStartY(), ActiveMaterialCellSize() * 64 / 48, ActiveMaterialCellSize() * 64 / 48);
	}

	IntRef SelectedWearPlace(PointRef scaledMouse)
	{
		for (int i = 0; i < wearPlaceStartLength; i++)
		{
			PointRef p = wearPlaceStart[i];
			p.X += InventoryStartX();
			p.Y += InventoryStartY();
			PointRef cells = wearPlaceCells[i];
			if (scaledMouse.X >= p.X && scaledMouse.Y >= p.Y
				&& scaledMouse.X < p.X + cells.X * CellDrawSize
				&& scaledMouse.Y < p.Y + cells.Y * CellDrawSize)
			{
				return IntRef.Create(i);
			}
		}
		return null;
	}

	const int wearPlaceStartLength = 5;
	//indexed by enum WearPlace
	PointRef[] wearPlaceStart;
	//indexed by enum WearPlace
	PointRef[] wearPlaceCells;

	void DrawItem(int screenposX, int screenposY, Packet_Item item, int drawsizeX, int drawsizeY)
	{
		if (item == null)
		{
			return;
		}
		int sizex = dataItems.ItemSizeX(item);
		int sizey = dataItems.ItemSizeY(item);
		if (drawsizeX == 0 || drawsizeX == -1)
		{
			drawsizeX = CellDrawSize * sizex;
			drawsizeY = CellDrawSize * sizey;
		}
		if (item.ItemClass == Packet_ItemClassEnum.Block)
		{
			if (item.BlockId == 0)
			{
				return;
			}
			game.Draw2dTexture(game.terrainTexture, screenposX, screenposY,
				drawsizeX, drawsizeY, IntRef.Create(dataItems.TextureIdForInventory()[item.BlockId]), game.texturesPacked(), ColorCi.FromArgb(255, 255, 255, 255), false);
			if (item.BlockCount > 1)
			{
				FontCi font = new FontCi();
				font.size = 8;
				game.Draw2dText(game.platform.IntToString(item.BlockCount), font, screenposX, screenposY, null, false);
			}
		}
		else
		{
			game.Draw2dBitmapFile(dataItems.ItemGraphics(item), screenposX, screenposY,
				drawsizeX, drawsizeY);
		}
	}

	public void DrawItemInfo(int screenposX, int screenposY, Packet_Item item)
	{
		int sizex = dataItems.ItemSizeX(item);
		int sizey = dataItems.ItemSizeY(item);
		IntRef tw = new IntRef();
		IntRef th = new IntRef();
		float one = 1;
		FontCi font = new FontCi();
		font.size = 10;
		game.platform.TextSize(dataItems.ItemInfo(item), font, tw, th);
		tw.value += 6;
		th.value += 4;
		int w = game.platform.FloatToInt(tw.value + CellDrawSize * sizex);
		int h = game.platform.FloatToInt(th.value < CellDrawSize * sizey ? CellDrawSize * sizey + 4 : th.value);
		if (screenposX < w + 20) { screenposX = w + 20; }
		if (screenposY < h + 20) { screenposY = h + 20; }
		if (screenposX > game.Width() - (w + 20)) { screenposX = game.Width() - (w + 20); }
		if (screenposY > game.Height() - (h + 20)) { screenposY = game.Height() - (h + 20); }
		game.Draw2dTexture(game.WhiteTexture(), screenposX - w, screenposY - h, w, h, null, 0, ColorCi.FromArgb(255, 0, 0, 0), false);
		game.Draw2dTexture(game.WhiteTexture(), screenposX - w + 2, screenposY - h + 2, w - 4, h - 4, null, 0, ColorCi.FromArgb(255, 105, 105, 105), false);
		game.Draw2dText(dataItems.ItemInfo(item), font, screenposX - tw.value + 4, screenposY - h + 2, null, false);
		Packet_Item item2 = new Packet_Item();
		item2.BlockId = item.BlockId;
		DrawItem(screenposX - w + 2, screenposY - h + 2, item2, 0, 0);
	}

	public void DrawPagingText()
    {
		string page = game.platform.IntToString(InventoryPage);
		string total = game.platform.IntToString(InventoryPageTotal);
		string pagingText = game.platform.StringFormat2("Page  {0}  of  {1}", page, total);

		FontCi pagingFont = new FontCi();
		pagingFont.style = 1; // bold
		pagingFont.size = 9;

		// border + middle point of the 2nd to last cell
		float pagingX = CellsStartX() + (1 + CellCountInPageX - 1) + (CellDrawSize * (CellCountInPageX - 1.5f)) - 6;

		// center between the arrows
		float pagingY = CellsStartY() - (CellDrawSize / 3) - 2;

		// white text
		IntRef pagingColor = IntRef.Create(ColorCi.FromArgb(255, 255, 255, 255));

		game.Draw2dText(pagingText, pagingFont, pagingX, pagingY, pagingColor, false);
	}

	public bool GetMouseOverForPagePrev(PointRef scaledMouse)
	{
		int bordersX = 1 + CellCountInPageX - 2;
		int bordersY = 1;

		// 23 = left gui edge to right arrow edge
		//  5 = extra spacing to left cursor edge
		int leaveX = 23 + 5;

		// 1 = top border
		// 5 = finger tip
		int enterY = 1 + 5;

		if (scaledMouse.X >= (CellsStartX() + bordersX + (CellDrawSize * (CellCountInPageX - 2)) + 1) &&
			scaledMouse.X <= (CellsStartX() + bordersX + (CellDrawSize * (CellCountInPageX - 2)) + leaveX) &&
			scaledMouse.Y >= (CellsStartY() + bordersY - (CellDrawSize / 2)) &&
			scaledMouse.Y <= (CellsStartY() + bordersY - enterY))
		{
			return true;
		}

		return false;
	}

	public bool GetMouseOverForPageNext(PointRef scaledMouse)
    {
		int bordersX = 1 + CellCountInPageX;
		int bordersY = 1;

		// 23 = right gui edge to left arrow edge
		// 13 = 2nd knuckle after the pointer
		int leaveX = 23 + 13;

		// 1 = top border
		// 5 = finger tip
		int enterY = 1 + 5;

		if (scaledMouse.X >= (CellsStartX() + bordersX + (CellDrawSize * CellCountInPageX) - leaveX) &&
			scaledMouse.X <= (CellsStartX() + bordersX + (CellDrawSize * CellCountInPageX) - 1) &&
			scaledMouse.Y >= (CellsStartY() + bordersY - (CellDrawSize / 2)) &&
			scaledMouse.Y <= (CellsStartY() + bordersY - enterY))
		{
			return true;
		}

		return false;
	}

	public Packet_Item GetMouseOverForInventory(PointRef scaledMouse)
	{
		Packet_Item item = null;

		if (InventoryItemsPos != null)
		{
			for (int i = 0; i < game.d_Inventory.ItemsCount; i++)
			{
				if (InventoryItemsPos[i] != null)
				{
					if (scaledMouse.X >= InventoryItemsPos[i].xMin &&
						scaledMouse.X <= InventoryItemsPos[i].xMax &&
						scaledMouse.Y >= InventoryItemsPos[i].yMin &&
						scaledMouse.Y <= InventoryItemsPos[i].yMax)
					{
						item = InventoryItems[i]; break;
					}
				}
			}
		}

		return item;
	}

	public override void OnMouseWheelChanged(Game game_, MouseWheelEventArgs args)
	{
		float delta = args.GetDeltaPrecise();
		if ((game_.guistate == GuiState.Normal || (game_.guistate == GuiState.Inventory && !IsMouseOverCells()))
			&& (!game_.keyboardState[game_.GetKey(GlKeys.LShift)]))
		{
			game_.ActiveMaterial -= game_.platform.FloatToInt(delta);
			game_.ActiveMaterial = game_.ActiveMaterial % 10;
			while (game_.ActiveMaterial < 0)
			{
				game_.ActiveMaterial += 10;
			}
		}
	}
}
