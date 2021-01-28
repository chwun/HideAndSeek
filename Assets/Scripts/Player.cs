using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HideAndSeek.Items;

namespace HideAndSeek
{
	public class Player
	{
		public GameObject PlayerObject { get; private set; }

		public string Name { get; set; }

		public int Points { get; set; }

		public bool IsAlive { get; set; }

		public bool IsSeeker {get; private set;}

		public PlayerInventory Inventory { get; private set; }

		public Player(GameObject playerObject, string name, bool isSeeker)
		{
			PlayerObject = playerObject;
			Name = name;
			Points = 0;
			IsAlive = true;
			Inventory = new PlayerInventory();
		}

	}
}
