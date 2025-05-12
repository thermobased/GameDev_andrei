using UnityEngine;

public class Box : Item
{
    [SerializeField] private GameObject[] dropPrefabs;
    [SerializeField] private float dropForce = 5f;
    [SerializeField] private float dropSpreadAngle = 45f;

    public override void GetDamage(float damage)
    {
        Debug.Log("box damage");
        if (damage > 0)
        {
            Die();
        }
    }
    
    public override void Die()
    {
        DropItems();
        base.Die();
    }

    private void DropItems()
    {
        if (dropPrefabs == null || dropPrefabs.Length == 0)
            return;
        
        int itemsToDrop = Random.Range(0, dropPrefabs.Length + 1);

        for (int i = 0; i < itemsToDrop; i++)
        {
            GameObject itemPrefab = dropPrefabs[Random.Range(0, dropPrefabs.Length)];
            
            if (itemPrefab != null)
            {
                GameObject droppedItem = Instantiate(itemPrefab, transform.position, Quaternion.identity);
                
                Rigidbody rb = droppedItem.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Vector3 dropDirection = Quaternion.Euler(
                        Random.Range(-dropSpreadAngle, dropSpreadAngle),
                        Random.Range(-dropSpreadAngle, dropSpreadAngle),
                        Random.Range(-dropSpreadAngle, dropSpreadAngle)
                    ) * transform.up;
                    
                    rb.AddForce(dropDirection * dropForce, ForceMode.Impulse);
                }
            }
        }
    }
}