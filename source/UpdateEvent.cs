
using Godot;

public partial class UpdateEvent : InputEventAction
{
    public float DeltaTime;

    public UpdateEvent(double deltaTime)
    {
        this.DeltaTime = (float)deltaTime;
    }
}