
using Godot;

namespace Crunch
{
	public partial class Application : SingletonNode<Application>
	{
		public World World { get; private set; }

		public override void _Ready()
		{
			DisplayServer.WindowSetSize(Vector2I.One, GetWindow().GetWindowId());

			base._Ready();
			World = Utils.InstantinateScene("world") as World;
			AddChild(World);
			The.WindowManager.CreateNewWindow();
			The.WindowManager.CreateNewWindow();
		}

		public override void _Process(double delta)
		{
			float deltaTime = (float)delta;
			The.WindowManager.Update(deltaTime);
			World.Update(deltaTime);
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