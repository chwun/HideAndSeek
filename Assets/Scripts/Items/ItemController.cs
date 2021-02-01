using UnityEngine;

namespace HideAndSeek.Items
{
	public class ItemController : MonoBehaviour
	{
		private Item item;

		private ItemTrigger itemTrigger;

		public void Start()
		{
			itemTrigger = GetComponentInChildren<ItemTrigger>();
		}

		public void Update()
		{
			if (itemTrigger.Triggered)
			{
				itemTrigger.TriggeredPlayer.Inventory.AddItem(item);
				ItemSpawner.Instance.RemoveItem(item.Id);
			}
		}

		public void SetItem(Item item)
		{
			this.item = item;
		}
	}
}
