
using Godot;

namespace Crunch
{
    public partial class Window : Godot.Window
    {
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

        public void Update(float deltaTime)
        {
            var managers = this.FindAll<CameraManager>();
            foreach (var manager in managers)
            {
                if (manager.GetViewport().IsMouseAtWindow())
                {
                    manager.ProcessInput(deltaTime);
                    break;
                }
            }
        }
    }
}
