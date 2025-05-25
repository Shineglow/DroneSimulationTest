using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
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
			if (other.gameObject.layer == LayerMask.NameToLayer("Item") && other.TryGetComponent(out ResourceItemItemMono item))
			{
				MoveToSelf(item).Forget();
			}
		}

		private async UniTaskVoid MoveToSelf(ResourceItemItemMono item)
		{
			var position = transform.position;
			await item.transform
					  .DOMove(position, (position - item.transform.position).magnitude / grabSpeed)
					  .AsyncWaitForCompletion()
					  .AsUniTask();
			
			OnResourceGrabbed?.Invoke(item);
		}
	}
}