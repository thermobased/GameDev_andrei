// Weapon Item that inherits from base Item
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory/Weapon")]
public class WeaponItemData : ItemData
{
    public float damage;
    public float staminaCost;
    public float durability;

    private void OnEnable()
    {
        itemType = ItemType.Weapon;
        isStackable = false;
    }
}