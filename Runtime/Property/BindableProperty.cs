using System;
using UnityEngine.Scripting;

namespace GameFrameX.Runtime
{
    [Preserve]
    public sealed class BindableProperty<T>
    {
        private T _value;
        private Action<T> _onValueChanged;

        /// <summary>
        /// 值
        /// </summary>
        public T Value
        {
            get { return _value; }
            set
            {
                if (!Equals(_value, value))
                {
                    _value = value;
                    _onValueChanged?.Invoke(_value);
                }
            }
        }

        private BindableProperty()
        {
            _onValueChanged = null;
        }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="defaultValue">默认值</param>
        [Preserve]
        public BindableProperty(T defaultValue = default) : this()
        {
            _value = defaultValue;
        }


        /// <summary>
        /// 注册值变化事件
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        [Preserve]
        public BindableProperty<T> Add(Action<T> callback)
        {
            GameFrameworkGuard.NotNull(callback, nameof(callback));
            _onValueChanged += callback;
            return this;
        }

        /// <summary>
        /// 注册事件
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        [Preserve]
        public BindableProperty<T> RegisterWithInitValue(Action<T> callback)
        {
            GameFrameworkGuard.NotNull(callback, nameof(callback));
            callback?.Invoke(_value);
            return Add(callback);
        }

        /// <summary>
        /// 移除事件
        /// </summary>
        /// <param name="callback">事件</param>
        [Preserve]
        public void Remove(Action<T> callback)
        {
            GameFrameworkGuard.NotNull(callback, nameof(callback));
            _onValueChanged -= callback;
        }

        /// <summary>
        /// 清除事件
        /// </summary>
        [Preserve]
        public void Clear()
        {
            _onValueChanged = null;
        }
    }
}