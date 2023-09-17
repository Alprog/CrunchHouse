
using System.Collections.Generic;
using System.Linq;
using Godot;

namespace Crunch
{
    public class ConsoleHistory
    {
        private List<string> Lines = new List<string>();
        private int Position = 0;

        public void Add(string line)
        {
            if (Lines.Count == 0 || Lines.Last() != line)
            {
                Lines.Add(line);
            }
            Position = Lines.Count;
        }

        public bool Empty => Lines.Count == 0;

        public string MoveBack()
        {
            return Move(-1);
        }

        public string MoveForward()
        {
            return Move(1);
        }

        private string Move(int offset)
        {
            Position = Mathf.Clamp(Position + offset, 0, Lines.Count - 1);
            return Lines[Position];
        }
    }
}