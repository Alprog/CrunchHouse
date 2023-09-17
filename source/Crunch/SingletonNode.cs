
using Godot;

namespace Crunch
{
    public partial class SingletonNode<T> : Node where T : Node
    {
        public static T Instance { get; private set; }

        public override void _Ready()
        {
            Instance = this as T;
        }
    }
}
