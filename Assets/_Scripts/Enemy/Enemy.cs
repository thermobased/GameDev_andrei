using UnityEngine;
using System;

public abstract class Enemy : MonoBehaviour
{
    public static event Action<Enemy> onEnemyDeath;
    [SerializeField] protected EnemyData enemyData;
    private float currentHealth;

    protected virtual void Awake()
    {
        if (enemyData != null)
        {
            currentHealth = enemyData.maxHealth;
        } else {Debug.LogError("error");}
    }
    
    public virtual void Die()
    {
        onEnemyDeath?.Invoke(this);
        Destroy(gameObject);
    }

    public virtual void GetDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void SetEnemyData(EnemyData data)
    {
        enemyData = data;
        currentHealth = enemyData.maxHealth;
    }
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
}
