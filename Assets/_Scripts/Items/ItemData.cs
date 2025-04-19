using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public string id;
    public string displayName;
    public ItemType itemType;
    public int cost;
    public Sprite icon;
    public string description;
    public bool isStackable = false;
    public int maxStackSize = 1;
    
    // Enum to differentiate between item types
    public enum ItemType
    {
        Weapon,
        Armor,
        Consumable
    }
}