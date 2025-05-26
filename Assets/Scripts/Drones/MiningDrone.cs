using System;
using Drones.DroneComponents;
using UnityEngine;

namespace Drones
{
	public class MiningDrone : Drone
	{
		[SerializeField] private Miner minerComponent;
		[SerializeField] private ItemsGrabber itemsGrabber;
		[SerializeField] private Inventory inventory;

		private void Awake()
		{
			minerComponent.OnInteractionPossible += OnMiningPossible;
			itemsGrabber.OnResourceGrabbed += OnResourceGrabbed;
			inventory.OnInventoryFull += OnInventoryFull;
		}

		private void OnInventoryFull(Inventory obj)
		{
			throw new NotImplementedException();
		}

		private void OnResourceGrabbed(IResourceItem obj)
		{
			throw new NotImplementedException();
		}

		private void OnMiningPossible(DroneInteractionComponent obj)
		{
			throw new NotImplementedException();
		}
	}
}