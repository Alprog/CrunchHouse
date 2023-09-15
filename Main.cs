using Godot;

public partial class Main : Control
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

	public void OnCloseButtonPressed()
	{
		GetTree().Quit();
	}
}
