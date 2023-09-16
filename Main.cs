using Godot;

public partial class Main : Control
{
	[Export] Window SecondWindow;
	
	public void OnWindowButtonPressed()
	{
		SecondWindow.Visible ^= true;
	}

	public void OnCloseButtonPressed()
	{
		GetTree().Quit();
	}
}
