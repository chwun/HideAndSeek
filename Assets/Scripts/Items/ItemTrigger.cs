using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HideAndSeek
{
	public class ItemTrigger : MonoBehaviour
	{
		public bool Triggered { get; private set; }

		public Player TriggeredPlayer { get; private set; }

		private bool isUsed = false;

		public void OnTriggerEnter(Collider other)
		{
			if (!isUsed)
			{
				if (other.tag.Contains("Player"))
				{
					TriggeredPlayer = other.GetComponent<PlayerManager>()?.Player;

					if (TriggeredPlayer != null)
					{
						Triggered = true;
						isUsed = true;
					}
					else
					{
						Triggered = false;
					}
				}
			}
		}
	}
}
