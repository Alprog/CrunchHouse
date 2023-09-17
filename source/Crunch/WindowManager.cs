
using System.Collections.Generic;
using Godot;

namespace Crunch
{
    public class WindowManager : Singleton<WindowManager>
    {
        private List<Window> Windows = new List<Window>();
        private Window FocusedWindow = null;

        public Window CreateNewWindow()
        {
            var currentNode = The.Application.GetNode();
           
            if (Windows.Count > 0)
            {
                var godotWindow = new Godot.Window 
                { 
                    Name = "GodotWindow",
                    Mode = Godot.Window.ModeEnum.Fullscreen,
                    InitialPosition = Godot.Window.WindowInitialPosition.CenterOtherScreen,
                    CurrentScreen = 1
                };
                currentNode.AddChild(godotWindow);
                currentNode = godotWindow;
            }

            var window = Utils.InstantinateScene("window") as Window;
            window.Name = GetWindowName(Windows.Count);
            currentNode.AddChild(window);           
            Windows.Add(window);           
            window.Initialize();
            return window;
        }

        public void CloseWindow(Window window)
        {
            if (window.IsMain)
            {
                The.Application.Quit();
            }
            else
            {
                Windows.Remove(window);
                window.GodotWindow.QueueFree();
            }
        }

        private string GetWindowName(int index)
        {
            if (index == 0)
            {
                return "MainWindow";
            }
            else
            {
                return "ExternalWindow" + index;
            }
        }

        public void Update(float deltaTime)
        {
            RefreshFocusedWindow();
            FocusedWindow.Update(deltaTime);
        }

        public void RefreshFocusedWindow()
        {
            for (int i = Windows.Count - 1; i >= 0; i--)
            {
                var godotWindow = Windows[i].GodotWindow;
                if (godotWindow.IsMouseAtWindow())
                {
                    var id = godotWindow.GetWindowId();
                    if (!DisplayServer.WindowIsFocused(id))
                    {
                        godotWindow.GrabFocus();
                    }
                    FocusedWindow = Windows[i];
                    break;
                }
            }    
        }
    }
}
