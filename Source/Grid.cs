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

		int index = 0;
		
		for (int z = 0; z < 200; z++)
		{			
			for (int x = 0; x < 200; x++)
			{
				builder.AddTile(x, z, index++);
			}
		}
	
		var material = GD.Load("res://custom.material") as ShaderMaterial;

		var colors = new Vector3[100];
		for (int i = 0; i < 100; i++)
		{
			colors[i] = Vector3.One / 2;
		}
		colors[0] = new Vector3(1, 0, 0);
		colors[1] = new Vector3(0, 1, 0);
		colors[2] = new Vector3(0, 0, 1);
		colors[3] = new Vector3(1, 1, 0);
		colors[4] = new Vector3(0, 1, 1);
		colors[5] = new Vector3(1, 0, 1);

		material.SetShaderParameter("colors", colors);
		builder.Materials.Add(material);

		return builder.Build();
	}
}
