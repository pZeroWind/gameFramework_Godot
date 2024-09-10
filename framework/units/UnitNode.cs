using System.Collections.Generic;
using Godot;

namespace Framework
{

    public enum UnitProperty
    {
        /// <summary>
        /// 体力
        /// </summary>
        Hp,

        /// <summary>
        /// 最大体力
        /// </summary>
        MaxHp,

        /// <summary>
        /// 法力
        /// </summary>
        Mp,

        /// <summary>
        /// 最大法力
        /// </summary>
        MaxMp,

        /// <summary>
        /// 耐力
        /// </summary>
        Sp,

        /// <summary>
        /// 最大耐力
        /// </summary>
        MaxSp,

        /// <summary>
        /// 移动速度
        /// </summary>
        Speed,

        /// <summary>
        /// 时间倍率
        /// </summary>
        TimeScale,
    }

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
            if(_value is S v) return v;
            return default;
        }

        public void SetValue<S>(S value)
        {
            if(value is T v) _value = v;
        }
    }

    public partial class UnitNode : CharacterBody2D
    {
        private Dictionary<UnitProperty, ITValue> _properties = new();

        public UnitNode()
        {
            SetProperty<float>(UnitProperty.TimeScale, 1f);
        }

        /// <summary>
        /// 获取属性
        /// </summary>
        public T GetProperty<T>(UnitProperty key)
        {
            if(_properties.ContainsKey(key))
            {
                return _properties[key].GetValue<T>();
            }
            return default;
        }

        /// <summary>
        /// 设置属性
        /// </summary>
        public void SetProperty<T>(UnitProperty key, T value)
        {
            if (!_properties.ContainsKey(key))
            {
                _properties.Add(key, new TValue<T>());
            }
            _properties[key].SetValue(value);
        }
    }
}