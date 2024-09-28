using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

namespace Framework.Runtime;

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

    /// <summary>
    /// 绑定单位
    /// </summary>
    private readonly UnitNode _owner;

    public Dictionary<string, ITValue> GetValues() => _properties;

    public PropertyManager(Node node = null)
    {
        if (node is UnitNode u)
        {
            _owner = u;
        }
    }

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
        if (_properties.TryGetValue(key, out ITValue value))
        {
            return value.As<T>();
        }
        return default;
    }

    /// <summary>
    /// 获取属性 - 限定float
    /// </summary>
    public float GetFloat(string key)
    {
        if (_properties.TryGetValue(key, out ITValue value))
        {
            float result = 0f;
            result += GetSpecialProperty(key);
            result += value.As<float>();
            return result;
        }
        return 0f;
    }

    /// <summary>
    /// 获取属性 - 限定int
    /// </summary>
    public int GetInt(string key)
    {
        if (_properties.TryGetValue(key, out ITValue value))
        {
            return value.As<int>();
        }
        return 0;
    }

    /// <summary>
    /// 获取属性 - 限定bool
    /// </summary>
    public bool GetBool(string key)
    {
        if (_properties.TryGetValue(key, out ITValue value))
        {
            return value.As<bool>();
        }
        return false;
    }

    /// <summary>
    /// 获取属性限定double
    /// </summary>
    public double GetDouble(string key)
    {
        if (_properties.TryGetValue(key, out ITValue value))
        {
            return value.As<double>();
        }
        return 0d;
    }

    /// <summary>
    /// 添加特殊属性值
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    private int GetSpecialProperty(string key)
    {
        if (_owner is null) return 0;
        int result = 0;
        switch (key)
        {
            case UnitPropertyName.MaxHP:
                result += _owner.BuffMgr
                    .GetBuff<NumericBuff>()
                    .Where(buff => buff.PropertyName == key)
                    .Sum(buff => buff.Value);
                break;
        }
        return result;
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
}