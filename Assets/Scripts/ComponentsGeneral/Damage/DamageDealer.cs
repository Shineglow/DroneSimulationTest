using UnityEngine;

namespace ComponentsGeneral.Damage
{
	public class DamageDealer : MonoBehaviour, IDamageDealer
	{
		public Vector3 Position => transform.position;
	}
}