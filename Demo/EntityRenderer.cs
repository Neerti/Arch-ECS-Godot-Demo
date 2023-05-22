using Godot;
using Arch.Core;

namespace ArchDemo;

public partial class EntityRenderer : Node2D
{
	// World ECS
	public World World;
	
	// ECS Systems.
	private MovementSystem _movementSystem;
	private ColorSystem _colorSystem;
	
	// Sprite that represents each entity.
	public Texture2D EntityTexture;
	

	/// <summary>
	/// Adds the specified number of entities, with all components populated with random values.
	/// </summary>
	/// <param name="amount">How many entities to add.</param>
	public void AddEntities(int amount)
	{
		for (var i = 0; i < amount; i++)
		{
			World.Create(
				new Position {
					Vec2 = new Vector2(
						GD.Randf() * GetViewportRect().Size.X, 
						GD.Randf() * GetViewportRect().Size.Y)
				},
				new Velocity {Vec2 = new Vector2((float)GD.RandRange(-1.0f, 1.0f) * 200f, (float)GD.RandRange(-1.0f, 1.0f) * 200f)},
				new Sprite {SpriteColor = new Color(GD.Randf(), GD.Randf(), GD.Randf())}
			);
		}
	}

	/// <summary>
	/// Removes all entities, which will also clear the screen.
	/// </summary>
	public void ClearEntities()
	{
		World.Clear();
	}

	// Godot methods.
	
	// Called once when the node is ready. In the demo, this is used to set everything up.
	public override void _Ready()
	{
		// ECS setup.
		World = World.Create();
		_movementSystem = new MovementSystem(World, new Rect2I(0, 0, GetTree().Root.Size));
		_colorSystem = new ColorSystem(World);

		EntityTexture = GD.Load<Texture2D>("res://Sprite.png");

		// Button setup.
		// This is likely considered bad practice in an actual Godot project, but this is done in the demo
		// to minimize the number of files and nodes in the project.
		GetNode<Button>("/root/Demo/MarginContainer/VBoxContainer/HBoxContainer/Add100Button").Pressed +=
			() => AddEntities(100);
		GetNode<Button>("/root/Demo/MarginContainer/VBoxContainer/HBoxContainer/Add500Button").Pressed +=
			() => AddEntities(500);
		GetNode<Button>("/root/Demo/MarginContainer/VBoxContainer/HBoxContainer/Add1000Button").Pressed +=
			() => AddEntities(1000);
		GetNode<Button>("/root/Demo/MarginContainer/VBoxContainer/ClearButton").Pressed +=
			ClearEntities;
		GetNode<SpinBox>("/root/Demo/MarginContainer/VBoxContainer/PhysicsTicksSpinBox").ValueChanged +=
			value => Engine.PhysicsTicksPerSecond = (int) value;
	}
	
	// Called every frame. In the demo, this is required for manual drawing.
	public override void _Process(double delta)
	{
		QueueRedraw();
	}
	
	// Called by default 60 times a second, and is decoupled from FPS. In the demo,
	// the interval can be changed in the UI.
	public override void _PhysicsProcess(double delta)
	{
		// Systems are updated at a constant rate,
		// but they could also be tied to FPS by updating them in _Process() instead.
		_movementSystem.Update(delta);
		_colorSystem.Update(delta);
	}
	
	// Called every frame, and manually draws each entity.
	public override void _Draw()
	{
		var desc = new QueryDescription().WithAll<Position, Sprite>();

		World.Query(in desc, (ref Position pos, ref Sprite sp) =>
		{
			DrawTexture(EntityTexture, pos.Vec2, sp.SpriteColor);
		});
	}
}


