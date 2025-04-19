// Clearly define the inventory slot data structure
[System.Serializable]
public class InventorySlotData
{
    public ItemData itemData;
    public int amount;
    
    public InventorySlotData()
    {
        itemData = null;
        amount = 0;
    }
}