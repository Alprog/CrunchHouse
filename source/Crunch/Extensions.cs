

using System;
using System.Reflection;
using Godot;

namespace Crunch
{
    public static partial class Extensions
    {   
        public static ConstructorInfo GetDefaultConstructor(this Type type)
        {
            var flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            return type.GetTypeInfo().GetConstructor(flags, null, EmptyArray<Type>.Value, null);
        }

        public static object InvokeDefaultConstructor(this Type type)
        {
            return type.GetDefaultConstructor().Invoke(EmptyArray<object>.Value);
        }

        public static Vector2I GetSize(this Viewport viewport)
        {
            if (viewport is SubViewport subViewport)
            {
                return subViewport.Size;
            }
            if (viewport is Godot.Window window)
            {
                return window.Size;
            }
            return Vector2I.Zero;
        }

        public static bool IsMouseAtWindow(this Viewport viewport)
        {
            var rect = new Rect2I(Vector2I.Zero, viewport.GetSize());
            return rect.HasPoint(viewport.GetMousePosition().ToVector2I());
        }
    }
}