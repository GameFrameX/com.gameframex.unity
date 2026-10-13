using System;
using UnityEngine.Scripting;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 可绑定属性，值变化时触发注册的回调。
    /// </summary>
    /// <remarks>
    /// Bindable property that invokes registered callbacks when its value changes.
    /// </remarks>
    [Preserve]
    public sealed class BindableProperty<T>
    {
        private T _value;
        private Action<T> _onValueChanged;

        /// <summary>
        /// 值
        /// </summary>
        /// <remarks>
        /// Gets or sets the value and triggers the change callback when the new value differs.
        /// </remarks>
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
        /// <remarks>
        /// Initializes a new instance with an optional default value.
        /// </remarks>
        /// <param name="defaultValue">默认值 / Default value</param>
        [Preserve]
        public BindableProperty(T defaultValue = default) : this()
        {
            _value = defaultValue;
        }


        /// <summary>
        /// 注册值变化事件
        /// </summary>
        /// <remarks>
        /// Registers a value-changed callback.
        /// </remarks>
        /// <param name="callback">值变化回调 / Value-changed callback</param>
        /// <returns>当前可绑定属性 / The current bindable property</returns>
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
        /// <remarks>
        /// Registers a callback and immediately invokes it with the current value.
        /// </remarks>
        /// <param name="callback">事件回调 / Event callback</param>
        /// <returns>当前可绑定属性 / The current bindable property</returns>
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
        /// <remarks>
        /// Removes an event callback.
        /// </remarks>
        /// <param name="callback">事件 / Event callback</param>
        [Preserve]
        public void Remove(Action<T> callback)
        {
            GameFrameworkGuard.NotNull(callback, nameof(callback));
            _onValueChanged -= callback;
        }

        /// <summary>
        /// 清除事件
        /// </summary>
        /// <remarks>
        /// Clears all event callbacks.
        /// </remarks>
        [Preserve]
        public void Clear()
        {
            _onValueChanged = null;
        }
    }
}