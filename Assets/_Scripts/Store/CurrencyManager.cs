using UnityEngine;
using System;

public class CurrencyManager : MonoBehaviour
{
    private static CurrencyManager _instance;
    public static CurrencyManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("CurrencyManager instance not found!");
            }
            return _instance;
        }
    }

    public event Action<int> OnCurrencyChanged;
    
    [SerializeField] private int startingCurrency = 0;
    private int currentCurrency;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        _instance = this;
        currentCurrency = startingCurrency;
    }

    private void Start()
    {
        // Subscribe to enemy death events
        Enemy.onEnemyDeath += OnEnemyDefeated;
    }

    private void OnDestroy()
    {
        // Unsubscribe when destroyed
        Enemy.onEnemyDeath -= OnEnemyDefeated;
        
        if (_instance == this)
        {
            _instance = null;
        }
    }

    private void OnEnemyDefeated(Enemy enemy)
    {
        // Get the enemy data to access currency reward
        EnemyData enemyData = enemy.GetEnemyData();
        if (enemyData != null)
        {
            AddCurrency(enemyData.currencyReward);
        }
    }

    public void AddCurrency(int amount)
    {
        if (amount > 0)
        {
            currentCurrency += amount;
            OnCurrencyChanged?.Invoke(currentCurrency);
        }
    }

    public bool SpendCurrency(int amount)
    {
        if (amount <= 0)
            return true;
            
        if (currentCurrency >= amount)
        {
            currentCurrency -= amount;
            OnCurrencyChanged?.Invoke(currentCurrency);
            return true;
        }
        
        return false; // Not enough currency
    }

    public int GetCurrentCurrency()
    {
        return currentCurrency;
    }
}