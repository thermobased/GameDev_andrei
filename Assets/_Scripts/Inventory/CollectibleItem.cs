// Collectible item that can be picked up in the world
using System;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public ItemData itemData;
    public int amount = 1;
    
    public static Action<ItemData, int> onItemPickedUp;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            onItemPickedUp?.Invoke(itemData, amount);
            Destroy(gameObject);
        }
    }
}