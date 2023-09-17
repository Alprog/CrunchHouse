
using Godot;

namespace Crunch
{
    public partial class Window : Control
    {
        public Godot.Window GodotWindow => GetWindow();
        public bool IsMain => GodotWindow == GetTree().Root;

        public void Initialize()
        {
            //var console = Utils.InstantinateScene("console") as Console;
            //console.Visible = false;
            //AddChild(console);
        }

        public void OnCloseButtonPressed()
        {
            The.WindowManager.CloseWindow(this);
        }
    }
}
