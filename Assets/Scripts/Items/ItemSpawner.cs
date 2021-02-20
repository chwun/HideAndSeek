using System.Collections;
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
				SpawnItemOnPosition(spawnPosition.position, spawnPosition.rotation);
			}
		}

		private void SpawnItemOnPosition(Vector3 spawnPosition, Quaternion spawnRotation)
		{
			SceneItemTemplate sceneItemTemplate = GetNextRandomSceneItemTemplate();
			Item newItem = new Item(nextItemId++, sceneItemTemplate.Type, sceneItemTemplate.ItemSprite);

			GameObject itemObject = Instantiate(sceneItemTemplate.ItemPrefab, spawnPosition, spawnRotation, ItemsContainer.transform);
			itemObject.name = $"Item_{newItem.Id}";
			itemObject.GetComponentInChildren<ItemTrigger>().SetItem(newItem);

			SpawnedItems[newItem.Id] = itemObject;
		}

		private SceneItemTemplate GetNextRandomSceneItemTemplate()
		{
			int randomListPosition = Random.Range(0, sceneItemTemplatesCount);
			return SceneItemTemplates[randomListPosition];
		}

		private IEnumerator RespawnItemOnPosition(Vector3 spawnPosition, Quaternion spawnRotation, float delay)
		{
			yield return new WaitForSeconds(delay);

			SpawnItemOnPosition(spawnPosition, spawnRotation);
		}

		public void RemoveItem(int itemId)
		{
			GameObject itemObject = SpawnedItems[itemId];
			Vector3 itemPosition = itemObject.transform.position;
			Quaternion itemRotation = itemObject.transform.rotation;

			Destroy(itemObject);
			// SpawnedItems.Remove(itemId);
			StartCoroutine(RespawnItemOnPosition(itemPosition, itemRotation, 2f));
		}
	}
}
