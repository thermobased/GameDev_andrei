using UnityEngine;

public class Box : Item
{
    [SerializeField] private GameObject[] dropPrefabs;

    public override void GetDamage(float damage)
    {
        Debug.Log("box damage");
        if (damage > 0)
        {
            Die();
        }
        //base.GetDamage(damage);
    }
    
    public override void Die()
    {
        //drop something
        base.Die();
    }
}