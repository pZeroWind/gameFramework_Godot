
using System;
using System.Collections.Generic;
using Godot;

namespace Framework.Runtime;

[SingletonNode]
public partial class InputManager : Node
{
    //private Dictionary<string, > _inputDict;

    private bool _reset;

    public override void _Input(InputEvent @event)
    {
        if (_reset)
        {

        }
    }

    public override void _Process(double delta)
    {
        if (_reset)
        {
            _move = Vector2.Zero;
            Attack = false;
            Skill = false;
            Dodge = false;
            Ultimate = false;
            Operate = false;
            return;
        }
        InputMove();
        Attack = Input.IsActionJustPressed("attack");
        Skill = Input.IsActionJustPressed("skill");
        Dodge = Input.IsActionJustPressed("dodge");
        Ultimate = Input.IsActionJustPressed("ultimate");
        Operate = Input.IsActionJustPressed("operate");
    }

    private Vector2 _move = Vector2.Zero;

    public bool Attack { get; private set; }

    public bool Skill { get; private set; }

    public bool Dodge { get; private set; }

    public bool Ultimate { get; private set; }

    public bool Operate { get; private set; }

    public Vector2 Move => _move;



    private void InputMove()
    {
        _move = Vector2.Zero;
        _move.Y += Input.IsActionPressed("move_up") ? -1 : 0;
        _move.Y += Input.IsActionPressed("move_down") ? 1 : 0;
        _move.X += Input.IsActionPressed("move_right") ? 1 : 0;
        _move.X += Input.IsActionPressed("move_left") ? -1 : 0;
        _move = _move.Normalized();
    }
}