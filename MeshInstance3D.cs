using Godot;
using System;

public partial class MeshInstance3D : Godot.MeshInstance3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var material = MaterialOverride as ShaderMaterial;
		material.SetShaderParameter("height_scale", 0.5f);

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}
