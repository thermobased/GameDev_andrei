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
            ApplyEffect(hit);
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
    
    private void ApplyEffect(Collider2D hit)
    {
        Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
        if (rb == null) return;
        
        Vector2 direction = hit.transform.position - transform.position;
        float distance = direction.magnitude;
        float forceFactor = 1f - Mathf.Clamp01(distance / explosionRadius);

        Vector2 forceDirection = direction.normalized;
        forceDirection.y += upwardsModifier * forceFactor;
        forceDirection.Normalize();
        
        Vector2 force = forceDirection * explosionForce * forceFactor;
        rb.AddForce(force, ForceMode2D.Impulse);
        
        float randomSign = Random.Range(0, 2) * 2 - 1;
        float rotationalForce = randomSign * torqueForce * forceFactor;
        rb.AddTorque(rotationalForce, ForceMode2D.Impulse);
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}