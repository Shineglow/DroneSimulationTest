using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Drones.DroneComponents
{
    public abstract class DroneInteractionComponent : MonoBehaviour
    {
        public abstract event Action<DroneInteractionComponent> OnInteractionPossible;
        [field: SerializeField]
        public int InteractionPriority { get; protected set; }

        public abstract UniTaskVoid Interact(CancellationToken interactCancellationToken);
        public abstract void Stop();
    }
}