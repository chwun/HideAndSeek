using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HideAndSeek
{
	public class MainMenuManager : MonoBehaviour
	{
		public void OnPlay()
		{
			SceneManager.LoadScene("Level01");
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
