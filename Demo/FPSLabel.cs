using Godot;

namespace ArchDemo;

/// <summary>
/// A simple label that updates every frame with some information, mainly the FPS.
/// </summary>
public partial class FPSLabel : Label
{
	public override void _Process(double delta)
	{
		Text = $@"FPS: {Performance.GetMonitor(Performance.Monitor.TimeFps)}
Entities: {GetNode<EntityRenderer>("/root/Demo/EntityRenderer").World.Size}";
	}
	
}
