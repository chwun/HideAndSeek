using UnityEngine;

namespace HideAndSeek.Network
{
	public class PlayerSpawnPoint : MonoBehaviour
	{
		private void Awake() => PlayerSpawnSystem.SetSpawnPoint(transform);

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.blue;
			Gizmos.DrawSphere(transform.position, 1f);
			Gizmos.color = Color.green;
			Gizmos.DrawLine(transform.position, transform.position + transform.forward * 4);
		}
	}
}
