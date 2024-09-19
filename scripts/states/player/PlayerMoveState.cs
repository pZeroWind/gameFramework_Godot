using Framework;
using Godot;

namespace GameApp;

public partial class PlayerMoveState : StateNode
{
    private InputManager _inpMgr;

    private GlobalManager _globalMgr;

    protected override void OnInitialize()
    {
        _inpMgr = InputManager.Instance;
        _globalMgr = GlobalManager.Instance;
        AddCanToState(State.Idle, () => _inpMgr.Move == Vector2.Zero);
    }

    public override void OnExecute(UnitNode node, double fTick)
    {
        node.MoveAndCollide(_inpMgr.Move * node.Properties[UnitPropertyName.Speed].As<float>() * _globalMgr.GlobalTimeScale);
    }
}