# Arch ECS for Godot Demo
This is a simple demonstration of [Arch](https://github.com/genaray/Arch), a C# based Entity Component System, being used inside of the [Godot](https://godotengine.org) game engine.

The demo implements a simple example where each entity is represented as a tiny square sprite that shifts colors and bounces off the edge of the window. Using ECS and MultiMesh2D allows for far more sprites to exist compared to ordinary sprite nodes.

Entities can be added or cleared using the UI buttons.

A large portion of the code used was adapted from [Arch's MonoGame-based sample](https://github.com/genaray/Arch/tree/master/src/Arch.Samples).

Language: C#

Renderer: Forward+

# Screenshots
Note: Screenshots taken on a release build of the demo.
![No entities](https://raw.githubusercontent.com/Neerti/Arch-ECS-Godot-Demo/main/Screenshots/empty.png)
![Fifty thousand entities at roughly one hundred and fourty frames per second](https://raw.githubusercontent.com/Neerti/Arch-ECS-Godot-Demo/main/Screenshots/50k.png)
![One hundred thousand entities at roughly 60 frames per second](https://raw.githubusercontent.com/Neerti/Arch-ECS-Godot-Demo/main/Screenshots/100k.png)
