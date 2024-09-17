using Framework;
using Godot;

namespace GameApp;

public class PlayerIdleState : StateObject
{
    private InputManager _inpMgr;

    private BehaviorTree _tree = new();

    protected override void OnInitialize()
    {
        _inpMgr = InputManager.Instance;
        AddCanToState(State.Move, () => _inpMgr.Move != Vector2.Zero);
        AddCanToState(State.Attack, () => _inpMgr.Attack);
        
        _tree.SetRoot<SequenceNode>()
            .AddChildren(
                BehaviorNode.CreateNode<TestConditionNode>(),
                BehaviorNode.CreateNode<SelectorNode>()
                    .AddChildren(
                        BehaviorNode.CreateNode<TestActionNode>()
                    )
            );
    }

    public override void OnExecute(UnitNode node, double fTick)
    {
        _tree.OnExecute(fTick);
        //node.MoveAndCollide(_inpMgr.Move * node.PropertyManager.GetProperty<float>(UnitPropertyName.Speed));
    }
}