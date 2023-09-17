
using Godot;

namespace Crunch
{
	public partial class Application : SingletonNode<Application>
	{
		public override void _Ready()
		{
			base._Ready();
			AddChild(Utils.InstantinateScene("world"));
			The.WindowManager.CreateNewWindow();
			The.WindowManager.CreateNewWindow();
		}

		public override void _Process(double delta)
		{
			float deltaTime = (float)delta;
			The.WindowManager.Update(deltaTime);
		}

		public Node GetNode()
		{
			return this;
		}

		public void Quit()
		{
			GetTree().Quit();
		}
	}
}