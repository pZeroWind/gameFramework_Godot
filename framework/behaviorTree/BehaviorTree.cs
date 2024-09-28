using Godot;
using System;

namespace Framework;

public class BehaviorTree
{
    private BehaviorNode _root;

    private bool _enable;

    /// <summary>
    /// 设置根节点
    /// </summary>
    public BehaviorNode SetRoot<T>() where T : CompositeNode, new()
    {
        _root = new T();
        _enable = true;
        return _root;
    }

    /// <summary>
    /// 运行行为树
    /// </summary>
    public void OnExecute(double tick)
    {
        if (_enable)
            _root.OnExecute(tick ,this);
    }
}
