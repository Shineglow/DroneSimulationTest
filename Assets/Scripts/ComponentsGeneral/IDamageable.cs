using System;

namespace ComponentsGeneral
{
	public interface IDamageable
	{
		event Action<float> OnTakeDamage;
		void TakeDamage(float damage);
		bool CanTakeDamage();
	}
}