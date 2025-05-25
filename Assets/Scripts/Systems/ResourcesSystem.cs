using System.Collections.Generic;
using GameResources;
using UnityEngine;

namespace Systems
{
    public class ResourcesSystem : MonoBehaviour
    {
        [SerializeField] private List<Resource> resources;

        private void Awake()
        {
            foreach (var res in resources)
            {
                res.OnDestroyed += OnResourceDestroyed;
            }
        }

        private void OnResourceDestroyed(Resource resource)
        {
            resource.gameObject.SetActive(false);
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

            return result.position;
        }
    }
}
