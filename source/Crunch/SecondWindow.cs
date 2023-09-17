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

		}

		public void OnCloseButtonPressed()
		{
			Visible ^= true;
		}
	}
}