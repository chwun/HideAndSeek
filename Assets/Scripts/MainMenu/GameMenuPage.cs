using HideAndSeek.Network;
using UnityEngine;

namespace HideAndSeek.MainMenu
{
	public class GameMenuPage : MonoBehaviour
	{
		[SerializeField] CustomNetworkManager networkManager;

		[SerializeField] private GameObject mainMenuPage;
		[SerializeField] private GameObject joinGamePage;

		private void OnEnable()
		{
			CustomNetworkManager.ReturnToGameMenu += OnReturnToGameMenu;
		}

		private void OnDestroy()
		{
			CustomNetworkManager.ReturnToGameMenu -= OnReturnToGameMenu;
		}

		private void OnReturnToGameMenu()
		{
			// show game menu page when player gets disconnected
			gameObject.SetActive(true);
		}

		public void OnHost()
		{
			networkManager.StartHost();

			gameObject.SetActive(false);
		}

		public void OnJoin()
		{
			gameObject.SetActive(false);
			joinGamePage.SetActive(true);
		}

		public void OnBack()
		{
			gameObject.SetActive(false);
			mainMenuPage.SetActive(true);
		}
	}
}
