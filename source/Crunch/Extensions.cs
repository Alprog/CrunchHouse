

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

        public static bool IsMouseAtWindow(this SubViewport viewport)
        {

            var rect = new Rect2I(Vector2I.Zero, viewport.Size);
            return rect.HasPoint(viewport.GetMousePosition().ToVector2I());
        }

        public static bool IsMouseAtWindow(this Godot.Window window)
        {
            var rect = new Rect2I(Vector2I.Zero, window.Size);
            return rect.HasPoint(window.GetMousePosition().ToVector2I());
        }
    }
}