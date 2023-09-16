using Godot;
using System;

public partial class SecondWindow : Window
{
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
		if (Visible && IsMouseAtWindow())
		{
			if (!DisplayServer.WindowIsFocused(this.GetWindowId()))
			{
				this.GrabFocus();
			}
		}
	}

	private bool IsMouseAtWindow()
	{
		var rect = new Rect2I(Vector2I.Zero, Size);
		return rect.HasPoint(GetMousePosition().ToVector2I());
	}

	public void OnCloseButtonPressed()
	{
		Visible ^= true;
	}
}
