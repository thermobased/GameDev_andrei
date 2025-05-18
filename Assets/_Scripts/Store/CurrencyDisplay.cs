using UnityEngine;
using TMPro;

public class CurrencyDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currencyText;
    
    private void Start()
    {
        if (CurrencyManager.Instance != null)
        {
            CurrencyManager.Instance.OnCurrencyChanged += UpdateDisplay;
            UpdateDisplay(CurrencyManager.Instance.GetCurrentCurrency());
        }
    }
    
    private void OnDestroy()
    {
        if (CurrencyManager.Instance != null)
            CurrencyManager.Instance.OnCurrencyChanged -= UpdateDisplay;
    }
    
    private void UpdateDisplay(int amount)
    {
        if (currencyText != null)
            currencyText.text = amount.ToString();
    }
}