public class ScreenSingleplayer : MainMenuScreen
{
	public ScreenSingleplayer()
	{
		wtxt_title = new TextWidget();
		wtxt_title.SetFont(fontTitle);
		AddWidget(wtxt_title);

		wlst_worldList = new ListWidget();
		AddWidget(wlst_worldList);

		wbtn_newWorld = new ButtonWidget();
		wbtn_newWorld.SetText("Create New World");
		AddWidget(wbtn_newWorld);

		wbtn_back = new ButtonWidget();
		AddWidget(wbtn_back);

		// wbtn_openFile = new ButtonWidget();
		// AddWidget(wbtn_openFile);

		gamesPerPage = 4;
	}

	TextWidget wtxt_title;
	ListWidget wlst_worldList;
	ButtonWidget wbtn_newWorld;
	ButtonWidget wbtn_back;
	// ButtonWidget wbtn_openFile;

	string[] savegames;
	int savegamesCount;
	int gamesPerPage;

	public override void LoadTranslations()
	{
		wtxt_title.SetText(menu.lang.Get("MainMenu_Singleplayer"));
		// wbtn_openFile.SetText(menu.lang.Get("MainMenu_SingleplayerButtonCreate"));
		wbtn_back.SetText(menu.lang.Get("MainMenu_ButtonBack"));
	}

	public override void Render(float dt)
	{
		float scale = menu.uiRenderer.GetScale();
		float buttonheight = 64 * scale;
		float buttonwidth = 244 * scale;
		float spacebetween = 10 * scale;

		float listimage = 64;
		float listpages = 64;
		float listwidth = 350 + listimage + listpages;

		float windowX = gamePlatform.GetCanvasWidth();
		float windowY = gamePlatform.GetCanvasHeight();

		wtxt_title.x = windowX / 2;
		wtxt_title.y = windowY / 2 - 210;
		wtxt_title.SetAlignment(TextAlign.Center);

		wlst_worldList.x = windowX / 2 - ((listwidth - 64) / 2);
		wlst_worldList.y = wtxt_title.y + wtxt_title.sizey;
		wlst_worldList.sizex = listwidth;
		wlst_worldList.sizey = gamesPerPage * (buttonheight + 6);

		wbtn_newWorld.x = windowX / 2 - (buttonwidth / 2);
		wbtn_newWorld.y = wlst_worldList.y + wlst_worldList.sizey + (spacebetween * 2);
		wbtn_newWorld.sizex = buttonwidth;
		wbtn_newWorld.sizey = buttonheight;

		wbtn_back.x = 40 * scale;
		wbtn_back.y = gamePlatform.GetCanvasHeight() - 104 * scale;
		wbtn_back.sizex = buttonwidth;
		wbtn_back.sizey = buttonheight;

		// wbtn_openFile.x = windowX / 2 - (buttonwidth / 2);
		// wbtn_openFile.y = windowY / 2 - (2 * (buttonheight + spacebetween)) + offsetfrommiddle;
		// wbtn_openFile.sizex = buttonwidth;
		// wbtn_openFile.sizey = buttonheight;

		LoadSavegameList();

		DrawWidgets(dt);
	}

	public override void OnBackPressed()
	{
		menu.StartMainMenu();
	}

	public override void OnButton(AbstractMenuWidget w)
	{
		if (w == wbtn_newWorld)
		{
			menu.StartGameSettings("", false);
		}

		if (w == wbtn_back)
		{
			OnBackPressed();
		}

		// if (w == wbtn_openFile)
		// {
		// 	string extension;
		// 	if (gamePlatform.SinglePlayerServerAvailable())
		// 	{
		// 		extension = "arcanus";
		// 	}
		// 	else
		// 	{
		// 		extension = "arcanusdbg";
		// 	}
		// 	string result = gamePlatform.FileOpenDialog(extension, "Arcanus Game", gamePlatform.PathSavegames());
		// 	if (result != null)
		// 	{
		// 		menu.ConnectToSingleplayer(result);
		// 	}
		// }
	}

	void LoadSavegameList()
	{
		if (savegames == null)
		{
			IntRef savegamesCount_ = new IntRef();
			savegames = menu.GetSavegames(savegamesCount_);
			savegamesCount = savegamesCount_.value;

			for (int i = 0; i < savegamesCount; i++)
			{
				ListEntry e = new ListEntry();
				e.textTopLeft = menu.p.FileName(savegames[i]);
				e.textBottomLeft = menu.p.StringFormat("Last played on {0}", menu.p.FileLastWriteTime(savegames[i]));
				e.imageMain = menu.p.FileImage(savegames[i]);
				e.menu = menu;

				wlst_worldList.AddElement(e);
			}

			int emptySlots = gamesPerPage - (savegamesCount % gamesPerPage);

			if (emptySlots < gamesPerPage || savegamesCount == 0)
			{
				for (int i = 0; i < emptySlots; i++)
				{
					ListEntry e = new ListEntry();
					e.textTopLeft = "";
					e.textBottomLeft = "Empty Slot";
					e.imageMain = "none";
					e.menu = menu;

					wlst_worldList.AddElement(e);
				}
			}
		}
	}
}
