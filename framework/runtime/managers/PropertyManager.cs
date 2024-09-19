using System;
using System.Collections.Generic;
using Godot;

namespace Framework;

public interface ITValue
{
    void Val(Variant value);

    T As<T>();
}

public class TValue : ITValue
{
    private Variant _value;

    public T As<[MustBeVariant] T>()
    {
        return _value.As<T>();
    }

    public void Val(Variant value)
    {
        _value = value;
    }
}

public sealed class PropertyManager
{
    private readonly Dictionary<string, ITValue> _properties = [];

    public Dictionary<string, ITValue> GetValues() => _properties;

    /// <summary>
    /// 索引器
    /// </summary>
    public ITValue this[string key]
    {
        get
        {
            if (!_properties.ContainsKey(key))
                _properties.Add(key, new TValue());
            return _properties[key];
        }
    }

    /// <summary>
    /// 获取属性
    /// </summary>
    public T Get<T>(string key)
    {
        if (_properties.ContainsKey(key))
        {
            return _properties[key].As<T>();
        }
        return default;
    }

    /// <summary>
    /// 设置属性
    /// </summary>
    public void Set(string key, Variant value)
    {
        if (!_properties.ContainsKey(key))
        {
            _properties.Add(key, new TValue());
        }
        _properties[key].Val(value);
    }

    internal void Set<T>(string maxHP, Variant variant)
    {
        throw new NotImplementedException();
    }
}