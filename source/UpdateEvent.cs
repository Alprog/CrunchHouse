
using Godot;

public partial class UpdateEvent : InputEventKey
{
    public float DeltaTime;

    public UpdateEvent(double deltaTime)
    {
        this.DeltaTime = (float)deltaTime;
    }
}