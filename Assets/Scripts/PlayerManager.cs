using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HideAndSeek
{
	public class PlayerManager : MonoBehaviour
	{

		private GameManager gameManager;

		public Player Player { get; private set; }

		void Awake()
		{
			gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
		}

		public void SetPlayer(Player player)
		{
			Player = player;
		}

		void OnTriggerEnter(Collider other)
		{
			if (Player.IsSeeker)
			{
				if (other.gameObject.tag == "HidingPlayer")
				{
					gameManager.Catch(this.gameObject, other.gameObject);
				}
			}
		}
	}
}
