using Godot;
using System;

public partial class Console : VSplitContainer
{
	private LineEdit CommandLine => FindChild("CommandLine") as LineEdit;
	private RichTextLabel Output => FindChild("Output") as RichTextLabel;
	
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventKey eventKey)
		{
			if (@event.IsPressed())
			{
				if (eventKey.Keycode == Key.Quoteleft)
				{
					Toggle();
					AcceptEvent();
				}
				if (eventKey.Keycode == Key.Enter)
				{
					Output.Text += "\n" + CommandLine.Text;
					Output.ScrollToLine(Int32.MaxValue);
					CommandLine.Clear();
					AcceptEvent();
				}
			}			
		}

		if (Visible)
		{
			if (@event is UpdateEvent @updateEvent)
			{
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
		Visible ^= true;
		if (Visible)
		{
			CommandLine.GrabFocus();
		}
		else
		{
			CommandLine.ReleaseFocus();
		}		
	}
}
