public class ScreenGameSettings : MainMenuScreen
{
	public ScreenGameSettings()
	{
		wtxt_title = new TextWidget();
		wtxt_title.SetFont(fontTitle);
		AddWidget(wtxt_title);

		wlst_settings = new SettingsWidget();
		AddWidget(wlst_settings);

		wtxt_error = new TextWidget();
		wtxt_error.SetFont(fontMessage);
		wtxt_error.SetVisible(false);
		AddWidget(wtxt_error);

		wbtn_play = new ButtonWidget();
		wbtn_play.SetText("&ePlay");
		AddWidget(wbtn_play);

		wbtn_back = new ButtonWidget();
		AddWidget(wbtn_back);
	}

	TextWidget wtxt_title;
	SettingsWidget wlst_settings;
	TextWidget wtxt_error;
	ButtonWidget wbtn_play;
	ButtonWidget wbtn_back;

	string _name;

	public override void LoadTranslations()
	{
		wtxt_title.SetText("World Settings");
		wbtn_back.SetText(menu.lang.Get("MainMenu_ButtonBack"));
	}

	public override void Render(float dt)
	{
		float scale = menu.uiRenderer.GetScale();
		float buttonheight = 64 * scale;
		float buttonwidth = 244 * scale;
		float spacebetween = 10 * scale;

		int settingscount = 4;
		float settingswidth = 414;

		float windowX = gamePlatform.GetCanvasWidth();
		float windowY = gamePlatform.GetCanvasHeight();

		wtxt_title.x = windowX / 2;
		wtxt_title.y = windowY / 2 - 210;
		wtxt_title.SetAlignment(TextAlign.Center);

		wlst_settings.x = windowX / 2 - (settingswidth / 2);
		wlst_settings.y = wtxt_title.y + wtxt_title.sizey;
		wlst_settings.sizex = settingswidth;
		wlst_settings.sizey = settingscount * (buttonheight + 6);
		wlst_settings.Load(gamePlatform, _name);

		wtxt_error.x = windowX / 2;
		wtxt_error.y = wlst_settings.y + wlst_settings.sizey - (spacebetween * 2);
		wtxt_error.SetAlignment(TextAlign.Center);

		wbtn_play.x = windowX / 2 - (buttonwidth / 2);
		wbtn_play.y = wlst_settings.y + wlst_settings.sizey + (spacebetween * 2);
		wbtn_play.sizex = buttonwidth;
		wbtn_play.sizey = buttonheight;

		wbtn_back.x = 40 * scale;
		wbtn_back.y = gamePlatform.GetCanvasHeight() - 104 * scale;
		wbtn_back.sizex = buttonwidth;
		wbtn_back.sizey = buttonheight;

		DrawWidgets(dt);
	}

	public override void OnBackPressed()
	{
		menu.StartSingleplayer();
	}

	public override void OnButton(AbstractMenuWidget w)
	{
		if (w == wbtn_play)
		{
			wtxt_error.SetVisible(false);

			string nameInSettings = wlst_settings.GetName();
			string filename = menu.p.PathCombine(menu.p.PathSavegames(),
				menu.p.StringFormat("{0}.arcanus", nameInSettings));

			if (nameInSettings == "")
			{
				wtxt_error.SetText("&4You must enter a name!");
				wtxt_error.SetVisible(true);
				return;
			}
			else if (_name != nameInSettings)
			{
				if (menu.p.FileExists(filename))
				{
					wtxt_error.SetText("&4This name already exists!");
					wtxt_error.SetVisible(true);
					return;
				}
				else if (_name != "")
				{
					string filenameOld = menu.p.PathCombine(menu.p.PathSavegames(),
						menu.p.StringFormat("{0}.arcanus", _name));

					menu.p.FileRename(filenameOld, filename);
				}
			}

			ServerConfigCi config = new ServerConfigCi();
			config.PvP = wlst_settings.GetPvP();
			config.PvE = wlst_settings.GetPvE();

			menu.ConnectToSingleplayer(filename, config);
		}

		if (w == wbtn_back)
		{
			OnBackPressed();
		}
	}

	public void Load(string name)
    {
		_name = name;
	}
}
