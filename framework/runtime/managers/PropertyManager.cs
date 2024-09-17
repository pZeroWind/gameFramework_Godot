using System;
using System.Collections.Generic;
using Godot;

namespace Framework;

public interface ITValue
{

    void SetValue<T>(T value);

    T GetValue<T>();
}

public class TValue<T> : ITValue
{
    private T _value;

    public S GetValue<S>()
    {
        if (_value is S v) return v;
        return default;
    }

    public void SetValue<S>(S value)
    {
        if (value is T v) _value = v;
    }
}

public sealed class PropertyManager
{
    private readonly Dictionary<string, ITValue> _properties = [];

    public Dictionary<string, ITValue> GetValues() => _properties;


    /// <summary>
    /// 获取属性
    /// </summary>
    public T Get<T>(string key)
    {
        if (_properties.ContainsKey(key))
        {
            return _properties[key].GetValue<T>();
        }
        return default;
    }

    /// <summary>
    /// 设置属性
    /// </summary>
    public void Set<T>(string key, T value)
    {
        if (!_properties.ContainsKey(key))
        {
            _properties.Add(key, new TValue<T>());
        }
        _properties[key].SetValue(value);
    }

    internal void Set<T>(string maxHP, Variant variant)
    {
        throw new NotImplementedException();
    }
}