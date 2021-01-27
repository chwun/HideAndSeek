using UnityEngine;

namespace HideAndSeek.Interactions.SlidingDoor
{
	public class SlidingDoorController : MonoBehaviour
	{
		private Animator doorAnimator;
		private DoorTrigger doorTrigger;

		void Start()
		{
			doorAnimator = GetComponent<Animator>();
			doorTrigger = GetComponentInChildren<DoorTrigger>();
		}

		void Update()
		{
			if (doorTrigger.PlayerDetected)
			{
				doorAnimator.SetTrigger("OpenDoor");
			}
			else
			{
				doorAnimator.SetTrigger("CloseDoor");
			}
		}
	}
}
