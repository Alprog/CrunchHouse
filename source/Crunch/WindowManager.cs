
using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

namespace Crunch
{
    public class WindowManager : Singleton<WindowManager>
    {
        private List<Window> Windows = new List<Window>();
        private Window FocusedWindow = null;

        public Window CreateNewWindow()
        {
            var window = Utils.InstantinateScene("window") as Window;
            window.CurrentScreen = Windows.Count;
            window.Name = String.Format("Window{0}", Windows.Count + 1);
            Windows.Add(window);           
            The.Application.GetNode().AddChild(window);
            window.Initialize();
            return window;
        }

        public void CloseWindow(Window window)
        {
            Windows.Remove(window);
            window.QueueFree();
            if (Windows.Count == 0)
            {
                The.Application.Quit();
            }
        }

        public void RefreshFocusedWindow()
        {
            foreach (var window in Windows)
            {
                if (window.IsMouseAtWindow())
                {
                    SetFocusedWindow(window);
                    return;
                }
            }
            SetFocusedWindow(Windows.First());
        }

        private void SetFocusedWindow(Window window)
        {
            if (!DisplayServer.WindowIsFocused(window.GetWindowId()))
            {
                window.GrabFocus();
            }
            FocusedWindow = window;
        }
    }
}
