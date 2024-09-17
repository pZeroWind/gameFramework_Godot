namespace Framework;

/// <summary>
/// 条件节点
/// </summary>
public abstract class ConditionNode : LeafNode
{
    /// <summary>
    /// 判断条件
    /// </summary>
    public abstract bool Condition(double fTick, BehaviorTree tree);

    public override NodeState OnExecute(double fTick, BehaviorTree tree)
    {
        return Condition(fTick, tree) ? NodeState.Success : NodeState.Failed;
    }
}