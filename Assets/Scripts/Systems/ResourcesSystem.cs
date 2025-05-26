using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameResources;
using General.ObjectsPool;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Systems
{
    public class ResourcesSystem : MonoBehaviour
    {
        private PoolGeneric<Resource, Resource> resourcesPool;
        private PoolGeneric<ResourceItemMono, IResourceItem> resourceItemsPool;
        private HashSet<Resource> resources = new();

        [SerializeField] private Transform spawnCenter;
        [SerializeField] private Vector2 halfSize;
        [SerializeField] private int initialCount;

        [SerializeField] private Resource resourcePrefab;
        [SerializeField] private ResourceItemMono resourceItemsPrefab;

        [SerializeField] private float resourceSpawnDelay = 10f;

        private void Awake()
        {
            resourceItemsPool = new PoolGeneric<ResourceItemMono, IResourceItem>(
                 () => Instantiate(resourceItemsPrefab),
                 resource => resource.gameObject.SetActive(false),
                 resource => resource.gameObject.SetActive(true),
                 4
                );
            resourcesPool = new PoolGeneric<Resource, Resource>(
                                                                () =>
                                                                {
                                                                    var instance = Instantiate(resourcePrefab);
                                                                    instance.SetPool(resourceItemsPool);
                                                                    instance.OnDestroyed += OnResourceDestroyed;
                                                                    return instance;
                                                                },
                                                                resource => resource.gameObject.SetActive(false),
                                                                resource => resource.gameObject.SetActive(true),
                                                                4);

            for (int i = 0; i < initialCount; i++)
            {
                SpawnNewResource();
            }

            SpawnOverTime().Forget();
        }

        private async UniTaskVoid SpawnOverTime()
        {
            while (gameObject.activeSelf)
            {
                await UniTask.Delay(Convert.ToInt32(resourceSpawnDelay*1000));
                
            }
        }

        private void OnResourceDestroyed(Resource resource)
        {
            resourcesPool.Free(resource);
            SpawnNewResource();
        }

        private void SpawnNewResource()
        {
            var resource = resourcesPool.Get();
            Vector3 vector3Pos = new Vector3(Random.Range(-halfSize.x, halfSize.x), 0f, Random.Range(-halfSize.y, halfSize.y));
            resource.transform.position = spawnCenter.position + vector3Pos;
            resource.transform.rotation = Quaternion.Euler(0f, Random.Range(0, 360), 0f);
        }

        public Vector3 GetNearestToObjectResource(Vector3 startPosition)
        {
            Transform result = null;
            float lowestDistance = float.PositiveInfinity;
            
            foreach (var resource in resources)
            {
                var sqrMagnitude = (resource.transform.position - startPosition).sqrMagnitude;
                if (sqrMagnitude < lowestDistance)
                {
                    lowestDistance = sqrMagnitude;
                    result = resource.transform;
                }
            }
            
            return result == null ? Vector3.zero : result.position;
        }
    }
}
