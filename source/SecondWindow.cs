using Godot;
using System;

public partial class SecondWindow : Window
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
