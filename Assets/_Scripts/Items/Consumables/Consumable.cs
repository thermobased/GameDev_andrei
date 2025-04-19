// Weapon Item that inherits from base Item
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable", menuName = "Inventory/Consumable")]
public class ConsumableItemData : ItemData
{
    public float protection;
    public float durability;

    private void OnEnable()
    {
        itemType = ItemType.Consumable;
        isStackable = true;
        maxStackSize = 100;
    }
}