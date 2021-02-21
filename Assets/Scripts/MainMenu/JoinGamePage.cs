using HideAndSeek.Network;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HideAndSeek.MainMenu
{
	public class JoinGamePage : MonoBehaviour
	{
		[SerializeField] CustomNetworkManager networkManager;

		[SerializeField] private GameObject gameMenuPage;

		[SerializeField] private TMP_InputField inputFieldIpAddress;
		[SerializeField] private Button buttonOk;

		private void Start()
		{
			InitInputField();
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
			{
				if (buttonOk.interactable)
				{
					OnOk();
				}
			}
			else if (Input.GetKeyDown(KeyCode.Escape))
			{
				OnCancel();
			}
		}

		private void OnEnable()
		{
			CustomNetworkManager.ClientConnected += OnClientConnected;
			CustomNetworkManager.ClientDisconnected += OnClientDisconnected;
		}

		private void OnDisable()
		{
			CustomNetworkManager.ClientConnected -= OnClientConnected;
			CustomNetworkManager.ClientDisconnected -= OnClientDisconnected;
		}

		private void InitInputField()
		{
			if (!PlayerPrefs.HasKey(Constants.PlayerPrefsLastHostIpKey))
			{
				return;
			}

			string storedHostIp = PlayerPrefs.GetString(Constants.PlayerPrefsLastHostIpKey);
			inputFieldIpAddress.text = storedHostIp;

			RefreshOkButtonState(storedHostIp);
		}

		private void RefreshOkButtonState(string hostIp)
		{
			// TODO: ip validation?

			buttonOk.interactable = !string.IsNullOrWhiteSpace(hostIp);
		}

		private void SaveHostIp()
		{
			PlayerPrefs.SetString(Constants.PlayerPrefsLastHostIpKey, inputFieldIpAddress.text);
		}

		private void OnClientConnected()
		{
			buttonOk.interactable = true;
			gameObject.SetActive(false);
		}

		private void OnClientDisconnected()
		{
			buttonOk.interactable = true;
		}

		public void OnPlayerNameInputChanged(string hostIp)
		{
			RefreshOkButtonState(hostIp);
		}

		public void OnOk()
		{
			SaveHostIp();

			string hostIp = inputFieldIpAddress.text;

			networkManager.networkAddress = hostIp;
			networkManager.StartClient();

			buttonOk.interactable = false;
		}

		public void OnCancel()
		{
			gameObject.SetActive(false);
			gameMenuPage.SetActive(true);
		}
	}
}
