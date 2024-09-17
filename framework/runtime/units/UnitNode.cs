using System.Collections.Generic;
using Godot;

namespace Framework;
public static class UnitPropertyName
{
    /// <summary>
    /// 体力
    /// </summary>
    public const string HP = "HP";

    /// <summary>
    /// 最大体力
    /// </summary>
    public const string MaxHP = "MaxHP";

    /// <summary>
    /// 法力
    /// </summary>
    public const string MP = "MP";

    /// <summary>
    /// 最大法力
    /// </summary>
    public const string MaxMP = "MaxMP";

    /// <summary>
    /// 耐力
    /// </summary>
    public const string SP = "SP";

    /// <summary>
    /// 最大耐力
    /// </summary>
    public const string MaxSP = "MaxSP";

    /// <summary>
    /// 移动速度
    /// </summary>
    public const string Speed = "Speed";

    /// <summary>
    /// 时间倍率
    /// </summary>
    public const string TimeScale = "TimeScale";
}

public abstract partial class UnitNode : CharacterBody2D
{
    public PropertyManager Properties { get; }

    [Export]
    public string UnitName { get; set; } = "default";

    public UnitNode()
    {
        Properties = new();
        Properties.Set<float>(UnitPropertyName.TimeScale, 1f);
    }

    
}