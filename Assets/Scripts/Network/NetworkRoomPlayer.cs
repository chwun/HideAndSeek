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
	public class NetworkRoomPlayer : NetworkBehaviour
	{
		[Header("UI")]
		[SerializeField] private GameObject lobbyUI;
		[SerializeField] private List<TMP_Text> playerNameTexts = new List<TMP_Text>();
		[SerializeField] private List<Image> playerReadyImages = new List<Image>();
		[SerializeField] private Button buttonReady;
		[SerializeField] private Button buttonStartGame;

		[SyncVar(hook = nameof(HandleDisplayNameChanged))]
		[HideInInspector]
		public string DisplayName = "Loading...";

		[SyncVar(hook = nameof(HandleReadyStatusChanged))]
		[HideInInspector]
		public bool IsReady = false;

		private bool isHost;
		public bool IsHost
		{
			get
			{
				return isHost;
			}
			set
			{
				isHost = value;
			}
		}

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

		private static Color GreenColor = (Color)new Color32(8, 150, 23, 255);
		private static Color RedColor = (Color)new Color32(150, 4, 10, 255);
		private const string GreenColorHex = "#089617";
		private const string RedColorHex = "#96040A";

		public override void OnStartAuthority()
		{
			CmdSetDisplayName(PlayerPrefs.GetString(Constants.PlayerPrefsPlayerNameKey));

			lobbyUI.SetActive(true);
			buttonStartGame.gameObject.SetActive(IsHost);
		}

		public override void OnStartClient()
		{
			Room.RoomPlayers.Add(this);

			UpdateDisplay();
		}

		public override void OnStopClient()
		{
			Room.RoomPlayers.Remove(this);

			UpdateDisplay();
		}

		private void HandleDisplayNameChanged(string oldValue, string newValue) => UpdateDisplay();
		private void HandleReadyStatusChanged(bool oldValue, bool newValue) => UpdateDisplay();

		private void UpdateDisplay()
		{
			// only execute this on own player object (which has authority):
			if (!hasAuthority)
			{
				Room.RoomPlayers.FirstOrDefault(x => x.hasAuthority)?.UpdateDisplay();
				return;
			}

			for (int i = 0; i < playerNameTexts.Count; i++)
			{
				if (i >= Room.RoomPlayers.Count)
				{
					playerNameTexts[i].text = "---";
					playerReadyImages[i].GetComponent<Image>().color = Color.white;
				}
				else
				{
					playerNameTexts[i].text = Room.RoomPlayers[i].DisplayName;
					playerReadyImages[i].GetComponent<Image>().color = Room.RoomPlayers[i].IsReady
						? GreenColor
						: RedColor;
				}
			}

			if (IsReady)
			{
				buttonReady.GetComponentInChildren<TMP_Text>().text = $"<color={GreenColorHex}>ready</color>";
			}
			else
			{
				buttonReady.GetComponentInChildren<TMP_Text>().text = $"<color={RedColorHex}>Not ready</color>";
			}
		}

		public void HandleReadyToStart(bool readyToStart)
		{
			if (!IsHost)
			{
				return;
			}

			buttonStartGame.interactable = readyToStart;
		}

		[Command]
		public void CmdSetDisplayName(string displayName)
		{
			DisplayName = displayName;
		}

		[Command]
		public void CmdReadyUp()
		{
			IsReady = !IsReady;

			Room.NotifyHostOfReadyState();
		}

		[Command]
		public void CmdStartGame()
		{
			// only host can start the game:
			if (Room.RoomPlayers[0].connectionToClient != connectionToClient)
			{
				return;
			}

			Room.StartGame();
		}

		public void OnCancel()
		{
			// TODO: maybe user should confirm: really cancel?

			if (IsHost)
			{
				Room.StopHost();
			}
			else
			{
				Room.StopClient();
			}
		}

		public void OnReady()
		{
			CmdReadyUp();
		}

		public void OnStartGame()
		{
			CmdStartGame();
		}
	}
}
