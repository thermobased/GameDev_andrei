using UnityEngine;
using System;
using System.Collections;
using _Scripts;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    public static event Action<Enemy> onEnemyDeath;
    [SerializeField] protected EnemyData enemyData;
    [SerializeField] protected GameObject damageTextPrefab;
    [SerializeField] protected Color damageTextColor = Color.white;
    
    private float currentHealth;
    private SpriteRenderer spriteRenderer;

    [Header("Damage Feedback")]
    [SerializeField] protected float damageTintDuration = 0.15f;
    [SerializeField] protected Color damageTintColor = new Color(1f, 0.3f, 0.3f, 1f);

    protected virtual void Awake()
    {
        if (enemyData != null)
        {
            currentHealth = enemyData.maxHealth;
        } 
        else 
        {
            Debug.LogError("EnemyData is null!");
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            if (spriteRenderer == null)
            {
                Debug.LogWarning("No SpriteRenderer found on Enemy or its children!");
            }
        }
    }
    
    public EnemyData GetEnemyData()
    {
        return enemyData;
    }
    
    
    public virtual void Die()
    {
        onEnemyDeath?.Invoke(this);
        Destroy(gameObject);
    }

    public virtual void GetDamage(float damage)
    {
        currentHealth -= damage;
        
        if (spriteRenderer != null)
        {
            StartCoroutine(FlashSprite());
        }
        
        SpawnDamageText(damage);
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual IEnumerator FlashSprite()
    {
        Color originalColor = spriteRenderer.color;
        
        spriteRenderer.color = damageTintColor;
        
        yield return new WaitForSeconds(damageTintDuration);
        
        spriteRenderer.color = originalColor;
    }
    
    protected virtual void SpawnDamageText(float damage)
    {
        if (damageTextPrefab != null)
        {
            Vector3 spawnPosition = transform.position + new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), 0.5f, 0);
            GameObject damageTextObject = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity);

            DamageText damageText = damageTextObject.GetComponent<DamageText>();
            if (damageText != null)
            {
                damageText.Setup(damage, damageTextColor);
            }
        }
    }

    public void SetEnemyData(EnemyData data)
    {
        enemyData = data;
        currentHealth = enemyData.maxHealth;
    }

}