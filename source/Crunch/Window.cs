
using Godot;

namespace Crunch
{
    public partial class Window : Godot.Window
    {
        EventScope TopMenuLayer => this.Find<EventScope>("TopMenuLayer");
        Console Console => this.Find<Console>();
        EventScope LowLevelLayer => this.Find<EventScope>("LowLevelLayer");

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
            ProcessGlobalShortcuts(inputEvent);

            if (!IsInputHandled())
            {
                TopMenuLayer.ProcessEvent(inputEvent);
            }

            if (!IsInputHandled())
            {
                if (Console.Visible)
                {
                    Console.ProcessEvent(inputEvent);
                }
            }

            if (!IsInputHandled())
            {
                LowLevelLayer.ProcessEvent(inputEvent);
            }

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

        public bool ProcessGlobalShortcuts(InputEvent inputEvent)
        {
            if (inputEvent is InputEventKey eventKey)
			{
				if (eventKey.IsPressed() && eventKey.Keycode == Key.Quoteleft)
				{
					Console.Toggle();
                    SetInputAsHandled();
                    return true;
                }
            }
            return false;
        }
    }
}
