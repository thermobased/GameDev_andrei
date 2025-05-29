using System;
using System.Collections.Generic;

[System.Serializable]
public class InventoryData
{
    public int coins = 100;
    public Dictionary<ProjectileType, int> ProjectileAmounts = new Dictionary<ProjectileType, int>();
    public ProjectileType currentProjectileType = ProjectileType.Bomb;
}