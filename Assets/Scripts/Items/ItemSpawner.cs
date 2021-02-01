using System.Collections.Generic;
using UnityEngine;

namespace HideAndSeek.Items
{
	public class ItemSpawner : MonoBehaviour
	{
		public static ItemSpawner Instance { get; private set; }

		public List<Transform> SpawnPositions;

		public GameObject ItemsContainer;

		public List<SceneItemTemplate> SceneItemTemplates;
		private int sceneItemTemplatesCount;
		private int nextItemId = 1;

		private Dictionary<int, GameObject> SpawnedItems = new Dictionary<int, GameObject>();

		public void Awake()
		{
			Instance = this;
		}

		public void Start()
		{
			sceneItemTemplatesCount = SceneItemTemplates.Count;

			SpawnItemsOnAllPositions();
		}

		private void SpawnItemsOnAllPositions()
		{
			foreach (Transform spawnPosition in SpawnPositions)
			{
				SpawnItemOnPosition(spawnPosition);
			}
		}

		private void SpawnItemOnPosition(Transform spawnPosition)
		{
			SceneItemTemplate sceneItemTemplate = GetNextRandomSceneItemTemplate();
			Item newItem = new Item(nextItemId++, sceneItemTemplate.Type, sceneItemTemplate.ItemSprite);

			GameObject itemObject = Instantiate(sceneItemTemplate.ItemPrefab, spawnPosition.position, spawnPosition.rotation, ItemsContainer.transform);
			itemObject.name = $"Item_{newItem.Id}";
			itemObject.GetComponent<ItemController>().SetItem(newItem);

			SpawnedItems[newItem.Id] = itemObject;
		}

		private SceneItemTemplate GetNextRandomSceneItemTemplate()
		{
			int randomListPosition = Random.Range(0, sceneItemTemplatesCount);
			return SceneItemTemplates[randomListPosition];
		}

		public void RemoveItem(int itemId)
		{
			GameObject itemObject = SpawnedItems[itemId];

			Destroy(itemObject);
			SpawnedItems.Remove(itemId);

			// TODO: evtl. nach gewisser Zeit neues Item spawnen?
		}
	}
}
