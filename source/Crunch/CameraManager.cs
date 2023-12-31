using Godot;
using System;
using System.Runtime.CompilerServices;

namespace Crunch
{
	public partial class CameraManager : Control
	{
		public static int i = 0;
		public String NameTag;

		public Vector3 FocusPosition;
		
		public const float PanningScreenPerSecond = 1.0f;

		public const float MinCellCount = 10.0f;
		public const float MaxCellCount = 160.0f;
		public float ZoomK = 0.5f;
		public int ZoomStepCount = 20;

		public const float MinAngle = Mathf.Pi * 0.20f;
		public const float MaxAngle = Mathf.Pi * 0.49f;
		public float AngleK = 0.5f;

		public float Rotation = 0.0f;

		public const float MinFov = Mathf.Pi / 30.0f;
		public const float MaxFov = Mathf.Pi / 3.0f;
		public float FovK = 0.5f;

		[Export] public Camera3D Camera { get; set; }

		public float VisibleAreaLength => Mathf.Lerp(MaxCellCount, MinCellCount, Mathf.Sqrt(ZoomK));
		public float Angle => Mathf.Lerp(MinAngle, MaxAngle, AngleK);
		public float FieldOfView => Mathf.Lerp(MinFov, MaxFov, FovK);

		public bool IsOrthogonal = false;

		public override void _Ready()
		{
			NameTag = "Camera" + (++i).ToString();
		}

		public override void _Process(double delta)
		{
			RefreshCameraPosition();
		}

		public void ProcessEvent(InputEvent inputEvent)
		{		
			if (inputEvent is UpdateEvent updateEvent)
			{
				ProcessUpdate(updateEvent.DeltaTime);
				return;
			}

			var mouseEvent = @inputEvent as InputEventMouseButton;
			if (mouseEvent != null && mouseEvent.IsPressed())
			{
				if (mouseEvent.ButtonIndex == MouseButton.WheelUp)
				{
					ZoomK += 1.0f / ZoomStepCount;
					ZoomK = Mathf.Clamp(ZoomK, 0, 1);
				}
				else if (mouseEvent.ButtonIndex == MouseButton.WheelDown)
				{
					ZoomK -= 1.0f / ZoomStepCount;
					ZoomK = Mathf.Clamp(ZoomK, 0, 1);
				}
			}
		}

		public void ProcessUpdate(float delta)
		{
			ProcessPressedKeys(delta);
			SetWorldHoverRay();
		}

		private void ProcessPressedKeys(float delta)
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

			Direction = Direction.Rotated(Vector3.Up, Rotation);

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

			if (Input.IsKeyPressed(Key.Y))
			{
				Rotation += Mathf.Pi * delta / 4;
			}
			if (Input.IsKeyPressed(Key.U))
			{
				Rotation -= Mathf.Pi * delta / 4;
			}
		}

		private void SetWorldHoverRay()
		{
			var screenPosition = GetViewport().GetMousePosition();
			var origin = Camera.ProjectRayOrigin(screenPosition);
			var direction = Camera.ProjectRayNormal(screenPosition);
			The.Application.World.HoverRay = new Ray(origin, direction);
		}

		public void RefreshCameraPosition()
		{
			var x = Mathf.Cos(Angle) * Mathf.Sin(Rotation);
			var y = Mathf.Sin(Angle);
			var z = Mathf.Cos(Angle) * Mathf.Cos(Rotation);
			var OffsetDirection = new Vector3(x, y, z);
			
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
}