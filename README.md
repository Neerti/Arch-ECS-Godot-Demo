# Arch ECS for Godot Demo
This is a simple demonstration of [Arch](https://github.com/genaray/Arch), a C# based Entity Component System, being used inside of the [Godot](https://godotengine.org) game engine.

The demo implements a simple example where each entity is represented as a tiny square sprite that shifts colors and bounces off the edge of the window. Using ECS and manual drawing code allows for far more sprites to exist compared to using Godot's scene tree with sprite nodes.

Entities can be added or cleared using the UI buttons.

A large portion of the code used was adapted from [Arch's MonoGame-based sample](https://github.com/genaray/Arch/tree/master/src/Arch.Samples).

Language: C#

Renderer: Forward+

# Screenshots
![No entities](https://raw.githubusercontent.com/Neerti/Arch-ECS-Godot-Demo/main/Screenshots/empty.png)
![Fifteen thousand entities](https://raw.githubusercontent.com/Neerti/Arch-ECS-Godot-Demo/main/Screenshots/15k.png)
