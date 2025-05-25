

using System;
using UnityEngine;

namespace Observable
{
	[Serializable]
	public class ObservableValue<T> : IObservableValueReadonly<T>
	{
		[SerializeField]
		private T _value;

		public T Value
		{
			get => _value;
			set
			{
				if (Equals(_value, value)) return;
				T old = _value;
				_value = value;
				OnValueChanged?.Invoke(old, value);
			}
		}

		public event Action<T, T> OnValueChanged;
	}
}