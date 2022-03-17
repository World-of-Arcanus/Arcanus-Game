public class ScreenGameSettings : MainMenuScreen
{
	public ScreenGameSettings()
	{
		wtxt_title = new TextWidget();
		wtxt_title.SetFont(fontTitle);
		AddWidget(wtxt_title);

		wbtn_play = new ButtonWidget();
		wbtn_play.SetText("Play");
		AddWidget(wbtn_play);

		wbtn_back = new ButtonWidget();
		AddWidget(wbtn_back);
	}

	TextWidget wtxt_title;
	ButtonWidget wbtn_play;
	ButtonWidget wbtn_back;

	public override void LoadTranslations()
	{
		wtxt_title.SetText("Game Settings");
		wbtn_back.SetText(menu.lang.Get("MainMenu_ButtonBack"));
	}

	public override void Render(float dt)
	{
		float scale = menu.uiRenderer.GetScale();
		float buttonheight = 64 * scale;
		float buttonwidth = 244 * scale;
		float spacebetween = 10 * scale;

		float windowX = gamePlatform.GetCanvasWidth();
		float windowY = gamePlatform.GetCanvasHeight();

		wtxt_title.x = windowX / 2;
		wtxt_title.y = windowY / 2 - 210;
		wtxt_title.SetAlignment(TextAlign.Center);

		wbtn_play.x = windowX / 2 - (buttonwidth / 2);
		wbtn_play.y = wtxt_title.y + wtxt_title.sizey + (spacebetween * 2);
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
		if (w == wbtn_back)
		{
			OnBackPressed();
		}
	}
}
