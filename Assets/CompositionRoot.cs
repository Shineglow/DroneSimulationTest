using System.Collections;
using System.Collections.Generic;
using Systems;
using Systems.DroneSystem;
using UnityEngine;
using UnityEngine.Serialization;

public class CompositionRoot : MonoBehaviour
{
   [FormerlySerializedAs("droneSystem")] [SerializeField] private DroneSystemMono droneSystemMono;
   [SerializeField] private ResourcesSystem resourcesSystem;

   public DroneSystemMono GetDroneSystem()
   {
      return droneSystemMono;
   }
}
