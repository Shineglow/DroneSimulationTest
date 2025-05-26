using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Drones.DroneComponents
{
    [RequireComponent(typeof(Collider))]
    public class MovableAi : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private Transform moveTo;
        [SerializeField] private float destinationEpsilon;
        private Vector3 _targetPosition;
        private bool _isStopped;

        public event Action<Transform> OnStop;

        private void Start()
        {
            if (moveTo != null)
            {
                _targetPosition = moveTo.position;
                MoveToPosition(_targetPosition);
            }
        }

        public void MoveToPosition(Vector3 position)
        {
            _targetPosition = position;
            navMeshAgent.SetDestination(position);
            WaitForStop().Forget();
        }

        public void SetPosition(Vector3 position)
        {
            rb.gameObject.SetActive(false);
            rb.transform.position = position;
            rb.gameObject.SetActive(true);
        }

        private void OnTriggerEnter(Collider other)
        {
            if ((other.transform.position - _targetPosition).sqrMagnitude < destinationEpsilon)
            {
                _isStopped = true;
            }
        }

        private async UniTaskVoid WaitForStop()
        {
            _isStopped = false;
            Vector3 previousPosition = transform.position - Vector3.left;
            Quaternion previousRotation = transform.rotation * Quaternion.Euler(90, 0, 0);
        
            while (!_isStopped)
            {
                _isStopped = true;
                if (previousPosition != transform.position)
                {
                    previousPosition = transform.position;
                    _isStopped = false;
                }

                if (previousRotation != transform.rotation)
                {
                    previousRotation = transform.rotation;
                    _isStopped = false;
                }

                await UniTask.Yield();
            }

            OnStop?.Invoke(transform);
        }
    }
}
