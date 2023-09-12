using Godot;
using System;

public partial class FPSLabel : Label
{
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
		Text = Engine.GetFramesPerSecond().ToString();
	}
}
