using Godot;
using System;
using System.Collections.Generic;

namespace Framework
{
    public partial class StateMachine : Node
    {
        private readonly Dictionary<State, IState> _states = new();

        private UnitNode _owner;

        private State _currentState = State.Idle;

        public State CurrentState => _currentState;

        public override void _Ready()
        {
            _owner = GetParent<UnitNode>();
        }

        public override void _Process(double delta)
        {
            if(_states.ContainsKey(_currentState))
            {
                _states[_currentState].OnExecute(_owner, delta);
            }
        }
    }
}


