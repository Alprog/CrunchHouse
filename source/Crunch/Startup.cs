using System;
using System.Collections.Generic;
using Godot;

namespace Crunch
{
	public partial class Startup : Control
	{
		public List<WindowRoot> Windows;

		public override void _Ready()
		{
			SpawnScene("world");
			var root = SpawnScene("window_root") as WindowRoot;
			//SpawnScene("second_window");
		}

		public Node SpawnScene(string sceneName)
		{
			var path = String.Format("res://scenes/{0}.tscn", sceneName);
			var scene = GD.Load<PackedScene>(path);
			var instance = scene.Instantiate();
			AddChild(instance);
			return instance;
		}

	}
}