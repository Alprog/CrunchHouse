using DarkCrystal.CommandLine;
using Godot;
using System;
using System.Collections.Generic;

namespace Crunch
{
	public partial class Console : EventScope
	{
		private LineEdit CommandLineEdit => FindChild("CommandLineEdit") as LineEdit;
		private RichTextLabel Output => FindChild("Output") as RichTextLabel;

		private ConsoleHistory History = new ConsoleHistory();
		private bool SkipNextAutoComplete = false;

		public void ProcessEvent(InputEvent @event)
		{
			if (@event is InputEventKey eventKey)
			{
				if (eventKey.IsPressed())
				{
					switch (eventKey.Keycode)
					{
						case Key.Quoteleft:
							Toggle();
							AcceptEvent();
							break;

						case Key.Enter:
							History.Add(CommandLineEdit.Text);
							Run(CommandLineEdit.Text);
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
		
			var sub = this.Find<SubViewport>();
			sub.HandleInputLocally = true;
			sub.PushInput(@event, true);

			GetWindow().SetInputAsHandled();
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

		private void Run(string line)
		{
			Output.Text += "\n > " + line; 

			string ResultString = null;
			try
			{
				ResultString = The.CommandLine.Execute(line)?.ToString();
			}
			catch (Exception exception)
			{
				ResultString = exception.InnerException?.Message ?? exception.Message;
			}
			Output.Text += "\n" + ResultString;
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
			var completedText = The.CommandLine.AutoComplete(baseText);
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
}