using UnityEngine;

public class BrickWall : Item
{
    [SerializeField] private float currentHealth = 0;
    /*[SerializeField] public Sprite[] damageSprites;*/
    
    public override void GetDamage(float damage)
    {
        Debug.Log("brickwall damage");
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
            
        } /*else if (currentHealth < currentHealth / 2)
        {
            UpdateDamageVisual(damageSprites[0]);
        } else if (currentHealth < currentHealth / 4)
        {
            UpdateDamageVisual(damageSprites[1]);
        }*/

        //base.GetDamage(damage);
    }
    
    
    /*public void UpdateDamageVisual(Sprite sprite)
    {
        GetComponent<SpriteRenderer>().sprite = sprite;
    }*/
    
    public override void Die()
    {
        base.Die();
    }
}