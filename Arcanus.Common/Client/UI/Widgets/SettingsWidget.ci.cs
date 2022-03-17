public class SettingsWidget : AbstractMenuWidget
{
	public SettingsWidget()
	{
		clickable = true;
	}

	public override void Draw(float dt, UiRenderer renderer)
	{
		if (!visible) { return; }
		if (sizex <= 0 || sizey <= 0) { return; }

		const int padding = 6;
		const int elementSizeY = 64;
		float scale = renderer.GetScale();

		renderer.Draw2dTexture(renderer.GetTexture("serverlist_entry_background.png"), x, y, sizex, sizey, null, 0, color);
	}
	public override void OnMouseDown(GamePlatform p, MouseEventArgs args)
	{
		if (!HasBeenClicked(args)) { return; }

		// TODO
	}

	public override void OnMouseMove(GamePlatform p, MouseEventArgs args)
	{
		// TODO
	}
}
