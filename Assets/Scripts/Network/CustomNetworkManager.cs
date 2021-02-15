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
		[Scene] [SerializeField] private string menuScene;
		[SerializeField] private NetworkRoomPlayer roomPlayerPrefab;

		public static event Action OnClientConnected;
		public static event Action OnClientDisconnected;

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

			OnClientConnected?.Invoke();
		}

		public override void OnClientDisconnect(NetworkConnection conn)
		{
			base.OnClientDisconnect(conn);

			OnClientDisconnected?.Invoke();
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

		public override void OnServerAddPlayer(NetworkConnection conn)
		{
			if (SceneManager.GetActiveScene().path == menuScene)
			{
				NetworkRoomPlayer roomPlayerInstance = Instantiate(roomPlayerPrefab);

				NetworkServer.AddPlayerForConnection(conn, roomPlayerInstance.gameObject);
			}
		}
	}
}
