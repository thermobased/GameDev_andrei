using UnityEngine;

public enum ProjectileType
{
    Bomb,
    Nuke,
    
}

[System.Serializable]
public class ProjectileData
{
    public ProjectileType type;
    public GameObject prefab;
    public Sprite icon;
}