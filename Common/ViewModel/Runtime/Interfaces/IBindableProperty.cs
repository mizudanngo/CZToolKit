#region 注 释
/***
 *
 *  Title:
 *  
 *  Description:
 *  
 *  Date:
 *  Version:
 *  Writer: 半只龙虾人
 *  Github: https://github.com/HalfLobsterMan
 *  Blog: https://www.crosshair.top/
 *
 */
#endregion
using System;

namespace CZToolKit.Common.ViewModel
{
    public interface IBindableProperty
    {
        event Action<object> onBoxedValueChanged;

        object ValueBoxed { get; set; }
        Type ValueType { get; }

        void SetValueWithNotify(object value);
        void SetValueWithoutNotify(object value);
        IBindableProperty<TOut> AsBindableProperty<TOut>();
        void NotifyValueChanged();
        void ClearValueChangedEvent();
    }

    public interface IBindableProperty<T> : IBindableProperty
    {
        event Action<T> onValueChanged;

        T Value { get; set; }

        void SetValueWithNotify(T value);
        void SetValueWithoutNotify(T value);
        void RegisterValueChangedEvent(Action<T> onValueChanged);
        void UnregisterValueChangedEvent(Action<T> onValueChanged);
        void NotifyValueChanged();
        void ClearValueChangedEvent();
    }
}
