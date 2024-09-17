namespace Framework;

/// <summary>
/// 顺序节点
/// </summary>
public class SequenceNode : CompositeNode
{
    public override NodeState OnExecute(double fTick, BehaviorTree tree)
    {
        bool runing = false;
        bool success = true;
        foreach (var node in Children)
        {
            var state = node.OnExecute(fTick, tree);
            if (state != NodeState.Success)
            {
                success = false;
                if (state == NodeState.Runing) runing = true;
                break;
            }
        }

        return runing ? NodeState.Runing :
        success ? NodeState.Success : NodeState.Failed;
    }
}