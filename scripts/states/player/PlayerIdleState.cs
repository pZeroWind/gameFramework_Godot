using Framework;
using Framework.Runtime;
using Godot;

namespace GameApp;

public partial class PlayerIdleState : StateNode
{
    [UseInject]
    public InputManager InpMgr { get; set; }

    protected override void OnInitialize()
    {
        AddCanToState(State.Move, () => InpMgr.Move != Vector2.Zero);
        //AddCanToState(State.Attack, () => _inpMgr.Attack);
    }

    public override void OnExecute(UnitNode node, double fTick)
    {
        //_tree.OnExecute(fTick);
        //node.MoveAndCollide(_inpMgr.Move * node.PropertyManager.GetProperty<float>(UnitPropertyName.Speed));
    }
}