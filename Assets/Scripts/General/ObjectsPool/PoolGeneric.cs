using System;
using System.Collections.Generic;

namespace General.ObjectsPool
{
	public class PoolGeneric<T, T1> : IPool<T1> where T : T1
	{
		private Stack<T> _free;
		private HashSet<T> _inUse;
		private readonly Func<T> _createNewInstance;
		private readonly Action<T> _onFreeInstance;
		private readonly Action<T> _onGetInstance;

		public PoolGeneric(Func<T> createNewInstance, Action<T> onFreeInstance, Action<T> onGetInstance, int capacity)
		{
			_createNewInstance = createNewInstance;
			_onFreeInstance = onFreeInstance;
			_onGetInstance = onGetInstance;
			_free = new Stack<T>(capacity);
			for (int i = 0; i < capacity; i++)
			{
				_free.Push(createNewInstance());
			}

			_inUse = new();
		}

		public T1 Get()
		{
			var result = _free.Count > 0 ? _free.Pop() : _createNewInstance();
			_inUse.Add(result);
			_onGetInstance?.Invoke(result);
			return result;
		}

		public void Free(T1 instance)
		{
			T baseInstance = (T)instance;
			_inUse.Remove(baseInstance);
			_onFreeInstance(baseInstance);
			_free.Push(baseInstance);
		}
	}
}