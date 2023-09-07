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
		var arrayMesh = new ArrayMesh();

		var vertices = new Vector3[]
		{
			new Vector3(0, 0, 0),
			new Vector3(1, 0, 0),
			new Vector3(0, 0, 1),
			new Vector3(1, 0, 1)			
		};

		var uv = new Vector2[]
		{
			new Vector2(0, 0),
			new Vector2(1, 0),
			new Vector2(0, 1),
			new Vector2(1, 1)			
		};

		var indecies = new int[]
		{
			0, 1, 2, 
			2, 1, 3
		};

		var normals = new Vector3[]
		{
			Vector3.Up,
			Vector3.Up,
			Vector3.Up,
			Vector3.Up
		};

		var arrays = new Godot.Collections.Array();
		arrays.Resize((int)Mesh.ArrayType.Max);
		arrays[(int)Mesh.ArrayType.Vertex] = vertices;
		arrays[(int)Mesh.ArrayType.TexUV] = uv;
		arrays[(int)Mesh.ArrayType.Index] = indecies;
		arrays[(int)Mesh.ArrayType.Normal] = normals;

		arrayMesh.AddSurfaceFromArrays(Mesh.PrimitiveType.Triangles, arrays);
		
		var material = GD.Load("res://grid.material") as Material;
		arrayMesh.SurfaceSetMaterial(0, material);

		return arrayMesh;
	}
}
