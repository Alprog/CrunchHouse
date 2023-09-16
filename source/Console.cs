using DarkCrystal.CommandLine;
using Godot;
using System;
using System.Collections.Generic;

public partial class Console : VSplitContainer
{
	public CommandLine CommandLine = new CommandLine(new DefaultGlobalResolver());

	private LineEdit CommandLineEdit => FindChild("CommandLineEdit") as LineEdit;
	private RichTextLabel Output => FindChild("Output") as RichTextLabel;

	private ConsoleHistory History = new ConsoleHistory();
	private bool SkipNextAutoComplete = false;

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventKey eventKey)
		{
			if (@event.IsPressed())
			{
				switch (eventKey.Keycode)
				{
					case Key.Quoteleft:
						Toggle();
						AcceptEvent();
						break;

					case Key.Enter:
						History.Add(CommandLineEdit.Text);
						Output.Text += "\n" + CommandLineEdit.Text;
						Output.ScrollToLine(Int32.MaxValue);
						CommandLineEdit.Clear();
						AcceptEvent();
						break;

					case Key.Tab:
						CommandLineEdit.Deselect();
						CommandLineEdit.CaretColumn = CommandLineEdit.Text.Length;
						AcceptEvent();
						break;

					case Key.Up:
						if (!History.Empty)
						{
							CommandLineEdit.Text = History.MoveBack();
							SetCaretToEnd();
							AcceptEvent();						
						}
						break;

					case Key.Down:
						if (!History.Empty)
						{
							CommandLineEdit.Text = History.MoveForward();
							SetCaretToEnd();
							AcceptEvent();
						}
						break;

					case Key.Delete:
					case Key.Backspace:
						SkipNextAutoComplete = true;
						break;
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
			CommandLineEdit.GrabFocus();
		}
		else
		{
			CommandLineEdit.ReleaseFocus();
		}		
	}

	private static int EditingFromCode = 0;

	private void OnCommandLineEditChanged(String text)
	{
		if (EditingFromCode == 0)
		{
			PerformAutoComplete();
		}
	}	

	private void PerformAutoComplete()
	{
		if (SkipNextAutoComplete)
		{
			SkipNextAutoComplete = false;
			return;
		}

		EditingFromCode++;
		PerformAutoCompleteInternal();
		EditingFromCode--;
	}

	private void PerformAutoCompleteInternal()
	{
		var baseText = CommandLineEdit.Text;
		var completedText = CommandLine.AutoComplete(baseText);
		if (completedText != String.Empty)
		{
			CommandLineEdit.Text = baseText + completedText;
			CommandLineEdit.Select(baseText.Length);
			SetCaretToEnd();
		}
	}

	private void SetCaretToEnd()
	{
		CommandLineEdit.CaretColumn = CommandLineEdit.Text.Length;
	}
}
