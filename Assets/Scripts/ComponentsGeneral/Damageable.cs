using System;
using Observable;
using UnityEngine;

namespace ComponentsGeneral
{
    public class Damageable : MonoBehaviour, IDamageable
    {
        public event Action<float> OnTakeDamage;

        private IObservableValueReadonly<bool> _canTakeDamage;
        public void Init(IObservableValueReadonly<bool> canTakeDamage)
        {
            _canTakeDamage = canTakeDamage;
        }

        public void TakeDamage(float damage)
        {
            if (_canTakeDamage is { Value: true })
            {
                OnTakeDamage?.Invoke(damage);
            }
        }

        public bool CanTakeDamage()
        {
            return _canTakeDamage is { Value: true };
        }
    }
}
