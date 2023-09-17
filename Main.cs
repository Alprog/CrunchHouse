
using Godot;

public partial class Main : Control
{
	[Export] Window SecondWindow;
	
	public override void _Process(double delta)
	{
		//var @event = new UpdateEvent(delta);
		//Input.ParseInputEvent(@event);
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
