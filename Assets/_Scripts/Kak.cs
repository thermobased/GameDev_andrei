using UnityEngine;

public class Kak : Enemy
{
    [SerializeField] private Color explosionColor = Color.red;
    
    public override void Die()
    {
        // Create explosion effect with red color
        CreateExplosionEffect(explosionColor);
        
        // Call the base class Die method
        base.Die();
    }
}