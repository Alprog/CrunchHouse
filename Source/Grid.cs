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
		
		var image = Image.Create(200, 200, false, Image.Format.Rgba8);
		for (int z = 0; z < 200; z++)
		{			
			for (int x = 0; x < 200; x++)
			{
				image.SetPixel(x, z, new Color(0, 0, 0));
				builder.AddTile(x, z, index++);
			}
		}

		
		for (int i = 0; i < 1000; i++)
		{
			var color = new Color((uint)i);
			image.SetPixel(i, i, color);
		}

		var mapTexture = ImageTexture.CreateFromImage(image);
	
		image.SetPixel(2, 0, new Color(3u));
		mapTexture.Update(image);

		var material = GD.Load("res://custom.material") as ShaderMaterial;
		material.SetShaderParameter("map", mapTexture);

		var colors = new Color[40000];
		colors[0] = new Color(1, 0, 0);
		colors[1] = new Color(0, 1, 0);
		colors[2] = new Color(0, 0, 1);
		colors[3] = new Color(1, 1, 0);
		colors[4] = new Color(0, 1, 1);
		colors[5] = new Color(1, 0, 1);

		colors[4095] = new Color(1, 1, 0);

		material.SetShaderParameter("colors", colors);
		builder.Materials.Add(material);

		return builder.Build();
	}
}
