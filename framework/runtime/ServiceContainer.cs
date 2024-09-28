using System;
using System.Collections.Generic;
using Godot;

namespace Framework.Runtime;
public sealed class ServiceContainer
{
    /// <summary>
    /// 单例对象
    /// </summary>
    private readonly Dictionary<Type, object> _singletons = [];
    public Dictionary<Type, object> Singletons => _singletons;

    /// <summary>
    /// 瞬时对象
    /// </summary>
    private readonly Dictionary<Type, Func<Node, object>> _transients = [];
    public Dictionary<Type, Func<Node, object>> Transients => _transients;

    /// <summary>
    /// 范围对象
    /// </summary>
    private readonly Dictionary<Type, Func<Node, object>> _scopeds = [];
    public Dictionary<Type, Func<Node, object>> Scopeds => _scopeds;

    /// <summary>
    /// 当前单位类型的范围对象单例
    /// </summary>
    private readonly Dictionary<Type, Dictionary<Type, object>> _scopedSingletons = [];
    public Dictionary<Type, Dictionary<Type, object>> ScopedSingletons => _scopedSingletons;

    /// <summary>
    /// 对象所存类型
    /// </summary>
    private readonly Dictionary<Type, InjectType> _injectTypes = [];
    public Dictionary<Type, InjectType> InjectTypes => _injectTypes;

    #region 单例对象
    public void Singleton<T>() where T : class, new()
    {
        Type type = typeof(T);
        if (!_singletons.ContainsKey(type))
        {
            _singletons.Add(type, new T());
            _injectTypes.Add(type, InjectType.Singleton);
        }
    }

    public void Singleton(object obj)
    {
        Type type = obj.GetType();
        if (!_singletons.ContainsKey(type))
        {
            _singletons.Add(type, obj);
            _injectTypes.Add(type, InjectType.Singleton);
        }
    }
    #endregion

    #region 瞬时对象
    public void Transient<T>() where T : class, new()
    {
        Type type = typeof(T);
        if (!_transients.ContainsKey(type))
        {
            _transients.Add(type, node => new T());
            _injectTypes.Add(type, InjectType.Transient);
        }
    }

    public void Transient<T>(Func<Node, T> func) where T : class
    {
        Type type = typeof(T);
        if (!_transients.ContainsKey(type))
        {
            _transients.Add(type, func);
            _injectTypes.Add(type, InjectType.Transient);
        }
    }
    #endregion

    #region 范围对象
    public void Scoped<T>() where T : class, new()
    {
        Type type = typeof(T);
        if (!_scopeds.ContainsKey(type))
        {
            _scopeds.Add(type, node => new T());
            _injectTypes.Add(type, InjectType.Scoped);
        }
    }

    public void Scoped<T>(T obj) where T : class
    {
        Type type = typeof(T);
        if (!_scopeds.ContainsKey(type))
        {
            _scopeds.Add(type, node => obj);
            _injectTypes.Add(type, InjectType.Scoped);
        }
    }

    public void Scoped<T>(Func<Node, T> func) where T : class
    {
        Type type = typeof(T);
        if (!_scopeds.ContainsKey(type))
        {
            _scopeds.Add(type, func);
            _injectTypes.Add(type, InjectType.Scoped);
        }
    }
    #endregion
}