// Weapon Item that inherits from base Item
using UnityEngine;

[CreateAssetMenu(fileName = "New Armor", menuName = "Inventory/Armor")]
public class ArmorItemData : ItemData
{
    public float protection;
    public float durability;

    private void OnEnable()
    {
        itemType = ItemType.Armor;
        isStackable = false;
    }
}