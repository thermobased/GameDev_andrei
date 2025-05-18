using UnityEngine;
using System.Collections;
using _Scripts;

public class Dynamite : Projectile
{
    [Header("Dynamite Settings")]
    [SerializeField] private float explosionRadius = 3f;
    [SerializeField] private float explosionForce = 500f;
    [SerializeField] private float upwardsModifier = 3f;
    [SerializeField] private float fallSpeed = 10f;
    [SerializeField] private float torqueForce = 2000f;
    [SerializeField] private float maxFlightTime = 5f;
    [SerializeField] private float delayBeforeDamage = 2f;

    private Rigidbody2D rb;
    private float flightTimer;
    private bool hasExploded = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 3f;
    }

    private void Update()
    {
        if (hasExploded) return;

        rb.linearVelocity = new Vector2(0f, -fallSpeed);
        flightTimer += Time.deltaTime;

        if (flightTimer >= maxFlightTime)
        {
            StartCoroutine(DelayedExplosion());
            hasExploded = true;
        }
    }

    private IEnumerator DelayedExplosion()
    {


        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        yield return new WaitForSeconds(delayBeforeDamage);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, affectedLayers);
        foreach (Collider2D hit in colliders)
        {
            ApplyEffect(hit);
            ApplyDamage(hit);
        }
        CreateEffect();
        Destroy(gameObject);
    }

    protected override void ApplyEffect(Collider2D hit)
    {
        Rigidbody2D hitRb = hit.GetComponent<Rigidbody2D>();
        if (hitRb == null) return;

        Vector2 direction = hit.transform.position - transform.position;
        float distance = direction.magnitude;
        float forceFactor = 1f - Mathf.Clamp01(distance / explosionRadius);

        Vector2 forceDirection = direction.normalized;
        forceDirection.y += upwardsModifier * forceFactor;
        forceDirection.Normalize();

        Vector2 force = forceDirection * explosionForce * forceFactor;
        hitRb.AddForce(force, ForceMode2D.Impulse);

        float randomSign = Random.Range(0, 2) * 2 - 1;
        float rotationalForce = randomSign * torqueForce * forceFactor;
        hitRb.AddTorque(rotationalForce, ForceMode2D.Impulse);
    }

    protected override void ApplyDamage(Collider2D hit)
    {
        IDamageable damageable = hit.GetComponent<IDamageable>();
        if (damageable == null) return;

        float distance = Vector2.Distance(transform.position, hit.transform.position);
        float damageFactor = 1f - Mathf.Clamp01(distance / explosionRadius);
        float damage = damageAmount * damageFactor;

        damageable.GetDamage(damage);
    }

    protected override void Activate()
    {
        if (hasExploded) return;

        StartCoroutine(DelayedExplosion());
        hasExploded = true;
    }
    
    protected override void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}