shader_type spatial;

uniform sampler2D cellTexture;
uniform vec3 colors[100];

varying flat uint index;

void vertex() 
{
	index = BONE_INDICES.x % 17u;
}

void fragment() 
{
	ALBEDO = colors[index] + texture(cellTexture, UV).xyz;
}