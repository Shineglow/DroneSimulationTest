using System;
using UnityEngine;

namespace ComponentsGeneral
{
	[RequireComponent(typeof(BoxCollider))]
	public class SpawnZone : MonoBehaviour
	{
		private BoxCollider _collider;
		
		public bool IsSpawnZoneFree { get; private set; }
		public event Action OnZoneFree;

		private void Awake()
		{
			_collider = GetComponent<BoxCollider>();
			var results = Physics.OverlapBox(_collider.center, _collider.bounds.extents, transform.rotation, LayerMask.GetMask("Drone"));
			if (results.Length > 0)
			{
				IsSpawnZoneFree = false;
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if(other.gameObject.layer == LayerMask.NameToLayer("Drone"))
				OnZoneFree?.Invoke();
		}
	}
}