using Godot;
using System;
using System.Collections.Generic;

namespace Framework;

/// <summary>
/// 状态枚举
/// </summary>
public enum State
{
	/// <summary>
	/// 待机
	/// </summary>
	Idle = 101,

	/// <summary>
	/// 移动
	/// </summary>
	Move,

	/// <summary>
	/// 攻击
	/// </summary>
	Attack
}

/// <summary>
/// 状态实体函数接口
/// </summary>
public interface IState
{
	/// <summary>
	/// 获取可转移的状态列表
	/// </summary>
	Dictionary<State, Func<bool>> GetCanToStates();

	/// <summary>
	/// 进入状态时调用
	/// </summary>
	void OnEnter(UnitNode node);

	/// <summary>
	/// 执行状态时调用
	/// </summary>
	/// <param name="tick"></param>
	void OnExecute(UnitNode node, double tick);

	/// <summary>
	/// 退出状态时调用
	/// </summary>
	void OnExit(UnitNode node);
}

/// <summary>
/// 状态抽象实体
/// </summary>
public abstract partial class StateNode : Node, IState
{
	private readonly Dictionary<State, Func<bool>> _canToState = new();

	private BehaviorTree _tree;

	public Dictionary<State, Func<bool>> GetCanToStates() => _canToState;

	protected BehaviorTree BehaviorTree => _tree;
	
	[Export]
	public bool EnableBehaviorTree { get; set; } = false;

	[Export]
	public State State { get; set; } = State.Idle;
	
	/// <summary>
	/// 状态构建时
	/// </summary>
	protected abstract void OnInitialize();

    public override void _Ready()
    {
		if(EnableBehaviorTree) _tree = new BehaviorTree();
        OnInitialize();
    }
    

	public virtual void OnEnter(UnitNode node) { }

	public virtual void OnExecute(UnitNode node, double tick)
	{	
		if(EnableBehaviorTree) _tree.OnExecute(tick);
	}

	public virtual void OnExit(UnitNode node) { }

	/// <summary>
	///  添加可转移状态
	/// </summary>
	protected void AddCanToState(State key, Func<bool> condition)
	{
		_canToState.TryAdd(key, condition);
    }
}

