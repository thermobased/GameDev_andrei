using UnityEngine;

[CreateAssetMenu(fileName = "New weapon", menuName = "New item/Weapon")]
public class WeaponData : ScriptableObject
{
    public string id;
    public string displayName;
    public float damage;
    public float staminaCost;
    public float durability;
    public int cost;
    public Sprite sprite;
}
