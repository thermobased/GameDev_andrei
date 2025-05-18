using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class StoreItemUI : MonoBehaviour
{
    public event Action<ProjectileType, int, int> OnPurchaseClicked;

    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private TextMeshProUGUI quantityText;
    [SerializeField] private Button purchaseButton;
    
    private ProjectileType _itemData;
    private int itemPrice;
    private int purchaseQuantity;
    
    public void Setup(ProjectileType data, string name, int price, int quantity, Sprite icon)
    {
        _itemData = data;
        itemPrice = price;
        purchaseQuantity = quantity;
        
        if (itemNameText != null)
            itemNameText.text = name;
            
        if (priceText != null)
            priceText.text = price.ToString();
            
        if (quantityText != null)
            quantityText.text = "x" + quantity.ToString();
            
        if (itemIcon != null && icon != null)
        {
            itemIcon.sprite = icon;
            itemIcon.preserveAspect = true;
        }
        
        if (purchaseButton != null)
            purchaseButton.onClick.AddListener(OnButtonClick);
            
        // Initial availability check
        UpdatePurchaseAvailability(CurrencyManager.Instance.GetCurrentCurrency());
    }
    
    public void UpdatePurchaseAvailability(int currentCurrency)
    {
        if (purchaseButton != null)
        {
            bool canAfford = currentCurrency >= itemPrice;
            purchaseButton.interactable = canAfford;
        }
    }
    
    private void OnButtonClick()
    {
        OnPurchaseClicked?.Invoke(_itemData, itemPrice, purchaseQuantity);
    }
    
    private void OnDestroy()
    {
        if (purchaseButton != null)
            purchaseButton.onClick.RemoveListener(OnButtonClick);
    }
}