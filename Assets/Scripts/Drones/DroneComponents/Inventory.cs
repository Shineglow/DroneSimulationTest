using System;
using System.Collections.Generic;
using GameResources;
using UnityEngine;

namespace Drones.DroneComponents
{
	public class Inventory : MonoBehaviour
	{
		[SerializeField] private int MaxItemsCount;
		private Dictionary<EResources, (EResources, int)> _resourceToCount = new();
		
		[field: SerializeField] public int CurrentItemsCount { get; private set; }

		public event Action<Inventory> OnInventoryFull;
		
		
	}
}