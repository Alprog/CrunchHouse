extends Node2D


# Called when the node enters the scene tree for the first time.
func _ready():
	var i:int = 3
	i = i + 1
	
	var gd = find_child("GDExample");
		
	var g = gd.get_amplitude();
	print(g);
	
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass
