
using Godot;

namespace Crunch
{
    public partial class UpdateEvent : InputEventAction
    {
        public float DeltaTime;
        public bool Processed;    

        public UpdateEvent(double deltaTime)
        {
            this.DeltaTime = (float)deltaTime;
        }
    }
}