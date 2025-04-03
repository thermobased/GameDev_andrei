using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    [Header("Player Info")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth = 50;
    [SerializeField] private int gold = 500;
    
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI goldText;


    private void UpdateStatsUI()
    {
        if (healthText != null)
        {
            healthText.text = $"{currentHealth}";
        }

        if (goldText != null)
        {
            goldText.text = $"Gold: {gold}";
        }
        
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }
    
    public void Heal(int healAmount)
    {
        // Add healing amount
        currentHealth += healAmount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        if (currentHealth < 0)
        {
            //Debug.Log($"{currentHealth}, {healAmount}");
            currentHealth = 0;
        }    
            
        Debug.Log($"Healed for {healAmount}. Current health: {currentHealth}/{maxHealth}");
        UpdateStatsUI();
    }

    public bool Charge(int chargeAmount)
    {
        if (chargeAmount > gold) return false;
        gold -= chargeAmount;
        return true;
    }
    
    private void Start()
    {
        UpdateStatsUI();
    }

}