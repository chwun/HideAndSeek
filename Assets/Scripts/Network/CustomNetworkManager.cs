using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HideAndSeek.Network
{
	public class CustomNetworkManager : NetworkManager
	{

		[Header("Custom")]
		[SerializeField] private int minPlayers = 2;
		[Scene] [SerializeField] private string menuScene;
		[SerializeField] private NetworkRoomPlayer roomPlayerPrefab;

		[SerializeField] private NetworkGamePlayer gamePlayerPrefab;
		[SerializeField] private GameObject playerSpawnSystem;

		[HideInInspector]
		public List<NetworkRoomPlayer> RoomPlayers { get; } = new List<NetworkRoomPlayer>();

		[HideInInspector]
		public List<NetworkGamePlayer> GamePlayers { get; } = new List<NetworkGamePlayer>();

		public static event Action ClientConnected;
		public static event Action ClientDisconnected;
		public static event Action ReturnToGameMenu;
		public static event Action<NetworkConnection> OnServerReadied;

		public override void OnStartServer()
		{
			spawnPrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs").ToList();
		}

		public override void OnStartClient()
		{
			var spawnablePrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs");

			foreach (var prefab in spawnablePrefabs)
			{
				ClientScene.RegisterPrefab(prefab);
			}
		}

		public override void OnClientConnect(NetworkConnection conn)
		{
			base.OnClientConnect(conn);

			ClientConnected?.Invoke();
		}

		public override void OnClientDisconnect(NetworkConnection conn)
		{
			base.OnClientDisconnect(conn);

			ClientDisconnected?.Invoke();
			ReturnToGameMenu?.Invoke();
		}

		public override void OnServerConnect(NetworkConnection conn)
		{
			if (numPlayers >= maxConnections)
			{
				conn.Disconnect();
				return;
			}

			// prevents players from joining when game is already in progress
			if (SceneManager.GetActiveScene().path != menuScene)
			{
				conn.Disconnect();
				return;
			}
		}

		public override void OnServerDisconnect(NetworkConnection conn)
		{
			if (conn.identity != null)
			{
				var player = conn.identity.GetComponent<NetworkRoomPlayer>();

				RoomPlayers.Remove(player);

				NotifyHostOfReadyState();
			}

			base.OnServerDisconnect(conn);
		}

		public override void OnStopServer()
		{
			RoomPlayers.Clear();
			ReturnToGameMenu?.Invoke();
		}

		public override void OnServerAddPlayer(NetworkConnection conn)
		{
			if (SceneManager.GetActiveScene().path == menuScene)
			{
				bool isHost = !RoomPlayers.Any();

				NetworkRoomPlayer roomPlayerInstance = Instantiate(roomPlayerPrefab);
				roomPlayerInstance.IsHost = isHost;

				NetworkServer.AddPlayerForConnection(conn, roomPlayerInstance.gameObject);
			}
		}

		public void NotifyHostOfReadyState()
		{
			RoomPlayers.FirstOrDefault(x => x.IsHost)?.HandleReadyToStart(IsReadyToStart());
		}

		private bool IsReadyToStart()
		{
			if (numPlayers < minPlayers)
			{
				return false;
			}

			if (RoomPlayers.Any(player => !player.IsReady))
			{
				return false;
			}

			return true;
		}

		public void StartGame()
		{
			if (SceneManager.GetActiveScene().path == menuScene)
			{
				if (!IsReadyToStart())
				{
					return;
				}

				ServerChangeScene("LevelTestMultiplayer");
			}
		}

		public override void ServerChangeScene(string newSceneName)
		{
			// from menu to game
			if ((SceneManager.GetActiveScene().path == menuScene) && (newSceneName.StartsWith("Level")))
			{
				for (int i = RoomPlayers.Count - 1; i >= 0; i--)
				{
					var conn = RoomPlayers[i].connectionToClient;
					var gamePlayerInstance = Instantiate(gamePlayerPrefab);
					gamePlayerInstance.SetDisplayName(RoomPlayers[i].DisplayName);

					NetworkServer.Destroy(conn.identity.gameObject);

					NetworkServer.ReplacePlayerForConnection(conn, gamePlayerInstance.gameObject, true);
				}
			}

			base.ServerChangeScene(newSceneName);
		}

		public override void OnServerSceneChanged(string sceneName)
		{
			if (sceneName.StartsWith("Level"))
			{
				GameObject playerSpawnSystemInstance = Instantiate(playerSpawnSystem);
				NetworkServer.Spawn(playerSpawnSystemInstance);
			}
		}

		public override void OnServerReady(NetworkConnection conn)
		{
			base.OnServerReady(conn);

			OnServerReadied?.Invoke(conn);
		}
	}
}
