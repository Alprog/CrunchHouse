

using System;
using System.Collections.Generic;
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

        public static T Find<T>(this Node node, string name = null) where T : Node
        {
            for (int i = 0; i < node.GetChildCount(); i++)
            {
                var child = node.GetChild(i);
                if (name == null || child.Name == name)
                {
                    if (child is T casted)
                    {
                        return casted;
                    }
                }

                var result = Find<T>(child, name);
                if (result != null)
                {
                    return result;
                }
            }
            return null;
        }

        public static List<T> FindAll<T>(this Node node, string name = null) where T : Node
        {
            var list = new List<T>();
            node.FindAll(ref list, name);
            return list;
        }

        public static void FindAll<T>(this Node node, ref List<T> list, string name = null) where T : Node
        {
            if (list == null)
            {
                list = new List<T>();
            }
            for (int i = 0; i < node.GetChildCount(); i++)
            {
                var child = node.GetChild(i);
                if (name == null || child.Name == name)
                {
                    if (child is T casted)
                    {
                        list.Add(casted);
                    }
                }

                FindAll(child, ref list, name);
            }
        }
    }
}