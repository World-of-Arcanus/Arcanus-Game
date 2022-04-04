public class SettingsWidget : AbstractMenuWidget
{
	TextWidget wlbl_name;
	TextBoxWidget wtxt_name;
	TextWidget wlbl_settings;
	CheckBoxWidget wcbx_pvp;
	CheckBoxWidget wcbx_pve;

	GamePlatform _p;
	string _name;
	bool _loaded;

	public SettingsWidget()
	{
		clickable = true;

		FontCi lblFont = new FontCi();
		lblFont.style = 1;
		lblFont.size = 14;

		wlbl_name = new TextWidget();
		wlbl_name.SetFont(lblFont);
		wlbl_name.SetText("Name");

		wtxt_name = new TextBoxWidget();
		wtxt_name.SetFocused(true);

		wlbl_settings = new TextWidget();
		wlbl_settings.SetFont(lblFont);
		wlbl_settings.SetText("Settings & Mods");

		wcbx_pvp = new CheckBoxWidget();
		wcbx_pvp.SetDescription("PvP - Players can attack each other");
		wcbx_pvp.SetChecked(true);

		wcbx_pve = new CheckBoxWidget();
		wcbx_pve.SetDescription("PvE - Creatures can attack players");
	}

	public override void Draw(float dt, UiRenderer renderer)
	{
		if (!visible) { return; }
		if (sizex <= 0 || sizey <= 0) { return; }

		const int spacing = 16;
		const int elementSizeY = 32;
		float scale = renderer.GetScale();

		renderer.Draw2dTexture(renderer.GetTexture("serverlist_entry_background.png"), x, y, sizex, sizey, null, 0, color);

		wlbl_name.x = x + spacing;
		wlbl_name.y = y + spacing + elementSizeY;
		wlbl_name.sizex = 80 * scale;
		wlbl_name.sizey = elementSizeY * scale;

		wtxt_name.x = x + wlbl_name.sizex + spacing;
		wtxt_name.y = y + spacing + 3;
		wtxt_name.sizex = sizex - wlbl_name.sizex - (spacing * 2) * scale;
		wtxt_name.sizey = elementSizeY * scale;

		if (!_loaded)
		{
			wtxt_name.SetContent(_p, _name);

			// string filename = _p.PathCombine(_p.PathSavegames(),
			// 	_p.StringFormat("{0}.server", _name));

			string filename = _p.PathCombine(_p.PathSavegames(), _p.PathCombine("..", _p.PathCombine("Configuration", "ServerConfig.txt")));

			if (_p.FileExists(filename))
			{
				ServerConfigCi config = _p.GetServerConfig(filename);
				wcbx_pvp.SetChecked(config.PvP);
				wcbx_pve.SetChecked(config.PvE);
			}

			_loaded = true;
		}

		wlbl_settings.x = x + spacing;
		wlbl_settings.y = wtxt_name.y + (wtxt_name.sizey * 2) + spacing;
		wlbl_settings.sizex = sizex - (spacing * 2) * scale;
		wlbl_settings.sizey = elementSizeY * scale;

		wcbx_pvp.x = x + spacing;
		wcbx_pvp.y = wlbl_settings.y + spacing + 2;
		wcbx_pvp.sizex = sizex - (spacing * 2) * scale;
		wcbx_pvp.sizey = elementSizeY * scale;

		wcbx_pve.x = x + spacing;
		wcbx_pve.y = wcbx_pvp.y + wcbx_pvp.sizey + spacing + 2;
		wcbx_pve.sizex = sizex - (spacing * 2) * scale;
		wcbx_pve.sizey = elementSizeY * scale;

		wlbl_name.Draw(dt, renderer);
		wtxt_name.Draw(dt, renderer);
		wlbl_settings.Draw(dt, renderer);
		wcbx_pvp.Draw(dt, renderer);
		wcbx_pve.Draw(dt, renderer);
	}
	public override void OnMouseDown(GamePlatform p, MouseEventArgs args)
	{
		wtxt_name.OnMouseDown(p, args);
		wcbx_pvp.OnMouseDown(p, args);
		wcbx_pve.OnMouseDown(p, args);
	}

	public override void OnMouseMove(GamePlatform p, MouseEventArgs args)
	{

	}

	public override void OnKeyPress(GamePlatform p, KeyPressEventArgs args)
	{
		wtxt_name.OnKeyPress(p, args);
	}

	public override void OnKeyDown(GamePlatform p, KeyEventArgs args)
	{
		wtxt_name.OnKeyDown(p, args);
	}

	public void Load(GamePlatform p, string name)
    {
		_p = p;
		_name = (name == "") ? "World" : name;
	}

	public string GetName()
	{
		return wtxt_name.GetContent();
	}

	public bool GetPvP()
	{
		return wcbx_pvp.GetChecked();
	}

	public bool GetPvE()
	{
		return wcbx_pve.GetChecked();
	}
}
