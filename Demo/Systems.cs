using System.Runtime.CompilerServices;
using Arch.Core;
using Godot;

// This is more or less copied straight from the MonoGame version, with alterations made to fit within Godot.

namespace ArchDemo;

public abstract class SystemBase<T>
{
	protected SystemBase(World world)
	{
		World = world;
	}
	
	public World World { get; private set; }

	public abstract void Update(in T state);
}


public class MovementSystem : SystemBase<double>
{
	private readonly QueryDescription _entitiesToMove = new QueryDescription().WithAll<Position, Velocity>();
	private readonly Rect2I _viewport;
	
	public MovementSystem(World world, Rect2I viewport) : base(world)
	{
		_viewport = viewport;
	}
	
	private readonly struct Move : IForEach<Position, Velocity>
	{
		private readonly float _deltaTime;

		public Move(float deltaTime)
		{
			_deltaTime = deltaTime;
		}
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Update(ref Position pos, ref Velocity vel)
		{
			pos.Vec2 += _deltaTime * vel.Vec2;
		}
	}

	private struct Bounce : IForEach<Position, Velocity>
	{
		private Rect2I _viewport;
		
		public Bounce(Rect2I viewport)
		{
			_viewport = viewport;
		}
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Update(ref Position pos, ref Velocity vel)
		{
			if (pos.Vec2.X >= _viewport.Position.X + _viewport.Size.X)
			{
				vel.Vec2.X = -vel.Vec2.X;
			}

			if (pos.Vec2.Y >= _viewport.Position.Y + _viewport.Size.Y)
			{
				vel.Vec2.Y = -vel.Vec2.Y;
			}

			if (pos.Vec2.X <= _viewport.Position.X)
			{
				vel.Vec2.X = -vel.Vec2.X;
			}

			if (pos.Vec2.Y <= _viewport.Position.Y)
			{
				vel.Vec2.Y = -vel.Vec2.Y;
			}
		}
	}
	
	public override void Update(in double time)
	{
		// A JobScheduler is required for these to work.
		// InlineQuery can be used as a non-parallel alternative if desired. 
		var movementJob = new Move((float)time);
		World.InlineParallelQuery<Move, Position, Velocity>(in _entitiesToMove, ref movementJob);

		var bounceJob = new Bounce(_viewport);
		World.InlineParallelQuery<Bounce, Position, Velocity>(in _entitiesToMove, ref bounceJob);
	}
	
	
}



public class ColorSystem : SystemBase<double>
{
	private readonly QueryDescription _entitiesToChangeColor = new QueryDescription().WithAll<Sprite>();
	private static double? _gameTime;

	public ColorSystem(World world) : base(world) { }
	
	public override void Update(in double time)
	{
		_gameTime = time;
		
		World.Query(in _entitiesToChangeColor, (ref Sprite sprite) =>
		{
			sprite.SpriteColor.H += (float)_gameTime % 1;
		});

	}
}


