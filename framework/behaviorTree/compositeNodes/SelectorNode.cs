namespace Framework;

/// <summary>
/// 选择节点
/// </summary>
public class SelectorNode : CompositeNode
{
    public override NodeState OnExecute(double fTick, BehaviorTree tree)
    {
        bool runing = false;
        bool failed = true;
        foreach (var node in Children)
        {
            var state = node.OnExecute(fTick, tree);
            if (state != NodeState.Failed)
            {
                failed = false;
                if (state == NodeState.Runing) runing = true;
                break;
            }
        }

        return runing ? NodeState.Runing :
        failed ? NodeState.Failed : NodeState.Success;
    }
}