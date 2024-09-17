using Godot;
using System;
using System.Collections.Generic;

namespace Framework;

public abstract partial class StateMachine : Node
{
    /// <summary>
    /// 状态字典
    /// </summary>
    private readonly Dictionary<State, IState> _states = new();

    /// <summary>
    /// 单位节点
    /// </summary>
    private UnitNode _owner;

    /// <summary>
    /// 当前状态
    /// </summary>
    private State _currentState = State.Idle;

    public override void _Ready()
    {
        _owner = GetParent<UnitNode>();
        OnInitialize();
    }

    /// <summary>
    /// 初始化时调用
    /// </summary>
    protected abstract void OnInitialize();

    /// <summary>
    /// 注册状态
    /// </summary>
    protected void StateRegister<T>(State key) where T : IState, new()
    {
        if (_states.ContainsKey(key)) return;
        _states.Add(key, new T());
    }

    /// <summary>
    /// 删除状态
    /// </summary>
    protected void StateRemove(State key)
    {
        if (_states.ContainsKey(key))
        {
            _states.Remove(key);
        }
    }

    public override void _Process(double delta)
    {
        // 若状态字典中存在当前状态
        if (_states.ContainsKey(_currentState))
        {
            // 检查是否可以切换成其他状态
            var canToStates = _states[_currentState].GetCanToStates();
            foreach (var state in canToStates)
            {
                // 若条件成立，进行状态切换
                if (state.Value.Invoke())
                {
                    _states[_currentState].OnExit(_owner);
                    _currentState = state.Key;
                    _states[_currentState].OnEnter(_owner);
                    break;
                }
            }
            // 获取时间倍率属性
            float timeScale = _owner.Properties.Get<float>(UnitPropertyName.TimeScale);
            // 执行当前状态
            _states[_currentState].OnExecute(_owner, delta * timeScale);
        }
    }
}


