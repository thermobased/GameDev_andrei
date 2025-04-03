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

        // Check if player needs healing
        if (currentHealth < maxHealth)
        {
            // Add healing amount
            currentHealth += healAmount;
            
            // Make sure we don't exceed max health
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            
            Debug.Log($"Healed for {healAmount}. Current health: {currentHealth}/{maxHealth}");
            UpdateStatsUI();
        }
    }

    public bool Charge(int chargeAmount)
    {
        if (chargeAmount <= gold)
        {
            gold -= chargeAmount;
            return true;
        }
        return false;
    }
    
    private void Start()
    {
        UpdateStatsUI();
    }

}