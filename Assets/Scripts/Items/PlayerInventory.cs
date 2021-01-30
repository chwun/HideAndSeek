namespace HideAndSeek.Items
{
	public class PlayerInventory
	{
		private const int InventorySize = 3;

		public Item[] Items { get; private set; }

		public int SelectedItemSlot { get; private set; }

		public PlayerInventory()
		{
			Items = new Item[InventorySize];
			SelectedItemSlot = 0;
		}

		public void AddItem(Item item)
		{
			int nextFreeItemSlot = GetNextFreeItemSlot();

			if (nextFreeItemSlot > -1)
			{
				// if free slot is found: add item to this slot
				Items[nextFreeItemSlot] = item;
			}
			else
			{
				// if no free slot: replace currently select item slot
				Items[SelectedItemSlot] = item;
			}
		}

		public void SelectNextItemSlot()
		{
			SelectedItemSlot = (SelectedItemSlot + 1) % InventorySize;
		}

		public void SelectPreviousItemSlot()
		{
			SelectedItemSlot = (SelectedItemSlot + InventorySize - 1) % InventorySize;
		}

		private int GetNextFreeItemSlot()
		{
			int nextFreeItemSlot = -1;

			for (int i = SelectedItemSlot; i < (SelectedItemSlot + InventorySize); i++)
			{
				int itemSlot = i % InventorySize;

				if (Items[itemSlot] == null)
				{
					nextFreeItemSlot = itemSlot;
					break;
				}
			}

			return nextFreeItemSlot;
		}
	}
}