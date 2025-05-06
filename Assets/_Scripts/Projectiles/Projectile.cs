using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [Header("Base Settings")]
    [SerializeField] protected float damageAmount = 50f;
    [SerializeField] protected GameObject effectPrefab;
    
    [Header("Layer Settings")]
    [SerializeField] protected LayerMask affectedLayers;
    [SerializeField] protected LayerMask triggeredLayers;
    
    [Header("Visual")]
    [SerializeField] protected Sprite projectileIcon;
    
    public Sprite Icon => projectileIcon;
    public string ProjectileName => GetType().Name;

    protected virtual void OnCollisionEnter2D(Collision2D col)
    {
        if (((1 << col.gameObject.layer) & triggeredLayers.value) != 0)
        {
            Activate();
        }
    }
    
    protected abstract void Activate();
    protected abstract void ApplyEffect(Collider2D hit);
    protected abstract void ApplyDamage(Collider2D hit);
    
    protected virtual void CreateEffect()
    {
        if (effectPrefab != null)
            Instantiate(effectPrefab, transform.position, Quaternion.identity);
    }
    
    protected virtual void OnDrawGizmosSelected() { }
}