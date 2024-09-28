using System;
using System.Collections.Generic;
using System.Reflection;
using Godot;

namespace Framework.Runtime;

public enum InjectType
{
    /// <summary>
    /// 单例
    /// </summary>
    Singleton,

    /// <summary>
    ///  瞬时
    /// </summary>
    Transient,

    /// <summary>
    /// 范围
    /// </summary>
    Scoped,
}

/// <summary>
/// 框架IOC根节点
/// </summary>
public abstract partial class FrameworkRootNode : Node
{
    private readonly ServiceContainer _services = new();

    /// <summary>
    /// 载入节点并注入依赖
    /// </summary>
    public void LoadNode<T>(T node) where T : Node
    {
        // 开始向节点注入对象
        var props = node.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        foreach (var prop in props)
        {
            var useInject = prop.GetCustomAttribute<UseInjectAttribute>();
            if (useInject == null) continue;
            var propType = prop.PropertyType;
            if (_services.InjectTypes.TryGetValue(propType, out InjectType type))
            {
                switch (type)
                {
                    case InjectType.Singleton:
                        {
                            if (_services.Singletons.TryGetValue(propType, out var obj))
                                prop.SetValue(node, obj);
                        }
                        break;
                    case InjectType.Transient:
                        {
                            if (_services.Transients.TryGetValue(propType, out var func))
                                prop.SetValue(node, func.Invoke(node));
                        }
                        break;
                    case InjectType.Scoped:
                        {
                            Type nodeType = typeof(T);
                            if (_services.ScopedSingletons.TryGetValue(nodeType, out var list) && list.TryGetValue(propType, out var obj))
                            {
                                prop.SetValue(node, obj);
                            }
                            else if (_services.Scopeds.TryGetValue(propType, out var func))
                            {
                                var newObj = func.Invoke(node);
                                if (!_services.ScopedSingletons.TryGetValue(nodeType, out var list2))
                                {
                                    list2 = [];
                                    _services.ScopedSingletons.Add(nodeType, list2);
                                }
                                list2.Add(propType, newObj);
                                prop.SetValue(node, newObj);
                            }
                        }
                        break;
                }
            }
        }
    }

    public void RemoveNode<T>(T node) where T : Node
    {
        Type type = typeof(T);
        if (_services.ScopedSingletons.TryGetValue(type, out var dict))
        {
            foreach (var key in dict.Keys)
            {
                dict.Remove(key);
            }
            _services.ScopedSingletons.Remove(type);
        }
    }

    /// <summary>
    /// 框架开始运行时 用于注册服务
    /// </summary>
    /// <param name="services"></param>
    protected abstract void OnStart(ServiceContainer services);

    /// <summary>
    /// 框架构建完毕 用于载入UnitNode对象
    /// </summary>
    /// <param name="services"></param>
    protected abstract void OnMounted();

    public override void _EnterTree()
    {
        _services.Transient<UnitManager>();
        _services.Transient(node => new PropertyManager(node));
        _services.Transient(node => new BuffManager(node));
        OnStart(_services);
        // BFS 遍历所有子节点进行构建
        Queue<Node> nodes = [];
        var arr = GetChildren();
        foreach (var node in arr)
        {
            var attr = node.GetType().GetCustomAttribute<SingletonNodeAttribute>();
            // 若当前节点为单例节点
            if (attr is not null)
            {
                _services.Singleton(node);
            }
            nodes.Enqueue(node);
        }
        while (nodes.Count > 0)
        {
            var curNode = nodes.Dequeue();
            LoadNode(curNode);
            var childrenArr = curNode.GetChildren();
            foreach (var node in childrenArr) nodes.Enqueue(node);
        }
    }

    public override void _Ready()
    {
        OnMounted();
    }
}