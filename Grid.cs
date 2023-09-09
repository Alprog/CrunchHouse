using Godot;
using System;

public partial class Grid : Node3D
{
	public override void _Ready()
	{
		var meshInstance = new MeshInstance3D();
		meshInstance.Mesh = CreateMesh();
		AddChild(meshInstance);
	}

	public override void _Process(double delta)
	{
	}

	public Mesh CreateMesh()
	{
		var builder = new MeshBuilder();

		for (int x = 0; x < 200; x++)
		{
			for (int z = 0; z < 200; z++)
			{
				builder.AddTile(x, z);
			}
		}
	

		var material = GD.Load("res://grid.material") as Material;
		builder.Materials.Add(material);

		return builder.Build();
	}
}
