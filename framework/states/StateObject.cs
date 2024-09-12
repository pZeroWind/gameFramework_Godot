using Godot;
using System;
using System.Collections.Generic;

namespace Framework
{
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
		/// <param name="fTick"></param>
		void OnExecute(UnitNode node, double fTick);

		/// <summary>
		/// 退出状态时调用
		/// </summary>
		void OnExit(UnitNode node);
	}

	/// <summary>
	/// 状态抽象实体
	/// </summary>
	public abstract class StateObject : IState
	{
		private readonly Dictionary<State, Func<bool>> _canToState = new();

		public Dictionary<State, Func<bool>> GetCanToStates() => _canToState;

		/// <summary>
		/// 状态构建时
		/// </summary>
		protected abstract void OnCreating();

		public StateObject()
		{
			OnCreating();
		}

		public virtual void OnEnter(UnitNode node) { }

		public virtual void OnExecute(UnitNode node, double fTick) { }

		public virtual void OnExit(UnitNode node) { }

		/// <summary>
		///  添加可转移状态
		/// </summary>
		protected void AddCanToState(State key, Func<bool> condition)
		{
			if (!_canToState.ContainsKey(key))
				_canToState.Add(key, condition);
		}
	}
}

