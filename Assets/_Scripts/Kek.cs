using UnityEngine;
public class Kek : Enemy
{
    [SerializeField] protected GameObject effectPrefab;
    protected virtual void CreateEffect()
    {
        if (effectPrefab != null)
            Instantiate(effectPrefab, transform.position, Quaternion.identity);
    }
    
    public override void Die()
    {
        CreateEffect();
        base.Die();
    }
}