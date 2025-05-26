using Drones.DroneComponents;
using UnityEngine;

namespace Drones
{
    public class Drone : MonoBehaviour
    {
        [SerializeField] private MovableAi movable;

        public void MoveTo(Vector3 position)
        {
            movable.MoveToPosition(position);
        }

        public void SetPosition(Vector3 position)
        {
            movable.SetPosition(position);
        }
        
        // private void Awake()
        // {
        //     foreach (var droneInteractionComponent in droneInteractionComponents)
        //     {
        //         droneInteractionComponent.OnInteractionPossible += OnInteractionPossible;
        //     }
        // }
        //
        // public void AddDroneComponent(DroneInteractionComponent component)
        // {
        //     component.transform.SetParent(transform);
        //     component.OnInteractionPossible += OnInteractionPossible;
        //     droneInteractionComponents.Add(component);
        // }
        //
        // private void OnInteractionPossible(DroneInteractionComponent component)
        // {
        //     if (_activeInteraction != null)
        //     {
        //         if (component.InteractionPriority > _activeInteraction.InteractionPriority)
        //         {
        //             _cts.Cancel();
        //         }
        //     }
        //
        //     _activeInteraction = component;
        //     _cts?.Dispose();
        //     _cts = new CancellationTokenSource();
        //     _activeInteraction.Interact(_cts.Token).Forget();
        // }
    }
}
