using System;
using ComponentsGeneral;
using General.ObjectsPool;
using Observable;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameResources
{
    public class Resource : MonoBehaviour
    {
        [SerializeField] private ObservableValue<float> durability;
        public IObservableValueReadonly<float> Durability => durability;

        [SerializeField] private Damageable _damageable;

        public event Action<Resource> OnDestroyed;

        [SerializeField] private ObservableValue<bool> canTakeDamage;

        [SerializeField] private ResourceItemMono itemPrefab;
        [SerializeField] private MinMaxFloat itemInitialSpeed;
        
        private IPool<IResourceItem> _resourceItemsPool;

        private void Awake()
        {
            _damageable ??= GetComponent<Damageable>();
            _damageable.OnTakeDamage += OnDamageTaken;
            _damageable.Init(canTakeDamage);
        }

        public void SetPool(IPool<IResourceItem> pool)
        {
            _resourceItemsPool = pool;
        }

        private void OnDamageTaken(float damage)
        {
            durability.Value -= Mathf.Min(damage, durability.Value);
            var item = _resourceItemsPool.Get(); // transform.position+Vector3.up*0.91f, Random.rotation
            item.Position = transform.position + Vector3.up * 0.91f;
            item.Rotation = Random.rotation;

            float pitch = Random.Range(45, 70);
            float yaw = Random.Range(0f, 360f);

            Quaternion rot = Quaternion.Euler(-pitch, yaw, 0f);
            var throwDirection = rot * Vector3.forward;
            item.Throw(throwDirection * Random.Range(itemInitialSpeed.Min, itemInitialSpeed.Max));
            
            if (durability.Value < 0.00001f)
            {
                canTakeDamage.Value = false;
                OnDestroyed?.Invoke(this);
            }
        }
    }
    
    [Serializable]
    public struct MinMaxFloat
    {
        public float Min;
        public float Max;
    }
}
