using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HideAndSeek
{
	public class SlidingDoorController : MonoBehaviour
	{
		[SerializeField]
		private DoorTrigger Trigger;

		private Animator doorAnimator;
		private bool openDoor = false;

		void Start()
		{
			doorAnimator = GetComponent<Animator>();
		}

		void Update()
		{
			if (Trigger.DoorEvent == DoorEvent.PlayerDetected)
			{
				if (!openDoor)
				{
					Debug.Log("open door!");
					openDoor = true;
					doorAnimator.SetBool("openDoor", openDoor);
				}
			}
			else
			{
				if (openDoor)
				{
					Debug.Log("close door!");
					openDoor = false;
					doorAnimator.SetBool("openDoor", openDoor);
				}
			}
		}
	}
}
