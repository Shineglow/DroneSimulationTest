namespace General.ObjectsPool
{
	public interface IPool<T>
	{
		T Get();
		void Free(T instance);
	}
}