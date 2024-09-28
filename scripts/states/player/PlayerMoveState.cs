using Framework;
using Framework.Runtime;
using Godot;

namespace GameApp;

public partial class PlayerMoveState : StateNode
{
    [UseInject]
    public InputManager InpMgr { get; set; }

    [UseInject]
    public GlobalManager GlobalMgr { get; set; }

    protected override void OnInitialize()
    {
        AddCanToState(State.Idle, () => InpMgr.Move == Vector2.Zero);
    }

    public override void OnExecute(UnitNode node, double fTick)
    {
        node.MoveAndCollide(InpMgr.Move * node.PropsMgr[UnitPropertyName.Speed].As<float>() * GlobalMgr.GlobalTimeScale);
    }
}