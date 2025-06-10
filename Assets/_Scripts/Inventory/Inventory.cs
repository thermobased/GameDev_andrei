using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class Inventory : MonoBehaviour
{
    private InventoryData _data;
    [SerializeField] private List<ProjectileData> allProjectileData;
    
    public static Inventory Instance { get; private set; }
    public static event Action<int> OnCoinsChanged;
    public static event Action<ProjectileType, int> OnProjectileQuantityChanged;
    public static event Action<ProjectileType> OnCurrentProjectileChanged;
    
    public int Coins => _data.coins;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeData()
    {
        _data = SaveManager.LoadData();
        
        foreach (var projectileData in allProjectileData)
        {
            if (!_data.ProjectileAmounts.ContainsKey(projectileData.type))
                _data.ProjectileAmounts[projectileData.type] = projectileData.initialQuantity;
        }
        SaveManager.SaveData(_data);
    }

    private void Start()
    {
        OnCoinsChanged?.Invoke(Coins);
        
        foreach (var kvp in _data.ProjectileAmounts)
        {
            OnProjectileQuantityChanged?.Invoke(kvp.Key, kvp.Value);
        }
        
        OnCurrentProjectileChanged?.Invoke(_data.currentProjectileType);
    }

    public bool TryBuyItem(ProjectileData item)
    {
        if (_data.coins < item.price) return false;

        _data.coins -= item.price;
        ChangeProjectileQuantity(item.type, item.soldAmount);
        
        OnCoinsChanged?.Invoke(Coins);
        SaveManager.SaveData(_data);
        return true;
    }

    private void ChangeProjectileQuantity(ProjectileType type, int amount)
    {
        if (!_data.ProjectileAmounts.ContainsKey(type))
            _data.ProjectileAmounts[type] = 0;
            
        _data.ProjectileAmounts[type] += amount;
        OnProjectileQuantityChanged?.Invoke(type, _data.ProjectileAmounts[type]);
        SaveManager.SaveData(_data);
    }

    public int GetProjectileQuantity(ProjectileType type)
    {
        return _data.ProjectileAmounts.TryGetValue(type, out int quantity) ? quantity : 0;
    }

    public ProjectileType GetCurrentProjectileType()
    {
        return _data.currentProjectileType;
    }

    public void SetCurrentProjectileType(ProjectileType type)
    {
        _data.currentProjectileType = type;
        OnCurrentProjectileChanged?.Invoke(type);
        SaveManager.SaveData(_data);
    }

    public bool CanUseProjectile(ProjectileType type)
    {
        var projectileData = GetProjectileData(type);
        if (projectileData != null && projectileData.isUnlimited)
            return true;
            
        return GetProjectileQuantity(type) > 0;
    }

    public bool UseProjectile(ProjectileType type)
    {
        var projectileData = GetProjectileData(type);
        if (projectileData != null && projectileData.isUnlimited)
            return true;
            
        if (GetProjectileQuantity(type) > 0)
        {
            ChangeProjectileQuantity(type, -1);
            return true;
        }
        
        return false;
    }

    public ProjectileData GetProjectileData(ProjectileType type)
    {
        return allProjectileData?.FirstOrDefault(data => data.type == type);
    }

    public List<ProjectileData> GetAllProjectileData()
    {
        return allProjectileData;
    }

    private void OnEnable()
    {
        Enemy.onEnemyDeath += AddCoins;
    }
    
    private void OnDisable()
    {
        Enemy.onEnemyDeath -= AddCoins;
    }
    
    public void AddCoins(Enemy enemy)
    {
        int reward = enemy.GetEnemyData().currencyReward;
        _data.coins += reward;
        OnCoinsChanged?.Invoke(Coins);
        SaveManager.SaveData(_data);
    }

    private void OnApplicationQuit()
    {
        SaveManager.SaveData(_data);
    }
}