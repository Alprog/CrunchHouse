shader_type spatial;

uniform sampler2D cellTexture;
uniform sampler2D map;
uniform vec4 colors[10];

varying flat int index;

void vertex() 
{
	int x = int(BONE_INDICES.x);
	int y = int(BONE_INDICES.y);	
	vec4 texel = texelFetch(map, ivec2(x, y), 0);
	ivec4 itexel = ivec4(texel * 255.0f);
	index = int(itexel.a);
}

void fragment() 
{
	vec3 fillColor = colors[index].xyz;
	vec3 cellColor = texture(cellTexture, UV).xyz;
	ALBEDO = fillColor + cellColor;
}