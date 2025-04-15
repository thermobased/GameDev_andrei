using UnityEngine;
using System;
using TMPro;
using UnityEngine.Rendering.Universal;

public class CollectibleWeapon : MonoBehaviour
{
    private Inventory inventory;
    public static Action<WeaponData> onWeaponPickedUp;
    WeaponData weaponData;
    private SpriteRenderer spriteRenderer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        onWeaponPickedUp?.Invoke(weaponData);
        inventory.GetItem(weaponData.id);
        Destroy(gameObject);
    }

    public void SetWeaponData(WeaponData data)
    {
        weaponData = data;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = data.sprite;
        }
    }
    void Start()
    {
        inventory = GameObject.FindFirstObjectByType<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
