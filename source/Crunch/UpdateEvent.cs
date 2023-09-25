
using Godot;

namespace Crunch
{
    public partial class UpdateEvent : InputEventAction
    {
        public float DeltaTime;

        public UpdateEvent(float deltaTime)
        {
            this.DeltaTime = deltaTime;
        }
    }
}