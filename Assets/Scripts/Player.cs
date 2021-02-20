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

		public bool IsSeeker { get; private set; }

		public PlayerInventory Inventory { get; private set; }

		public Player(GameObject playerObject, string name, bool isSeeker)
		{
			PlayerObject = playerObject;
			Name = name;
			Points = 0;
			IsAlive = true;
			Inventory = new PlayerInventory();
		}

		public void TriggerSelectedInventoryItem()
		{
			Item item = Inventory.GetAndRemoveSelectedItem();

			if (item != null)
			{
				// TODO: Items auslösen, evtl. Aktionen in PlayerManager usw.
				switch (item.Type)
				{
					case ItemType.Type1:
						Debug.Log("Item of type Type1 triggered");
						break;

					case ItemType.Type2:
						Debug.Log("Item of type Type2 triggered");
						break;
				}
			}
		}
	}
}