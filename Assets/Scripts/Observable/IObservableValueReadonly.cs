using System;

namespace Observable
{
	public interface IObservableValueReadonly<T>
	{
		T Value { get; set; }
		event Action<T, T> OnValueChanged;
	}
}