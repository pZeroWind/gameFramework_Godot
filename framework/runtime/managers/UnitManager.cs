using System;
using System.Collections.Generic;
using System.Linq;

namespace Framework;

public class UnitManager : Singleton<UnitManager>
{
    private readonly Dictionary<string, List<UnitNode>> Units = [];

    /// <summary>
    /// 按名称获取Unit列表
    /// </summary>
    public List<T> FindUnits<T>(string unitName) where T : UnitNode
    {
        if (Units.TryGetValue(unitName, out List<UnitNode> value))
            return value as List<T>;
        return [];
    }

    /// <summary>
    /// 按名称获取Unit
    /// </summary>
    public T FindSingle<T>(string unitName, int index = 0) where T : UnitNode
    {
        var key = typeof(T);
        if (Units.TryGetValue(unitName, out List<UnitNode> value) && value.Count > 0)
            return value[index] as T;
        return default;
    }

    /// <summary>
    /// 添加Unit
    /// </summary>
    public void AddUnit<T>(T unit) where T : UnitNode
    {
        if (!Units.TryGetValue(unit.UnitName, out List<UnitNode> value))
        {
            value = [];
            Units.Add(unit.UnitName, value);
        }
        value.Add(unit);
    }

    /// <summary>
    /// 清空全部角色
    /// </summary>
    public void ClearUnits()
    {
        Units.Clear();
    }
}