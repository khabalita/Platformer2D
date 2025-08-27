using Godot;
using System;

namespace godot.Resources.Fruit;

public partial class FruitResource : Resource
{
	[Export]
	public PackedScene fruitScene { get; private set; }
}
