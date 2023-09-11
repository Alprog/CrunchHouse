using Godot;
using System;

public partial class Grid : Node3D
{
	[Export] public Camera3D Camera;
	public TileMap TileMap;
	public Vector2I Pos;

	public override void _Ready()
	{
		TileMap = new TileMap(Vector2I.One * 200);

		var meshInstance = new MeshInstance3D();
		meshInstance.Mesh = CreateMesh();
		AddChild(meshInstance);		
	}

	public override void _Process(double delta)
	{
		ProcessInput();
		ProcessMouse();
		TileMap.Sync();
	}

	public void ProcessMouse()
	{
		var screenPosition = GetViewport().GetMousePosition();
		var rayOrigin = Camera.ProjectRayOrigin(screenPosition);
		var rayDirection = Camera.ProjectRayNormal(screenPosition);

		var distance = rayOrigin.Y / -rayDirection.Y;

		var collisionPosition = rayOrigin + rayDirection * distance;

		int x = (int)collisionPosition.X;
		int z = (int)collisionPosition.Z;

		TileMap.SetTileIndex(x, z, 4);
	}

	public void ProcessInput()
	{
		if (Input.IsKeyPressed(Key.I))
		{
			Pos.Y -= 1;
			MarkCurrent();
		}
		if (Input.IsKeyPressed(Key.J))
		{
			Pos.X -= 1;
			MarkCurrent();
		}
		if (Input.IsKeyPressed(Key.K))
		{
			Pos.Y += 1;
			MarkCurrent();
		}
		if (Input.IsKeyPressed(Key.L))
		{
			Pos.X += 1;
			MarkCurrent();
		}
	}

	public void MarkCurrent()
	{
		TileMap.SetTileIndex(Pos.X, Pos.Y, 2);
	}

	public Mesh CreateMesh()
	{
		var builder = new MeshBuilder();
		
		var size = TileMap.Size;
		for (int z = 0; z < size.Y; z++)
		{			
			for (int x = 0; x < size.X; x++)
			{
				builder.AddTile(x, z);
			}
		}

		var material = GD.Load("res://custom.material") as ShaderMaterial;
		material.SetShaderParameter("map", TileMap.Texture);

		var colors = new Color[10];
		colors[0] = new Color(0.1f, 0.4f, 0.1f);
		colors[1] = new Color(0, 1, 0);
		colors[2] = new Color(0, 0, 1);
		colors[3] = new Color(1, 1, 0);
		colors[4] = new Color(0, 1, 1);
		colors[5] = new Color(1, 0, 1);

		material.SetShaderParameter("colors", colors);
		builder.Materials.Add(material);

		return builder.Build();
	}
}
