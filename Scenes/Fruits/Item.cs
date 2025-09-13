using Godot;
namespace Game.ItemResource.Item;

public partial class Item : Area2D
{
	[Export]
	public ItemResource FruitData { get; set; }
	private AnimatedSprite2D sprite;

	public override void _Ready()
	{
		sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		if (FruitData != null)
		{
			sprite.SpriteFrames = FruitData.spriteFrames;
			sprite.Play();
		}
    }
}
