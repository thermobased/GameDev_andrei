using UnityEngine;
using UnityEngine.Serialization;

public enum ProjectileType
{
    Bomb,
    Nuke
}

[System.Serializable]
public class ProjectileData
{
    public ProjectileType type;
    public GameObject prefab;
    public Sprite icon;
    public int price; // Cost to purchase
    public int initialQuantity; // Starting quantity
    public bool isUnlimited; // For basic projectiles
    public string displayName; // User-friendly name
}