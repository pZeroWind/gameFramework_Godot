using Framework;

namespace GameApp;

public class TestConditionNode : ConditionNode
{
    public override bool Condition(double fTick, BehaviorTree tree)
    {
        return false;
    }
}