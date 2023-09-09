using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel;

public class MeshBuilder
{
	public List<Vector3> Positions = new List<Vector3>();
	public List<Vector2> UVs = new List<Vector2>();
	public List<Vector3> Normals = new List<Vector3>();
	public List<int> Indicies = new List<int>();
	public List<Material> Materials = new List<Material>();

	public void AddTile(float x, float z)
	{
		var vertexCount = Positions.Count;

		Positions.AddRange(new[]
		{
			new Vector3(x + 0, 0, z + 0),
			new Vector3(x + 1, 0, z + 0),
			new Vector3(x + 0, 0, z + 1),
			new Vector3(x + 1, 0, z + 1)
		});

		UVs.AddRange(new[]
		{
			new Vector2(0, 0),
			new Vector2(1, 0),
			new Vector2(0, 1),
			new Vector2(1, 1)
		});

		Normals.AddRange(new[]
		{
			Vector3.Up,
			Vector3.Up,
			Vector3.Up,
			Vector3.Up
		});

		int i = vertexCount;
		Indicies.AddRange(new[]
		{
			i + 0, i + 1, i + 2,
			i + 2, i + 1, i + 3
		});
	}

	public Mesh Build()
	{
		var arrays = new Godot.Collections.Array();
		arrays.Resize((int)Mesh.ArrayType.Max);
		arrays[(int)Mesh.ArrayType.Vertex] = Positions.ToArray();
		arrays[(int)Mesh.ArrayType.TexUV] = UVs.ToArray();
		arrays[(int)Mesh.ArrayType.Normal] = Normals.ToArray();
		arrays[(int)Mesh.ArrayType.Index] = Indicies.ToArray();

		var arrayMesh = new ArrayMesh();
		arrayMesh.AddSurfaceFromArrays(Mesh.PrimitiveType.Triangles, arrays);
		for (int i = 0; i < Materials.Count; i++)
		{
			arrayMesh.SurfaceSetMaterial(i, Materials[i]);
		}
		return arrayMesh;
	}
}
