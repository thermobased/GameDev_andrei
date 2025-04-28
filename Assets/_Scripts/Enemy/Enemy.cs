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
    
    protected void CreateExplosionEffect(Color color)
    {
        // Create a particle system for explosion effect
        GameObject effectObj = new GameObject("ExplosionEffect");
        effectObj.transform.position = transform.position;
    
        ParticleSystem particles = effectObj.AddComponent<ParticleSystem>();
    
        // Configure the particle system before playing
        var main = particles.main;
        main.startColor = color;
        main.duration = 1.0f;
        main.startLifetime = 1.0f;
        main.startSize = 0.1f;
        main.startSpeed = 3.0f;
        main.playOnAwake = false; // Don't play automatically
        main.simulationSpace = ParticleSystemSimulationSpace.World;
        main.loop = false; // Make sure it only plays once
    
        // Configure particle system shape for explosion
        var shape = particles.shape;
        shape.shapeType = ParticleSystemShapeType.Sphere;
        shape.radius = 0.1f;
    
        // Configure emission
        var emission = particles.emission;
        emission.rateOverTime = 0;
        emission.SetBursts(new ParticleSystem.Burst[]{ new ParticleSystem.Burst(0.0f, 20) });
    
        // Get the particle renderer
        ParticleSystemRenderer renderer = particles.GetComponent<ParticleSystemRenderer>();
        if (renderer != null)
        {
            renderer.renderMode = ParticleSystemRenderMode.Billboard;
        
            // Use the default particle material
            renderer.material = new Material(Shader.Find("Sprites/Default"));
        }
    
        // Calculate destroy time
        float totalDuration = main.duration + main.startLifetime.constant;
    
        // Start the particle system
        particles.Play();
    
        // Destroy the effect after it completes
        Destroy(effectObj, totalDuration);
    }
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
}
