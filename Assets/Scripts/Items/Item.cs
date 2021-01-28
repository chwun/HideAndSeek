namespace HideAndSeek.Items
{
	public class Item
	{
		public int Id { get; }

		public ItemType Type { get; }

		public Item(int id, ItemType type)
		{
			Id = id;
			Type = type;
		}
	}
}