using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Drones.DroneComponents
{
	[RequireComponent(typeof(Collider))]
	public class ItemsGrabber : MonoBehaviour
	{
		[SerializeField] private float grabSpeed;
		public bool CanGrabItems { get; set; }

		public event Action<IResourceItem> OnResourceGrabbed;
		
		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.layer == LayerMask.NameToLayer("ResourceItem") && other.TryGetComponent(out ResourceItemMono item))
			{
				Debug.Log($"{other.gameObject.name}");
				MoveToSelf(item).Forget();
			}
		}

		private async UniTaskVoid MoveToSelf(ResourceItemMono item)
		{
			var sqrSpeed = grabSpeed * grabSpeed;
			var direction = (item.transform.position - transform.position);
			float sqrDistance = direction.sqrMagnitude;
			while (sqrDistance > 0.2f)
			{
				var delta = sqrSpeed < sqrDistance ? grabSpeed : Mathf.Sqrt(sqrDistance);
				
				item.MoveDelta(direction.normalized*delta);
				sqrDistance = direction.sqrMagnitude;
				await UniTask.Yield();
			}
			
			OnResourceGrabbed?.Invoke(item);
		}
	}
}