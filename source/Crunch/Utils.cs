
using Godot;
using System;

namespace Crunch
{
    public static class Utils
    {
        public static Node InstantinateScene(string sceneName)
        {
            var path = String.Format("res://scenes/{0}.tscn", sceneName);
            var scene = GD.Load<PackedScene>(path);
            return scene.Instantiate();
        }
    }
}