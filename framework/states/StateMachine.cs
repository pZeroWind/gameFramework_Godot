using Framework.Runtime;
using Godot;
using System;
using System.Collections.Generic;

namespace Framework;

public partial class StateMachine : Node
{
    /// <summary>
    /// 状态字典
    /// </summary>
    private readonly Dictionary<State, IState> _states = [];

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
        foreach (var node in GetChildren())
        {
            if (node is StateNode stateNode)
                _states.Add(stateNode.State, stateNode);
        }
        GD.Print(_states.Count);
        foreach (var state in _states) GD.Print(state.Key.ToString());
    }

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
        _states.Remove(key);
    }

    public override void _Process(double delta)
    {
        //GD.Print(delta, _currentState.ToString());
        // 若状态字典中存在当前状态
        if (_states.TryGetValue(_currentState, out IState value))
        {
            
            // 检查是否可以切换成其他状态
            var canToStates = value.GetCanToStates();
            foreach (var state in canToStates)
            {
                // 若条件成立，进行状态切换
                if (state.Value.Invoke())
                {
                    value.OnExit(_owner);
                    _currentState = state.Key;
                    _states[_currentState].OnEnter(_owner);
                    break;
                }
            }
            // 获取时间倍率属性
            float timeScale = _owner.PropsMgr.Get<float>(UnitPropertyName.TimeScale);
            // 执行当前状态
            _states[_currentState].OnExecute(_owner, delta * timeScale);
        }
    }
}


