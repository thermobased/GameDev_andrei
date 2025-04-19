using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private int inventorySize = 24;
    [SerializeField] private ItemDatabase itemDatabase;
    
    private InventorySlotData[] inventorySlots;
    
    public Action OnInventoryChanged;
    
    private void Awake()
    {
        // Initialize inventory slots
        inventorySlots = new InventorySlotData[inventorySize];
        for (int i = 0; i < inventorySize; i++)
        {
            inventorySlots[i] = new InventorySlotData();
        }
    }
    
    private void OnEnable()
    {
        CollectibleItem.onItemPickedUp += HandleItemPickedUp;
    }
    
    private void OnDisable()
    {
        CollectibleItem.onItemPickedUp -= HandleItemPickedUp;
    }
    
    private void HandleItemPickedUp(ItemData item, int amount)
    {
        AddItem(item, amount);
    }
    
    // The rest of your PlayerInventory class remains mostly the same,
    // but you can add methods that use the ItemDatabase:
    
    
    public bool AddItem(ItemData item, int amount)
    {
        // First try to stack with existing same items if stackable
        if (item.isStackable)
        {
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                if (inventorySlots[i].itemData != null && 
                    inventorySlots[i].itemData.id == item.id && 
                    inventorySlots[i].amount < item.maxStackSize)
                {
                    int spaceInSlot = item.maxStackSize - inventorySlots[i].amount;
                    int amountToAdd = Mathf.Min(amount, spaceInSlot);
                    
                    inventorySlots[i].amount += amountToAdd;
                    amount -= amountToAdd;
                    
                    if (amount <= 0)
                    {
                        OnInventoryChanged?.Invoke();
                        return true;
                    }
                }
            }
        }
        
        // If we still have items to add, find empty slots
        if (amount > 0)
        {
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                if (inventorySlots[i].itemData == null)
                {
                    int amountToAdd = Mathf.Min(amount, item.maxStackSize);
                    inventorySlots[i].itemData = item;
                    inventorySlots[i].amount = amountToAdd;
                    amount -= amountToAdd;
                    
                    if (amount <= 0)
                    {
                        OnInventoryChanged?.Invoke();
                        return true;
                    }
                }
            }
        }
        
        // If we couldn't add all items
        if (amount > 0)
        {
            Debug.Log("Inventory full! Dropped " + amount + " " + item.displayName);
            OnInventoryChanged?.Invoke();
            return false;
        }
        
        OnInventoryChanged?.Invoke();
        return true;
    }
    
    public InventorySlotData[] GetAllSlots()
    {
        return inventorySlots;
    }
    
    
}