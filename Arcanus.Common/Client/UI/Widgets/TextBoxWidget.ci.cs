public class TextBoxWidget : AbstractMenuWidget
{
	TextWidget _text;
	FontCi _inputFont;
	string _textureNameInactive;
	string _textureNameActive;

	string _placeholderText;
	const string _placeholderColorCode = "&7";
	const string _typingChar = "_";

	bool _hideInput;
	string _textContent;
	string _textDisplay;
	TextAlign _align;

	public TextBoxWidget()
	{
		clickable = true;
		focusable = true;
		_textContent = "";
		_textDisplay = "";
		_textureNameInactive = "textbox.png";
		_textureNameActive = "textbox_sel.png";

		_inputFont = new FontCi();
		_text = new TextWidget();
		_text.SetBaseline(TextBaseline.Middle);
		_text.SetFont(_inputFont);
		_text.SetText(_placeholderColorCode + "Input");
	}

	public override void OnMouseDown(GamePlatform p, MouseEventArgs args)
	{
		SetFocused(HasBeenClicked(args));
	}

	public override void OnKeyPress(GamePlatform p, KeyPressEventArgs args)
	{
		if (hasKeyboardFocus)
		{
			if (p.IsValidTypingChar(args.GetKeyChar()))
			{
				SetContent(p, StringTools.StringAppend(p, _textContent, CharToString(p, args.GetKeyChar())));
			}
		}
		else
		{
			DefaultOnKeyPress(p, args);
		}
	}

	public override void OnKeyDown(GamePlatform p, KeyEventArgs args)
	{
		DefaultOnKeyDown(p, args);

		if (hasKeyboardFocus)
		{
			int key = args.GetKeyCode();

			// pasting text from clipboard
			if (args.GetCtrlPressed() && key == GlKeys.V)
			{
				if (p.ClipboardContainsText())
				{
					SetContent(p, StringTools.StringAppend(p, _textContent, p.ClipboardGetText()));
				}
				return;
			}

			// deleting characters using backspace
			if (key == GlKeys.BackSpace)
			{
				if (StringTools.StringLength(p, _textContent) > 0)
				{
					SetContent(p, StringTools.StringSubstring(p, _textContent, 0, StringTools.StringLength(p, _textContent) - 1));
				}
				return;
			}
		}
	}

	public override void Draw(float dt, UiRenderer renderer)
	{
		if (!visible) { return; }
		if (sizex <= 0 || sizey <= 0) { return; }

		renderer.Draw2dTexture(renderer.GetTexture(hasKeyboardFocus ? _textureNameActive : _textureNameInactive), x, y, sizex, sizey, null, 0, color);

		if (hasKeyboardFocus)
		{
			_text.SetText(StringTools.StringAppend(renderer.GetPlatform(), _textDisplay, _typingChar));
		}
		else
		{
			_text.SetText(_textDisplay);
		}

		_text.x = x;

		switch (_align)
		{
			case TextAlign.Left:
				break;
			case TextAlign.Center:
				_text.x += sizex / 2;
				break;
			case TextAlign.Right:
				_text.x += sizex - _text.sizex;
				break;
		}

		_text.y = y + (sizey / 2);

		_text.Draw(dt, renderer);
	}

	public override string GetEventResponse()
	{
		return _textContent;
	}

	public void SetInputHidden(bool hideInput)
	{
		_hideInput = hideInput;
	}

	public void SetContent(GamePlatform p, string c)
	{
		_textContent = (c != null) ? c : "";

		if (_hideInput)
		{
			// replace display text with '*' if display mode is hidden
			_textDisplay = (p != null) ? CharRepeat(p, 42, StringTools.StringLength(p, _textContent)) : "*hidden*";
		}
		else
		{
			_textDisplay = _textContent;
		}

		_text.SetText(_textDisplay);
	}
	public string GetContent()
	{
		return _textContent;
	}

	string CharToString(GamePlatform p, int a)
	{
		int[] arr = new int[1];
		arr[0] = a;

		return p.CharArrayToString(arr, 1);
	}
	string CharRepeat(GamePlatform p, int c, int length)
	{
		int[] charArray = new int[length];

		for (int i = 0; i < length; i++)
		{
			charArray[i] = c;
		}

		return p.CharArrayToString(charArray, length);
	}

	public void SetAlignment(TextAlign align)
	{
		_align = align;
	}
}
