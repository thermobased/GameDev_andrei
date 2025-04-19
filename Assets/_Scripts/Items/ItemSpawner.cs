using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject collectiblePrefab;
    [SerializeField] private string[] itemIdsToSpawn; // Store IDs instead of direct references
    [SerializeField] private ItemDatabase itemDatabase; // Reference to your database
    [SerializeField] private BoxCollider2D spawnArea;
    

    public GameObject SpawnItem(string itemId, Vector2 position)
    {
        // Get the item from the database by ID
        ItemData data = itemDatabase.GetItemById(itemId);
        Debug.Log($"data -- > {data} ...");
        
        if (data == null || collectiblePrefab == null)
        {
            Debug.LogWarning($"Failed to spawn item. ItemId: {itemId}, Database: {itemDatabase}, Prefab: {collectiblePrefab}");
            return null;
        }
        
        GameObject item = Instantiate(collectiblePrefab, position, Quaternion.identity);
        CollectibleItem collectible = item.GetComponent<CollectibleItem>();
        SpriteRenderer spriteRenderer = item.GetComponent<SpriteRenderer>();

        if (collectible != null)
        {
            collectible.itemData = data;
            collectible.amount = 1;
        }

        if (spriteRenderer != null && data.icon != null)
        {
            spriteRenderer.sprite = data.icon;
        }
        return item;
    }

    private Vector2 GetRandomPositionInArea()
    {
        Vector2 center = spawnArea.bounds.center;
        Vector2 size = spawnArea.bounds.size;
        
        float x = Random.Range(-size.x / 2, size.x / 2);
        float y = Random.Range(-size.y / 2, size.y / 2);
        
        return center + new Vector2(x, y);
    }

    public void SpawnRandomItemInArea()
    {
        if (itemIdsToSpawn == null) {
            Debug.LogWarning("Cannot spawn items: Missing data");
            return;
        }
        if(itemIdsToSpawn.Length == 0) {
            Debug.LogWarning("Cannot spawn items: Missing data");
            return;
        }
        if(spawnArea == null) {
            Debug.LogWarning("Cannot spawn items: Missing area");
            return;
        }
        if (itemDatabase == null) {
            Debug.LogWarning("Cannot spawn items: Missing data");
            return;
        }
        
        string randomItemId = itemIdsToSpawn[Random.Range(0, itemIdsToSpawn.Length)];
        Vector2 randomPosition = GetRandomPositionInArea();
        SpawnItem(randomItemId, randomPosition);
    }

}