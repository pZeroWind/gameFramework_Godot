using System;
using System.Collections;
using System.Collections.Generic;
using Godot;

namespace Framework.Runtime;
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
    private readonly CoroutineCore _coroutineCore = new();

    private FrameworkRootNode _root;

    [UseInject]
    public UnitManager UnitMgr { get; set; }

    [UseInject]
    public PropertyManager PropsMgr { get; set; }

    [UseInject]
    public BuffManager BuffMgr { get; set; }

    [Export]
    public string UnitName { get; set; } = "default";

    /// <summary>
    /// 开启协程
    /// </summary>
    public void StartCoroutine(IEnumerator routine)
    {
        _coroutineCore.StartCoroutine(routine);
    }

    /// <summary>
    /// 开启协程
    /// </summary>
    public void StartCoroutine(Func<IEnumerator> routine)
    {
        _coroutineCore.StartCoroutine(routine.Invoke());
    }

    public override void _EnterTree()
    {
        _root = GetParent<FrameworkRootNode>();
        Queue<Node> nodes = [];
        nodes.Enqueue(this);
        while (nodes.Count > 0)
        {
            var curNode = nodes.Dequeue();
            _root.LoadNode(curNode);
            var childrenArr = curNode.GetChildren();
            foreach (var child in childrenArr) nodes.Enqueue(child);
        }
    }

    public override void _Ready()
    {
        UnitMgr.AddUnit(this);
        PropsMgr[UnitPropertyName.TimeScale].Val(1f);
    }

    public override void _Process(double delta)
    {
        _coroutineCore.OnUpdateCoroutine(delta);
        BuffMgr.OnUpdate(delta);
    }

    public override void _ExitTree()
    {
        _root.RemoveNode(this);
    }
}