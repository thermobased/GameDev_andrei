using UnityEngine;
using UnityEngine.Serialization;

public enum ProjectileType
{
    Bomb,
    Nuke,
    Dynamite
}

[System.Serializable]
public class ProjectileData
{
    public ProjectileType type;
    public GameObject prefab;
    public Sprite icon;
    public int price;
    public int initialQuantity;
    public bool isUnlimited;
    public string displayName;
}