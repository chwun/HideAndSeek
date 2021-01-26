using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HideAndSeek
{
	public class DoorTrigger : MonoBehaviour
	{
		public bool PlayerDetected { get; private set; }

		private int numberOfDetectedPlayers = 0;

		public void OnTriggerEnter(Collider other)
		{
			if (other.tag.Contains("Player"))
			{
				numberOfDetectedPlayers++;
				PlayerDetected = true;
			}
		}

		public void OnTriggerExit(Collider other)
		{
			if (other.tag.Contains("Player"))
			{
				numberOfDetectedPlayers--;

				if (numberOfDetectedPlayers <= 0)
				{
					numberOfDetectedPlayers = 0;
					PlayerDetected = false;
				}
			}
		}
	}
}
