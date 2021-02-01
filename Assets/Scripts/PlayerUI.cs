using System;
using System.Collections.Generic;
using HideAndSeek.Items;
using UnityEngine;
using UnityEngine.UI;

namespace HideAndSeek
{
	public class PlayerUI : MonoBehaviour
	{
		[Header("Inventory")]
		public GameObject InventoryUI;
		public GameObject ItemSlot1;
		public GameObject ItemSlot2;
		public GameObject ItemSlot3;

		private List<Image> itemSlotBackgrounds;
		private List<Image> itemSlotIcons;

		private Player player;

		private void Awake()
		{
			itemSlotBackgrounds = new List<Image>()
			{
				ItemSlot1.transform.Find("Background").GetComponent<Image>(),
				ItemSlot2.transform.Find("Background").GetComponent<Image>(),
				ItemSlot3.transform.Find("Background").GetComponent<Image>()
			};

			itemSlotIcons = new List<Image>()
			{
				ItemSlot1.transform.Find("Icon").GetComponent<Image>(),
				ItemSlot2.transform.Find("Icon").GetComponent<Image>(),
				ItemSlot3.transform.Find("Icon").GetComponent<Image>()
			};
		}

		private void Update()
		{
			HandleScrollWheel();
			HandleNumberKeys();
			HandleMouseClick();
		}

		public void SetPlayer(Player player)
		{
			this.player = player;

			this.player.Inventory.InventoryItemsChanged += OnInventoryItemsChanged;
			this.player.Inventory.InventorySelectionChanged += OnInventorySelectionChanged;

			RefreshInventoryItems();
			RefreshInventorySelection();
		}

		private void HandleScrollWheel()
		{
			float mouseScrollDelta = Input.mouseScrollDelta.y;

			if (mouseScrollDelta > 0)
			{
				player.Inventory.SelectPreviousItemSlot();
			}
			else if (mouseScrollDelta < 0)
			{
				player.Inventory.SelectNextItemSlot();
			}
		}

		private void HandleNumberKeys()
		{
			if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
			{
				player.Inventory.SelectSpecificItemSlot(0);
			}
			else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
			{
				player.Inventory.SelectSpecificItemSlot(1);
			}
			else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
			{
				player.Inventory.SelectSpecificItemSlot(2);
			}
		}

		private void HandleMouseClick()
		{
			if (Input.GetMouseButtonDown(0))
			{
				player.TriggerSelectedInventoryItem();
			}
		}

		private void OnInventoryItemsChanged(object sender, EventArgs e)
		{
			RefreshInventoryItems();
		}

		private void OnInventorySelectionChanged(object sender, EventArgs e)
		{
			RefreshInventorySelection();
		}

		private void RefreshInventoryItems()
		{
			for (int i = 0; i < player.Inventory.Items.Length; i++)
			{
				Item item = player.Inventory.Items[i];

				if (item == null)
				{
					itemSlotIcons[i].sprite = null;
				}
				else
				{
					itemSlotIcons[i].sprite = item.Sprite;
				}
			}
		}

		private void RefreshInventorySelection()
		{
			for (int i = 0; i < player.Inventory.Items.Length; i++)
			{
				Item item = player.Inventory.Items[i];

				if (player.Inventory.SelectedItemSlot == i)
				{
					itemSlotBackgrounds[i].color = Color.black;
				}
				else
				{
					itemSlotBackgrounds[i].color = Color.white;
				}
			}
		}
	}
}
