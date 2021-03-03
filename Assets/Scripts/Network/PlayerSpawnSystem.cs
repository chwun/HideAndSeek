using Mirror;
using UnityEngine;

namespace HideAndSeek.Network
{
	public class PlayerSpawnSystem : NetworkBehaviour
	{
		[SerializeField] private GameObject playerPrefab;

		private static Transform spawnPoint;

		public static void SetSpawnPoint(Transform transform)
		{
			spawnPoint = transform;
		}

		public override void OnStartServer()
		{
			CustomNetworkManager.OnServerReadied += SpawnPlayer;
		}

		[ServerCallback]
		private void OnDestroy()
		{
			CustomNetworkManager.OnServerReadied -= SpawnPlayer;
		}

		[Server]
		public void SpawnPlayer(NetworkConnection conn)
		{
			GameObject playerInstance = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
			NetworkServer.Spawn(playerInstance, conn);
		}
	}
}
