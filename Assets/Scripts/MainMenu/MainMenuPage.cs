using UnityEngine;

namespace HideAndSeek.MainMenu
{
	public class MainMenuPage : MonoBehaviour
	{
		[SerializeField] private GameObject playerNamePage;

		public void OnPlay()
		{
			gameObject.SetActive(false);
			playerNamePage.SetActive(true);
		}

		public void OnSettings()
		{
			// TODO!
		}

		public void OnQuit()
		{
			Application.Quit();
		}
	}
}
