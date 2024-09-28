using Framework.Runtime;
using Godot;
using System;

namespace GameApp;
public partial class TestRootNode : FrameworkRootNode
{
    protected override void OnMounted()
    {
        //GetTree()
        var playerScene = GD.Load<PackedScene>("res://prefabs/characters/player.tscn");
        var player = playerScene.Instantiate<PlayerNode>();
        AddChild(player);
    }


    protected override void OnStart(ServiceContainer services)
    {
        services.Singleton<GlobalManager>();
    }
}
