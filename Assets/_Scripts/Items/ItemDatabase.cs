// Item Database
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Inventory/Item Database")]
public class ItemDatabase : ScriptableObject
{
    public List<ItemData> allItems = new List<ItemData>();
    
    private Dictionary<string, ItemData> itemDictionary;
    
    private void OnEnable()
    {
        BuildDictionary();
    }
    
    private void BuildDictionary()
    {
        itemDictionary = new Dictionary<string, ItemData>();
        
        foreach (var item in allItems)
        {
            if (!itemDictionary.ContainsKey(item.id))
            {
                itemDictionary.Add(item.id, item);
            }
            else
            {
                Debug.LogWarning($"Duplicate item ID found: {item.id}");
            }
        }
    }
    
    public ItemData GetItemById(string id)
    {
        if (itemDictionary == null)
        {
            BuildDictionary();
        }
        
        if (itemDictionary.TryGetValue(id, out ItemData item))
        {
            return item;
        }
        
        return null;
    }
}