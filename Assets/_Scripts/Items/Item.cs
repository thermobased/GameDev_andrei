using _Scripts;
using UnityEngine;

public class Item : MonoBehaviour, IDamageable
{
    
    [SerializeField] private GameObject destroyEffect;

    public virtual void GetDamage(float damage)
    {

    }

    public virtual void Die()
    {
        if (destroyEffect != null)
        {
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
    
    
}