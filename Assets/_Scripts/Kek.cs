using UnityEngine;
public class Kek : Enemy
{
    [SerializeField] private Color explosionColor = Color.green;
    
    public override void Die()
    {
        // Create explosion effect with green color
        CreateExplosionEffect(explosionColor);
        
        // Call the base class Die method
        base.Die();
    }
}