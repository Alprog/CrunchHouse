using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class CameraManager : Node3D
{
	[Export] public int I;

	public Vector3 FocusPosition;

	public const float PanningScreenPerSecond = 1.0f;

	public const float MinCellCount = 10.0f;
	public const float MaxCellCount = 80.0f;
	public float ZoomK = 0.5f;

	public const float MinAngle = Mathf.Pi * 0.20f;
	public const float MaxAngle = Mathf.Pi * 0.49f;
	public float AngleK = 0.5f;

	[Export] public Camera3D Camera { get; set; }

	public float VisibleAreaLength => Mathf.Lerp(MaxCellCount, MinCellCount, ZoomK);
	public float Angle => Mathf.Lerp(MinAngle, MaxAngle, AngleK);

	public bool IsOrthogonal = true;
	public float FieldOfView = Mathf.Pi / 18.0f;

	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
		ProcessInput((float)delta);
		RefreshCameraPosition();
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
	}

	public void RefreshCameraPosition()
	{
		var OffsetDirection = new Vector3(0, Mathf.Sin(Angle), Mathf.Cos(Angle));
		
		Camera.Projection = IsOrthogonal ? Camera3D.ProjectionType.Orthogonal : Camera3D.ProjectionType.Perspective;
		Camera.Size = VisibleAreaLength * Mathf.Sin(Angle);
		Camera.Fov = FieldOfView;

		GD.Print(VisibleAreaLength);

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
