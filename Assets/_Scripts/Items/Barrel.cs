using UnityEngine;
using _Scripts;
public class Barrel : Item
{
    [Header("Explosion Settings")]
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private float explosionForce = 700f;
    [SerializeField] private float upwardsModifier = 3f;
    [SerializeField] private float torqueForce = 2000f;
    [SerializeField] private float damageAmount = 2f;
    private bool isDead = false;

    [SerializeField] protected LayerMask affectedLayers;
    //[SerializeField] protected LayerMask triggeredLayers;

    public override void GetDamage(float damage)
    {
        if (isDead)
        {
            return;
        }

        if (damage > 0)
        {
            Die();
        }

    }
    
    public override void Die()
    {
        if (isDead) return;
        isDead = true;
        Explode();
        base.Die();
    }
    
    
    private void Explode()
    {
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, affectedLayers);
        foreach (Collider2D hit in colliders)
        {
            ApplyDamage(hit);
        }

    }
    
    private void ApplyDamage(Collider2D hit)
    {
        IDamageable damageable = hit.GetComponent<IDamageable>();
        if (damageable == null) return;
        
        
        float distance = Vector2.Distance(transform.position, hit.transform.position);
        float damageFactor = 1f - Mathf.Clamp01(distance / explosionRadius);
        float damage = damageAmount * damageFactor;
        
        damageable.GetDamage(damage);
    }
}