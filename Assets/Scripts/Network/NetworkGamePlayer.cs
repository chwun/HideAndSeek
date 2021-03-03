using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HideAndSeek.Network;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HideAndSeek
{
	public class NetworkGamePlayer : NetworkBehaviour
	{
		[SyncVar]
		private string displayName = "Loading...";

		private CustomNetworkManager room;
		public CustomNetworkManager Room
		{
			get
			{
				if (room == null)
				{
					room = NetworkManager.singleton as CustomNetworkManager;
				}

				return room;
			}
		}

		public override void OnStartClient()
		{
			DontDestroyOnLoad(gameObject);

			Room.GamePlayers.Add(this);
		}

		public override void OnStopClient()
		{
			Room.GamePlayers.Remove(this);
		}

		[Server]
		public void SetDisplayName(string displayName)
		{
			this.displayName = displayName;
		}
	}
}
