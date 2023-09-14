using Godot;

public partial class Main : Node2D
{
	[Export] Window SecondWindow;
	
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}

	public void OnWindowButtonPressed()
	{
		SecondWindow.Visible ^= true;
	}
}
