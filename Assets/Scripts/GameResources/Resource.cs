using System;
using ComponentsGeneral;
using Observable;
using UnityEngine;

namespace GameResources
{
    public class Resource : MonoBehaviour
    {
        [SerializeField] private ObservableValue<float> durability;
        public IObservableValueReadonly<float> Durability => durability;

        [SerializeField] private Damageable _damageable;

        public event Action<Resource> OnDestroyed;

        [SerializeField] private ObservableValue<bool> canTakeDamage;

        private void Awake()
        {
            _damageable ??= GetComponent<Damageable>();
            _damageable.OnTakeDamage += OnDamageTaken;
            _damageable.Init(canTakeDamage);
        }

        private void OnDamageTaken(float damage)
        {
            durability.Value -= Mathf.Min(damage, durability.Value);
            if (durability.Value < 0.00001f)
            {
                canTakeDamage.Value = false;
                OnDestroyed?.Invoke(this);
            }
        }
    }
}
