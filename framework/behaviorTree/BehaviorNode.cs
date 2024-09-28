using System.Collections.Generic;

namespace Framework;

public enum NodeState
{
    Success,
    Failed,
    Runing
}

public abstract class BehaviorNode
{
    private BehaviorNode _parent;

    private BehaviorNode[] _children;

    protected virtual bool EnableAdddChild { get; }

    public static BehaviorNode CreateNode<T>() where T : BehaviorNode, new() => new T();

    /// <summary>
    /// 设置子节点
    /// </summary>
    public BehaviorNode AddChildren(params BehaviorNode[] children)
    {
        if (!EnableAdddChild) return this;
        _children = children;
        foreach (var child in children)
            child._parent = this;
        return this;
    }

    public BehaviorNode Parent => _parent;

    public BehaviorNode[] Children => _children;

    public abstract NodeState OnExecute(double tick, BehaviorTree tree);
}