using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class CameraManager : Node3D
{
	[Export] public int I;

	public Vector3 FocusPosition;

	public const float PanningScreenPerSecond = 1.0f;

	public const float MinCellCount = 10.0f;
	public const float MaxCellCount = 160.0f;
	public float ZoomK = 0.5f;
	public int ZoomStepCount = 20;

	public const float MinAngle = Mathf.Pi * 0.20f;
	public const float MaxAngle = Mathf.Pi * 0.49f;
	public float AngleK = 0.5f;

	public const float MinFov = Mathf.Pi / 30.0f;
	public const float MaxFov = Mathf.Pi / 3.0f;
	public float FovK = 0.5f;

	[Export] public Camera3D Camera { get; set; }

	public float VisibleAreaLength => Mathf.Lerp(MaxCellCount, MinCellCount, Mathf.Sqrt(ZoomK));
	public float Angle => Mathf.Lerp(MinAngle, MaxAngle, AngleK);
	public float FieldOfView => Mathf.Lerp(MinFov, MaxFov, FovK);

	public bool IsOrthogonal = true;

	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
		ProcessInput((float)delta);
		RefreshCameraPosition();
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		var mouseEvent = @event as InputEventMouseButton;
		if (mouseEvent != null && mouseEvent.IsPressed())
		{
			if (mouseEvent.ButtonIndex == MouseButton.WheelUp)
			{
				ZoomK += 1.0f / ZoomStepCount;
			}
			else if (mouseEvent.ButtonIndex == MouseButton.WheelDown)
			{
				ZoomK -= 1.0f / ZoomStepCount;
			}
		}
	}

	public void ProcessInput(float delta)
	{
		var Direction = Vector3.Zero;

		if (Input.IsKeyPressed(Key.W))
		{
			Direction += Vector3.Forward;
		}
		if (Input.IsKeyPressed(Key.S))
		{
			Direction += Vector3.Back;
		}
		if (Input.IsKeyPressed(Key.A))
		{
			Direction += Vector3.Left;
		}
		if (Input.IsKeyPressed(Key.D))
		{
			Direction += Vector3.Right;
		}

		if (Input.IsKeyPressed(Key.Q))
		{
			ZoomK += delta / 2;
		}
		if (Input.IsKeyPressed(Key.E))
		{
			ZoomK -= delta / 2;
		}
		ZoomK = Mathf.Clamp(ZoomK, 0, 1);

		FocusPosition += Direction * VisibleAreaLength * PanningScreenPerSecond * delta;

		if (Input.IsKeyPressed(Key.Z))
		{
			AngleK += delta / 2;
		}
		if (Input.IsKeyPressed(Key.X))
		{
			AngleK -= delta / 2;
		}
		AngleK = Mathf.Clamp(AngleK, 0, 1);

		if (Input.IsKeyPressed(Key.Key1))
		{
			IsOrthogonal = false;
		}
		if (Input.IsKeyPressed(Key.Key2))
		{
			IsOrthogonal = true;
		}

		if (Input.IsKeyPressed(Key.O))
		{
			FovK += delta / 2;
		}
		if (Input.IsKeyPressed(Key.P))
		{
			FovK -= delta / 2;
		}
		FovK = Mathf.Clamp(FovK, 0, 1);
	}

	public void RefreshCameraPosition()
	{
		var OffsetDirection = new Vector3(0, Mathf.Sin(Angle), Mathf.Cos(Angle));
		
		Camera.Projection = IsOrthogonal ? Camera3D.ProjectionType.Orthogonal : Camera3D.ProjectionType.Perspective;
		Camera.Size = VisibleAreaLength * Mathf.Sin(Angle);
		Camera.Fov = Mathf.RadToDeg(FieldOfView);

		//GD.Print(VisibleAreaLength);

		var Distance = IsOrthogonal ? 80.0f : CalculateDistance();
		Camera.Position = FocusPosition + OffsetDirection * Distance;

		
		Camera.LookAt(FocusPosition);
	}

	public float CalculateDistance()
	{
		var viewAngle = Angle;

		var nearAngle = viewAngle + FieldOfView / 2;
		var farAngle = viewAngle - FieldOfView / 2;
		
		var nearXperY = Mathf.Cos(nearAngle) / Mathf.Sin(nearAngle);
		var farXperY = Mathf.Cos(farAngle) / Mathf.Sin(farAngle);

		// (farXperY - nearXperY) * Y = VisibleAreaLength
		var y = VisibleAreaLength / (farXperY - nearXperY);
		return y / Mathf.Sin(viewAngle);
	}
}
