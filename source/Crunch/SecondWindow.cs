using Godot;
using System;

namespace Crunch
{
	public partial class SecondWindow : Godot.Window
	{
		public override void _Ready()
		{
		}

		public override void _Process(double delta)
		{
			if (Visible && this.IsMouseAtWindow())
			{
				if (!DisplayServer.WindowIsFocused(this.GetWindowId()))
				{
					this.GrabFocus();
				}
			}
		}

		public void OnCloseButtonPressed()
		{
			Visible ^= true;
		}
	}
}