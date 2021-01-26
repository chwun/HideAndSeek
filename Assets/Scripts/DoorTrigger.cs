using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HideAndSeek
{
	public class DoorTrigger : MonoBehaviour
	{
		public DoorEvent DoorEvent { get; private set; }

		public void OnTriggerEnter(Collider other)
		{
			if (other.tag.Contains("Player"))
			{
				DoorEvent = DoorEvent.PlayerDetected;
			}
		}

		public void OnTriggerExit(Collider other)
		{
			if (other.tag.Contains("Player"))
			{
				DoorEvent = DoorEvent.None;
			}
		}
	}

	public enum DoorEvent
	{
		None,
		PlayerDetected
	}
}
