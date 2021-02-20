using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HideAndSeek.Items
{
	public class ItemTrigger : MonoBehaviour
	{

		public Player TriggeredPlayer { get; private set; }

		private bool isUsed = false;
		private Item item;

		public void OnTriggerEnter(Collider other)
		{
			if (!isUsed){
				pickItem(other);
			}
		}

		public void OnTriggerStay(Collider other)
		{
			if (!isUsed){
				pickItem(other);
			}
		}

		private void pickItem(Collider other)
		{
			if (other.tag.Contains("Player"))
			{
				TriggeredPlayer = other.GetComponent<PlayerManager>()?.Player;

				if (TriggeredPlayer != null)
				{
					if (TriggeredPlayer.Inventory.AddItem(item))
					{
						ItemSpawner.Instance.RemoveItem(item.Id);
						isUsed = true;
					}
				}
			}
		}
		public void SetItem(Item item)
		{
			this.item = item;
		}
	}
}
