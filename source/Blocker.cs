using Godot;
using Crunch;

public partial class Blocker : Control
{
	public override void _Input(InputEvent @event)
	{
		var window = GetViewport() as Crunch.Window;
		window.ProcessEvent(@event);
	}
}
