using Framework;
using Godot;

namespace GameApp;

public class PlayerMoveState : StateObject
{
    private InputManager _inpMgr;

    protected override void OnInitialize()
    {
        _inpMgr = InputManager.Instance;
        AddCanToState(State.Idle, () => _inpMgr.Move == Vector2.Zero);
    }

    public override void OnExecute(UnitNode node, double fTick)
    {
        node.MoveAndCollide(_inpMgr.Move * node.Properties.Get<float>(UnitPropertyName.Speed));
    }
}