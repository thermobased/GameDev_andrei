using UnityEngine;

public class Kok : Enemy
{
    [SerializeField] private Color explosionColor = Color.blue;
    
    public override void Die()
    {
        // Create explosion effect with blue color
        CreateExplosionEffect(explosionColor);
        
        // Call the base class Die method
        base.Die();
    }
}