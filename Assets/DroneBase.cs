using System.Collections.Generic;
using ComponentsGeneral;
using Cysharp.Threading.Tasks;
using Drones;
using General.ObjectsPool;
using UnityEngine;

public class DroneBase : MonoBehaviour
{
	private List<MiningDrone> _drones;
    private PoolGeneric<MiningDrone, MiningDrone> _dronesPool;

    [SerializeField] private MiningDrone dronePrefab;
    [SerializeField] private int dronesCount;

	[SerializeField] private List<SpawnZone> _spawnZones;

	private void Awake()
	{
		_drones = new(dronesCount);
		_dronesPool = new PoolGeneric<MiningDrone, MiningDrone>(
			() => Instantiate(dronePrefab),
			drone => drone.gameObject.SetActive(false),
			drone => drone.gameObject.SetActive(true),
			dronesCount);

		for (int i = 0; i < dronesCount; i++)
		{
			Spawn();
		}
	}

	public void SetDronesCount(int dronesCount)
	{
		while (dronesCount > _drones.Count)
		{
			Spawn();
		}

		while (dronesCount < _drones.Count)
		{
			DespawnRandom();
		}
	}

	private void Spawn()
	{
		SpawnAsync().Forget();
	}

	private void DespawnRandom()
	{
		int randomIndex = Random.Range(0, _drones.Count);
		if (randomIndex != _drones.Count - 1)
		{
			(_drones[randomIndex], _drones[^1]) = (_drones[^1], _drones[randomIndex]);
		}
		var free = _drones[^1];
		_drones.RemoveAt(_drones.Count - 1);
		_dronesPool.Free(free);
	}

	private async UniTaskVoid SpawnAsync()
	{
		SpawnZone targetZone = null;
		while (targetZone == null)
		{
			foreach (var spawnZone in _spawnZones)
			{
				if (spawnZone.IsSpawnZoneFree)
				{
					targetZone = spawnZone;
					break;
				}
			}
			await UniTask.Yield();
		}
		
		var drone = _dronesPool.Get();
		_drones.Add(drone);
		drone.SetPosition(targetZone.transform.position);
	}
}
