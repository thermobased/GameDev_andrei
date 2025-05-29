using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;
using System.Linq;

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
    public int soldAmount;
    public int price;
    public int initialQuantity;
    public bool isUnlimited;
    public string displayName;
}