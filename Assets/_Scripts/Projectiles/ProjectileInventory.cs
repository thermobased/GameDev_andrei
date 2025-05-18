using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

public class ProjectileInventory : MonoBehaviour
{
    private static ProjectileInventory _instance;
    public static ProjectileInventory Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("ProjectileInventory instance not found!");
            }
            return _instance;
        }
    }

    public event Action<ProjectileType, int> OnProjectileQuantityChanged;
    
    [FormerlySerializedAs("defaultProjectileType")] [SerializeField] private ProjectileType defaultProjectileData = ProjectileType.Bomb;
    
    // Dictionary to track quantities of each projectile type
    private Dictionary<ProjectileType, int> projectileQuantities = new Dictionary<ProjectileType, int>();
    
    // Reference to the projectile spawner to access projectile data
    [SerializeField] private ProjectileSpawner projectileSpawner;

    private ProjectileType currentProjectileType = ProjectileType.Bomb;

    public ProjectileType GetProjectileType()
    {
        return currentProjectileType;
    }

    public ProjectileType GetCurrentProjectileType()
    {
        return currentProjectileType;
    }

    public void SetCurrentProjectileType(ProjectileType type)
    {
        currentProjectileType = type;
    }
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        _instance = this;
    }

    private void Start()
    {
        if (projectileSpawner == null)
        {
            projectileSpawner = FindObjectOfType<ProjectileSpawner>();
            if (projectileSpawner == null)
            {
                Debug.LogError("ProjectileSpawner reference not found!");
                return;
            }
        }
        
        // Initialize quantities from projectile data
        InitializeQuantities();
    }

    private void InitializeQuantities()
    {
        foreach (var data in projectileSpawner.GetProjectileDataList())
        {
            if (data.isUnlimited)
            {
                // For unlimited projectiles, we use -1 to indicate unlimited
                projectileQuantities[data.type] = -1;
            }
            else
            {
                projectileQuantities[data.type] = data.initialQuantity;
            }
        }
    }

    public bool UseProjectile(ProjectileType data)
    {
        if (!projectileQuantities.ContainsKey(data))
        {
            Debug.LogWarning($"Projectile type {data} not found in inventory!");
            return false;
        }
        
        int quantity = projectileQuantities[data];
        
        // Check if unlimited (-1) or has available quantity
        if (quantity == -1 || quantity > 0)
        {
            // Only decrement if not unlimited
            if (quantity > 0)
            {
                projectileQuantities[data]--;
                OnProjectileQuantityChanged?.Invoke(data, projectileQuantities[data]);
            }
            return true;
        }
        
        return false;
    }

    public void AddProjectile(ProjectileType data, int amount)
    {
        if (!projectileQuantities.ContainsKey(data))
        {
            Debug.LogWarning($"Projectile type {data} not found in inventory!");
            return;
        }
        
        int currentQuantity = projectileQuantities[data];
        
        // Only add if not unlimited
        if (currentQuantity != -1)
        {
            projectileQuantities[data] += amount;
            OnProjectileQuantityChanged?.Invoke(data, projectileQuantities[data]);
        }
    }

    public int GetProjectileQuantity(ProjectileType data)
    {
        if (projectileQuantities.TryGetValue(data, out int quantity))
        {
            return quantity;
        }
        return 0;
    }
    
    public bool IsProjectileUnlimited(ProjectileType data)
    {
        return GetProjectileQuantity(data) == -1;
    }
    
    public List<ProjectileType> GetPurchasableProjectileTypes()
    {
        List<ProjectileType> purchasable = new List<ProjectileType>();
        
        foreach (var data in projectileSpawner.GetProjectileDataList())
        {
            if (!data.isUnlimited && data.price > 0)
            {
                purchasable.Add(data.type);
            }
        }
        
        return purchasable;
    }
}