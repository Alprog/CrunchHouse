
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

        public void ProcessEvent(InputEvent inputEvent)
        {
            var console = this.Find<Console>("Console");
            console.ProcessEvent(inputEvent);

            if (!IsInputHandled())
            {
                var managers = this.FindAll<CameraManager>();
                foreach (var manager in managers)
                {
                    if (manager.GetViewport().IsMouseAtWindow())
                    {
                        manager.ProcessEvent(inputEvent);
                        break;
                    }
                }       
            }

            SetInputAsHandled();
        }

        public void Update(float deltaTime)
        {
   
        }
    }
}
