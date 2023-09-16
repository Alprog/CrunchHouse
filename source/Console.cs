using Godot;
using System;

public partial class Console : VSplitContainer
{
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventKey eventKey)
		{
			if (@event.IsPressed() && eventKey.Keycode == Key.Quoteleft)
			{
				Toggle();
				AcceptEvent();
			}
		}
	}

	public override void _GuiInput(InputEvent @event)
	{
		if (Visible)
		{
			AcceptEvent();
		}
	}

	public void Toggle()
	{
		var commandLine = FindChild("CommandLine") as LineEdit;
		Visible ^= true;
		if (Visible)
		{
			commandLine.GrabFocus();
		}
		else
		{
			commandLine.ReleaseFocus();
		}		
	}
}
