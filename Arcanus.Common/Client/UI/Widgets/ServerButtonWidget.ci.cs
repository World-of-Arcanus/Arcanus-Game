public class ServerButtonWidget : AbstractMenuWidget
{
	string _name;
	string _motd;
	string _gamemode;
	string _playercount;
	ButtonState _state;
	ButtonState _stateLast;
	MainMenu _menu;

	string _imagename;
	bool _errorVersion;
	bool _errorConnect;

	TextWidget _textHeading;
	// TextWidget _textGamemode;
	// TextWidget _textPlayercount;
	TextWidget _textDescription;
	FontCi _fontServerHeading;
	FontCi _fontServerDescription;

	public ServerButtonWidget()
	{
		x = 0;
		y = 0;
		sizex = 0;
		sizey = 0;
		clickable = true;
		focusable = true;

		_name = null;
		_motd = null;
		_gamemode = null;
		_playercount = null;
		_state = ButtonState.Normal;

		_imagename = "serverlist_entry_noimage.png";
		_errorVersion = false;
		_errorConnect = false;

		_fontServerHeading = new FontCi();
		_fontServerHeading.style = 1;
		_fontServerHeading.size = 14;

		_fontServerDescription = new FontCi();
		_fontServerDescription.size = 10;

		_textHeading = new TextWidget();
		_textHeading.SetFont(_fontServerHeading);
		_textHeading.SetAlignment(TextAlign.Left);
		_textHeading.SetBaseline(TextBaseline.Top);

		// _textGamemode = new TextWidget();
		// _textGamemode.SetFont(_fontServerDescription);
		// _textGamemode.SetAlignment(TextAlign.Right);
		// _textGamemode.SetBaseline(TextBaseline.Bottom);

		// _textPlayercount = new TextWidget();
		// _textPlayercount.SetFont(_fontServerDescription);
		// _textPlayercount.SetAlignment(TextAlign.Right);
		// _textPlayercount.SetBaseline(TextBaseline.Top);

		_textDescription = new TextWidget();
		_textDescription.SetFont(_fontServerDescription);
		_textDescription.SetAlignment(TextAlign.Left);
		_textDescription.SetBaseline(TextBaseline.Bottom);
	}

	public override void Draw(float dt, UiRenderer renderer)
	{
		if (!visible) { return; }
		if (sizex <= 0 || sizey <= 0) { return; }

		switch (_state)
		{
			case ButtonState.Normal:
				renderer.Draw2dTexture(renderer.GetTexture("serverlist_entry_background.png"), x, y, sizex, sizey, null, 0, color);

				if (_state != _stateLast)
				{
					renderer.GetPlatform().SetWindowCursor(0, 0, 32, 32,
						renderer.GetFile("mousecursor.png"),
						renderer.GetFileLength("mousecursor.png")
					);
				}

				break;
			case ButtonState.Hover:
				renderer.Draw2dTexture(renderer.GetTexture("serverlist_entry_background_sel.png"), x, y, sizex, sizey, null, 0, color);

				if (_state != _stateLast)
				{
					renderer.GetPlatform().SetWindowCursor(0, 0, 32, 32,
						renderer.GetFile("mousecursor-click.png"),
						renderer.GetFileLength("mousecursor-click.png")
					);
				}

				break;
		}

		if (_imagename != "none")
		{
			renderer.Draw2dTexture(renderer.GetTexture(_imagename), x, y, sizey, sizey, null, 0, color);
		}

		// display warnings if server is unreachable or uses a different version
		if (_errorConnect)
		{
			renderer.Draw2dTexture(renderer.GetTexture("serverlist_entry_noresponse.png"), x - 38 * renderer.GetScale(), y, sizey / 2, sizey / 2, null, 0, color);
		}
		if (_errorVersion)
		{
			renderer.Draw2dTexture(renderer.GetTexture("serverlist_entry_differentversion.png"), x - 38 * renderer.GetScale(), y + sizey / 2, sizey / 2, sizey / 2, null, 0, color);
		}

		// highlight text if button is selected
		// if (hasKeyboardFocus)
		// {
		// 	_textHeading.SetText(StringTools.StringAppend(renderer.GetPlatform(), "&2", _name));
		// 	// _textGamemode.SetText(StringTools.StringAppend(renderer.GetPlatform(), "&2", _gamemode));
		// 	// _textPlayercount.SetText(StringTools.StringAppend(renderer.GetPlatform(), "&2", _playercount));
		// 	_textDescription.SetText(StringTools.StringAppend(renderer.GetPlatform(), "&2", _motd));
		// }
		// else
		// {
			_textHeading.SetText(_name);
			// _textGamemode.SetText(_gamemode);
			// _textPlayercount.SetText(_playercount);
			_textDescription.SetText(_motd);
		// }

		float scale = renderer.GetScale();

		_textHeading.x = x + 73 * scale;
		_textHeading.y = y + 10 + 25 * scale;
		_textHeading.Draw(dt, renderer);

		// _textGamemode.x = x + sizex - 10 * scale;
		// _textGamemode.y = y + sizey - 5 * scale;
		// _textGamemode.Draw(dt, renderer);

		// _textPlayercount.x = x + sizex - 10 * scale;
		// _textPlayercount.y = y + 5 * scale;
		// _textPlayercount.Draw(dt, renderer);

		if (_name == "")
		{
			_textDescription.x = x + (this.GetSizeX() / 2) - (_textDescription.GetSizeX() / 2);

			FontCi emptySlotFont = new FontCi();
			emptySlotFont.size = 12;

			_textDescription.SetFont(emptySlotFont);
			_textDescription.y = y + sizey - 40 * scale;
		}
		else
		{
			_textDescription.SetFont(_fontServerDescription);
			_textDescription.x = x + 73 * scale;
			_textDescription.y = y + sizey - 25 * scale;
		}

		_textDescription.Draw(dt, renderer);
	}
	public override void OnMouseDown(GamePlatform p, MouseEventArgs args)
	{
		if (HasBeenClicked(args))
		{
			// hasKeyboardFocus = true;
			this._menu.StartGameSettings(this._name, false);
		}
		else
		{
			// hasKeyboardFocus = false;
		}
	}

	public override void OnMouseMove(GamePlatform p, MouseEventArgs args)
	{
		// Check if mouse is inside the button rectangle
		if (IsCursorInside(args))
		{
			if (_state == ButtonState.Normal)
			{
				SetState(ButtonState.Hover);
			}
		}
		else
		{
			SetState(ButtonState.Normal);
		}
	}

	public ButtonState GetState()
	{
		return _state;
	}
	public void SetState(ButtonState state)
	{
		ButtonState last = _state;
		_state = state;
		_stateLast = last;
	}

	public string GetTextHeading()
	{
		return _name;
	}
	public void SetTextHeading(string text)
	{
		_name = text;
	}

	public string GetTextGamemode()
	{
		return _gamemode;
	}
	public void SetTextGamemode(string text)
	{
		_gamemode = text;
	}

	public string GetTextPlayercount()
	{
		return _playercount;
	}
	public void SetTextPlayercount(string text)
	{
		_playercount = text;
	}

	public string GetTextDescription()
	{
		return _motd;
	}
	public void SetTextDescription(string text)
	{
		_motd = text;
	}

	public string GetThumbnail()
	{
		return _imagename;
	}
	public void SetThumbnail(string image)
	{
		if (image == null || image == "")
		{
			_imagename = "serverlist_entry_noimage.png";
		}
		else
		{
			_imagename = image;
		}
	}

	public bool GetErrorConnect()
	{
		return _errorConnect;
	}
	public void SetErrorConnect(bool error)
	{
		_errorConnect = error;
	}

	public bool GetErrorVersion()
	{
		return _errorVersion;
	}

	public void SetErrorVersion(bool error)
	{
		_errorVersion = error;
	}

	public void SetMenu(MainMenu menu)
	{
		_menu = menu;
	}
}
