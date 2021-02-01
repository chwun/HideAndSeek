using UnityEngine;

namespace HideAndSeek.Items
{
	public class Item
	{
		public int Id { get; }

		public ItemType Type { get; }

		public Sprite Sprite { get; }

		public Item(int id, ItemType type, Sprite sprite)
		{
			Id = id;
			Type = type;
			Sprite = sprite;
		}
	}
}