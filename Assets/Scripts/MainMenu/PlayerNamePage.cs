using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HideAndSeek.MainMenu
{
	public class PlayerNamePage : MonoBehaviour
	{
		[SerializeField] private GameObject mainMenuPage;
		[SerializeField] private GameObject gameMenuPage;

		[SerializeField] private TMP_InputField inputFieldPlayerName;
		[SerializeField] private Button buttonOk;

		private const string PlayerPrefsPlayerNameKey = "PlayerName";

		private void Start()
		{
			InitInputField();
		}

		private void InitInputField()
		{
			if (!PlayerPrefs.HasKey(PlayerPrefsPlayerNameKey))
			{
				return;
			}

			string storedPlayerName = PlayerPrefs.GetString(PlayerPrefsPlayerNameKey);
			inputFieldPlayerName.text = storedPlayerName;

			RefreshOkButtonState(storedPlayerName);
		}

		private void RefreshOkButtonState(string playerName)
		{
			buttonOk.interactable = !string.IsNullOrWhiteSpace(name);
		}

		private void SavePlayerName()
		{
			PlayerPrefs.SetString(PlayerPrefsPlayerNameKey, inputFieldPlayerName.text);
		}

		public void OnPlayerNameInputChanged(string playerName)
		{
			RefreshOkButtonState(playerName);
		}

		public void OnOk()
		{
			SavePlayerName();

			gameObject.SetActive(false);
			gameMenuPage.SetActive(true);
		}

		public void OnCancel()
		{
			gameObject.SetActive(false);
			mainMenuPage.SetActive(true);
		}
	}
}
