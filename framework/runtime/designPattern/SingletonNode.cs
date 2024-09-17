using Godot;

namespace Framework;

/// <summary>
/// 单例节点
/// </summary>
public partial class SingletonNode<T>  : Node
    where T : Node
{
    /// <summary>
    /// 静态单例对象
    /// </summary>
    private static Node _value;

    private static readonly object _lock = new();

    /// <summary>
    /// 实例化对象
    /// </summary>
    public static T Instance {
        get
        {
            return (T)_value;
        }
    }

    public SingletonNode()
    {
        _value = this;
    }
}