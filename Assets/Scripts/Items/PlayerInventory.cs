using System;

namespace HideAndSeek.Items
{
	public class PlayerInventory
	{
		private const int InventorySize = 3;

		public Item[] Items { get; private set; }

		public int SelectedItemSlot { get; private set; }

		public event EventHandler InventoryItemsChanged;
		public event EventHandler InventorySelectionChanged;

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

			InventoryItemsChanged?.Invoke(this, EventArgs.Empty);
		}

		public void SelectNextItemSlot()
		{
			SelectedItemSlot = (SelectedItemSlot + 1) % InventorySize;
			InventorySelectionChanged?.Invoke(this, EventArgs.Empty);
		}

		public void SelectPreviousItemSlot()
		{
			SelectedItemSlot = (SelectedItemSlot + InventorySize - 1) % InventorySize;
			InventorySelectionChanged?.Invoke(this, EventArgs.Empty);
		}

		public void SelectSpecificItemSlot(int slotIndex)
		{
			if (slotIndex < InventorySize)
			{
				SelectedItemSlot = slotIndex;
				InventorySelectionChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public Item GetAndRemoveSelectedItem()
		{
			Item item = Items[SelectedItemSlot];
			Items[SelectedItemSlot] = null;
			InventoryItemsChanged?.Invoke(this, EventArgs.Empty);

			return item;
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