namespace Framework;

public abstract class LeafNode : BehaviorNode
{
    protected override bool EnableAdddChild => false;
}