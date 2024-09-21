using Framework;
using Godot;

namespace GameApp;

public partial class PlayerIdleState : StateNode
{
    private InputManager _inpMgr;

    protected override void OnInitialize()
    {
        _inpMgr = InputManager.Instance;
        AddCanToState(State.Move, () => _inpMgr.Move != Vector2.Zero);
        //AddCanToState(State.Attack, () => _inpMgr.Attack);
    }

    public override void OnExecute(UnitNode node, double fTick)
    {
        //_tree.OnExecute(fTick);
        //node.MoveAndCollide(_inpMgr.Move * node.PropertyManager.GetProperty<float>(UnitPropertyName.Speed));
    }
}