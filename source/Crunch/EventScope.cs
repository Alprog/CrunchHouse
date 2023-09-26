using Godot;
using Crunch;

public partial class EventScope : SubViewportContainer
{
	public void ProcessEvent(InputEvent inputEvent)
	{
		var sub = this.Find<SubViewport>();
		sub.HandleInputLocally = true;
		sub.PushInput(inputEvent, true);
		if (sub.IsInputHandled())
		{
			GetViewport().SetInputAsHandled();
		}
	}
}
