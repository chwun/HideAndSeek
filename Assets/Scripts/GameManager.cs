using UnityEngine;

namespace HideAndSeek
{
	public class GameManager : MonoBehaviour
	{
		public GameObject PlayerPrefab;
		public GameObject SpawnPosition;

		private GameObject player;

		void Start()
		{
			SpawnPlayer();
		}

		private void SpawnPlayer()
		{
			player = Instantiate(PlayerPrefab, SpawnPosition.transform.position, Quaternion.identity);
			player.name = "Player";
		}
	}
}
