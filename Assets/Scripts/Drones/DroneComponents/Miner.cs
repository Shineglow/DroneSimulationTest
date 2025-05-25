using System;
using System.Collections.Generic;
using System.Threading;
using ComponentsGeneral;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Drones.DroneComponents
{
	[RequireComponent(typeof(Collider))]
	public class Miner : DroneInteractionComponent
	{
		public override event Action<DroneInteractionComponent> OnInteractionPossible;
		private List<IDamageable> _resourceInArea = new();
		[SerializeField] private float resourceDamage;
		[SerializeField] private float mineSpeed;
		private int _delayMilliseconds;

		private void Awake()
		{
			_delayMilliseconds = Convert.ToInt32(1 / mineSpeed * 1000);
		}

		public override async UniTaskVoid Interact(CancellationToken interactCancellationToken)
		{
			_interactionStopRequested = false;
			while (_resourceInArea[0].CanTakeDamage())
			{
				_resourceInArea[0].TakeDamage(resourceDamage);
				if (interactCancellationToken.IsCancellationRequested || _interactionStopRequested)
				{
					return;
				}
				await UniTask.Delay(_delayMilliseconds);
			}
		}

		private bool _interactionStopRequested;
		public override void Stop()
		{
			_interactionStopRequested = true;
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.layer == LayerMask.NameToLayer("Resource")
				&& other.TryGetComponent(out IDamageable resource))
			{
				_resourceInArea.Add(resource);
				OnInteractionPossible?.Invoke(this);
			}
		}
	}
}