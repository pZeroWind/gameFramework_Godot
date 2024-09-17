
using Framework;

namespace GameApp;

public class TestActionNode : ActionNode
{
    public override NodeState OnExecute(double fTick, BehaviorTree tree)
    {
        return NodeState.Success;
    }
}