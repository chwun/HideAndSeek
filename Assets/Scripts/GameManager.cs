using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HideAndSeek
{
	public class GameManager : MonoBehaviour
	{
		public GameObject PlayerPrefab;
		public GameObject BotPrefab;
		public GameObject SpawnPositionSearchingPlayers;
		public GameObject SpawnPositionHidingPlayers;

		private List<Player> hidingPlayers;
		private List<Player> searchingPlayers;

		void Start()
		{
			SpawnSearchingPlayers();
			SpawnHidingPlayers();
		}

		private void SpawnSearchingPlayers()
		{
			searchingPlayers = new List<Player>();
			GameObject playerObject = Instantiate(PlayerPrefab, SpawnPositionSearchingPlayers.transform.position, SpawnPositionSearchingPlayers.transform.rotation);
			Player player = new Player(playerObject, "Jannis", true);
			searchingPlayers.Add(player);
			playerObject.GetComponent<PlayerManager>().SetPlayer(player);
			playerObject.name = "SearchingPlayer";
		}

		private void SpawnHidingPlayers()
		{
			hidingPlayers = new List<Player>();

			for (int i = 0; i < 20; i++)
			{
				GameObject playerObject = Instantiate(BotPrefab, SpawnPositionHidingPlayers.transform.position + new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f)), SpawnPositionHidingPlayers.transform.rotation);
				Player player = new Player(playerObject, "Bot " + i, false);
				hidingPlayers.Add(player);
				// playerObject.GetComponent<PlayerManager>().SetPlayer(player);
				playerObject.name = "HidingPlayer";
			}


		}

		public void Catch(GameObject searching, GameObject hiding)
		{
			Player hidingPlayer = hidingPlayers.Find(x => x.PlayerObject == hiding);
			Player searchingPlayer = searchingPlayers.Find(x => x.PlayerObject == searching);

			if (hidingPlayer.IsAlive)
			{
				hidingPlayer.PlayerObject.GetComponent<BotMovement>().Catch();
				hidingPlayer.IsAlive = false;
				searchingPlayer.Points++;
				Debug.Log(searchingPlayer.Name + " hat " + hidingPlayer.Name + " gefangen.");
			}

			if (hidingPlayers.All(x => !x.IsAlive))
			{
				foreach (Player player in searchingPlayers)
				{
					Debug.Log(player.Name + ": " + player.Points + " Punkte");
				}
			}
		}
	}
}
