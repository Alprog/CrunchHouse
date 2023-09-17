using Godot;
using System;

namespace Crunch
{
	public partial class WindowRoot : Node
	{
		public override void _Process(double delta)
		{
			GD.Print(this.GetViewport().Name);
		}

		public override void _Input(InputEvent @event)
		{
			//AcceptEvent();
		}

		public void OnCloseButtonPressed()
		{
			GetTree().Quit();
		}	
	}
}
