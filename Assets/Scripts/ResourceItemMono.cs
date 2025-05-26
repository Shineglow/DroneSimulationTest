using GameResources;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ResourceItemMono : MonoBehaviour, IResourceItem
{
	[SerializeField] private Rigidbody rb;
	public EResources Resource { get; set; }

	public Vector3 Position
	{
		get => transform.position; 
		set => transform.position = value;
	}

	public Quaternion Rotation
	{
		get => transform.rotation; 
		set => transform.rotation = value;
	}

	private void Awake()
	{
		rb ??= GetComponent<Rigidbody>();
	}

	public void Throw(Vector3 speed)
	{
		rb.AddForce(speed, ForceMode.Force);
	}

	public void MoveDelta(Vector3 directionDelta)
	{
		rb.MovePosition(rb.position + directionDelta);
	}
}