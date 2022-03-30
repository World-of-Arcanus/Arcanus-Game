/// <summary>
/// ScreenConnectToIp shows an input mask for entering server details manually.
/// This enables users to connect to private (unlisted) servers and also allows connections without a valid account.
/// </summary>
public class ScreenConnectToIp : MainMenuScreen
{
	public ScreenConnectToIp()
	{
		wbtn_back = new ButtonWidget();
		AddWidget(wbtn_back);

		wbtn_connect = new ButtonWidget();
		AddWidget(wbtn_connect);

		wtxt_title = new TextWidget();
		wtxt_title.SetFont(fontTitle);
		wtxt_title.SetAlignment(TextAlign.Center);
		AddWidget(wtxt_title);

		wtxt_statusMessage = new TextWidget();
		wtxt_statusMessage.SetFont(fontMessage);
		wtxt_statusMessage.SetAlignment(TextAlign.Center);
		AddWidget(wtxt_statusMessage);

		wtxt_ip = new TextWidget();
		wtxt_ip.SetFont(fontDefault);
		wtxt_ip.SetAlignment(TextAlign.Right);
		wtxt_ip.SetBaseline(TextBaseline.Middle);
		AddWidget(wtxt_ip);

		wtxt_port = new TextWidget();
		wtxt_port.SetFont(fontDefault);
		wtxt_port.SetAlignment(TextAlign.Right);
		wtxt_port.SetBaseline(TextBaseline.Middle);
		AddWidget(wtxt_port);

		wtbx_ip = new TextBoxWidget();
		AddWidget(wtbx_ip);

		wtbx_port = new TextBoxWidget();
		AddWidget(wtbx_port);

		wcbx_save = new CheckBoxWidget();
		wcbx_save.SetDescription("Save to server list");
		wcbx_save.SetChecked(true);
		AddWidget(wcbx_save);

		// tabbing setup
		wtbx_ip.SetNextWidget(wtbx_port);
		wtbx_port.SetNextWidget(wbtn_connect);
		wbtn_connect.SetNextWidget(wbtn_back);
		wbtn_back.SetNextWidget(wtbx_ip);
		wtbx_ip.SetFocused(true);
	}

	ButtonWidget wbtn_back;
	ButtonWidget wbtn_connect;
	TextWidget wtxt_title;
	TextWidget wtxt_statusMessage;
	TextWidget wtxt_ip;
	TextWidget wtxt_port;
	TextBoxWidget wtbx_ip;
	TextBoxWidget wtbx_port;
	CheckBoxWidget wcbx_save;

	bool loaded;

	public override void LoadTranslations()
	{
		wbtn_back.SetText(menu.lang.Get("MainMenu_ButtonBack"));
		wbtn_connect.SetText(menu.lang.Get("MainMenu_ConnectToIpConnect"));
		wtxt_ip.SetText(menu.lang.Get("MainMenu_ConnectToIpIp"));
		wtxt_port.SetText(menu.lang.Get("MainMenu_ConnectToIpPort"));
		wtxt_title.SetText(menu.lang.Get("MainMenu_MultiplayerConnectIP"));
	}

	public override void Render(float dt)
	{
		// load stored values or defaults
		if (!loaded)
		{
			wtbx_ip.SetContent(gamePlatform, gamePlatform.GetPreferences().GetString("ConnectToIpIp", "127.0.0.1"));
			wtbx_port.SetContent(gamePlatform, gamePlatform.GetPreferences().GetString("ConnectToIpPort", "25565"));
			loaded = true;
		}

		float windowX = gamePlatform.GetCanvasWidth();
		float windowY = gamePlatform.GetCanvasHeight();

		float bkwidth = 350;
		float bkheight = 4 * 64;
		float scale = menu.uiRenderer.GetScale();
		float leftx = (windowX / 2 - (bkwidth / 2)) + 50 + 25; // text width + padding
		float topy = windowY / 2 - 210;

		wtxt_title.x = windowX / 2;
		wtxt_title.y = topy;

		wtxt_statusMessage.x = windowX / 2;
		wtxt_statusMessage.y = topy + 258 * scale;

		menu.uiRenderer.Draw2dTexture(menu.uiRenderer.GetTexture("serverlist_entry_background.png"),
			windowX / 2 - (bkwidth / 2), wtxt_title.y + wtxt_title.sizey, bkwidth, bkheight, null, 0, -1);

		wtxt_ip.x = leftx - 20 * scale;
		wtxt_ip.y = topy + 66 * scale;

		wtbx_ip.x = leftx;
		wtbx_ip.y = topy + 50 * scale;
		wtbx_ip.sizex = bkwidth - 100;
		wtbx_ip.sizey = 32 * scale;

		wtxt_port.x = leftx - 20 * scale;
		wtxt_port.y = topy + 125 * scale;

		wtbx_port.x = leftx;
		wtbx_port.y = topy + 109 * scale;
		wtbx_port.sizex = bkwidth - 100;
		wtbx_port.sizey = 32 * scale;

		wcbx_save.x = leftx;
		wcbx_save.y = topy + 168 * scale;
		wcbx_save.sizex = bkwidth - 100;
		wcbx_save.sizey = 32 * scale;

		wbtn_connect.x = windowX / 2 - (256 / 2);
		wbtn_connect.y = topy + 336 * scale;
		wbtn_connect.sizex = 256 * scale;
		wbtn_connect.sizey = 64 * scale;

		wbtn_back.x = 40 * scale;
		wbtn_back.y = windowY - 104 * scale;
		wbtn_back.sizex = 256 * scale;
		wbtn_back.sizey = 64 * scale;

		DrawWidgets(dt);
	}

	public override void OnBackPressed()
	{
		menu.StartMainMenu();
		// menu.StartMultiplayer();
	}

	public override void OnButton(AbstractMenuWidget w)
	{
		if (w == wbtn_connect)
		{
			// check input
			IntRef ret = new IntRef();
			if (Game.StringEquals(wtbx_ip.GetContent(), ""))
			{
				wtxt_statusMessage.SetText(menu.lang.Get("MainMenu_ConnectToIpErrorIp"));
			}
			else if (!gamePlatform.IntTryParse(wtbx_port.GetContent(), ret))
			{
				wtxt_statusMessage.SetText(menu.lang.Get("MainMenu_ConnectToIpErrorPort"));
			}
			else
			{
				// save user input
				Preferences preferences = gamePlatform.GetPreferences();
				preferences.SetString("ConnectToIpIp", wtbx_ip.GetContent());
				preferences.SetString("ConnectToIpPort", wtbx_port.GetContent());
				gamePlatform.SetPreferences(preferences);

				// perform login
				menu.StartLogin(null, wtbx_ip.GetContent(), ret.value);
			}
		}
		if (w == wbtn_back)
		{
			OnBackPressed();
		}
	}
}
