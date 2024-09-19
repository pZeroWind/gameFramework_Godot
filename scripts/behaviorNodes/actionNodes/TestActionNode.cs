
using Framework;
using Godot;

namespace GameApp;

public class TestActionNode : ActionNode
{
    public override NodeState OnExecute(double fTick, BehaviorTree tree)
    {
        GD.Print("测试行为树执行");
        return NodeState.Success;
    }
}