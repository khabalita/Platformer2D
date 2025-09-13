using Godot;
namespace Game.ItemResource;

[GlobalClass] //permite crear nodos de este tipo, desde godot
public partial class ItemResource : Resource
{
	[Export]
	public string fruitName { get; set; }
	[Export]
	public SpriteFrames spriteFrames { get; set; }
}
