using GameResources;
using UnityEngine;

public interface IResourceItem
{
	EResources Resource { get; }
	Vector3 Position { get; set; }
	Quaternion Rotation { get; set; }
	void Throw(Vector3 throwDirection);
}