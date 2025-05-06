using UnityEngine;

public class BrickWall : Item
{
    [SerializeField] private float currentHealth = 0;
    
    
    public override void GetDamage(float damage)
    {
        Debug.Log("brickwall damage");
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
        //base.GetDamage(damage);
    }
    
    
    public override void Die()
    {
        base.Die();
    }
}