namespace Framework;

/// <summary>
/// 条件节点
/// </summary>
public abstract class ConditionNode : LeafNode
{
    /// <summary>
    /// 判断条件
    /// </summary>
    public abstract bool Condition(double tick, BehaviorTree tree);

    public override NodeState OnExecute(double tick, BehaviorTree tree)
    {
        return Condition(tick, tree) ? NodeState.Success : NodeState.Failed;
    }
}