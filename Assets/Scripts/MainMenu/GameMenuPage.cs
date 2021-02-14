using HideAndSeek.Network;
using UnityEngine;

namespace HideAndSeek.MainMenu
{
	public class GameMenuPage : MonoBehaviour
	{
		[SerializeField] CustomNetworkManager networkManager;

		[SerializeField] private GameObject mainMenuPage;
		[SerializeField] private GameObject joinGamePage;

		public void OnHost()
		{
			networkManager.StartHost();

			gameObject.SetActive(false);

			// TODO: show lobby ...
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
