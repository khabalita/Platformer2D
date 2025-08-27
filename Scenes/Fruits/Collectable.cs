using Godot;

public partial class Collectable : Area2D
{
	[Export]
	public string FruitAnimationName { get; private set; } = "Apple";
	[Export]
	public int ScoreValue { get; private set; } = 1;
	private AnimatedSprite2D animation;
	private bool isCollected = false;

	public override void _Ready()
	{
		animation = GetNode<AnimatedSprite2D>("Animation");
		BodyEntered += OnBodyEntered;
		animation.Play(FruitAnimationName);
		animation.AnimationFinished += OnAnimationFinished;
	}

	private void OnBodyEntered(Node2D body)
	{
		if (isCollected || !(body is Player)) return;

		isCollected = true;
		GD.Print("sumaste una" + FruitAnimationName);

		animation.Play("Collected");
	}

	private void OnAnimationFinished()
	{
			QueueFree();
	}
}
