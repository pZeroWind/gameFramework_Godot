namespace Framework;

public abstract class LeafNode : BehaviorNode
{
    protected override bool EnableChildren => false;
}