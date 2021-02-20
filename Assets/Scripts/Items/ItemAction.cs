using UnityEngine;

namespace HideAndSeek.Items
{
	public class ItemAction
	{

        public ItemAction(Player player, Item item)
        {
            this.player = player;
            this.item = item;
            this.type = item.Type;
        }

        private Player player;
		private Item item;
		private ItemType type;

        public void Use()
        {
            switch (type)
            {
                case ItemType.Speedboost:
                    Debug.Log("Item Speedboost triggered");
                    speedboost();
                    break;

                case ItemType.Invisible:
                    Debug.Log("Item Invisible triggered");
                    break;
            }
        }

        private void speedboost()
        {
            PlayerMovement playerMovement = player.PlayerObject.GetComponent<PlayerMovement>();
            Debug.Log("SpEEEEEEED");
        }
	}
}