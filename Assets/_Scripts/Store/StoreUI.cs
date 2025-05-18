using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class StoreUI : MonoBehaviour
{
    [Header("Store Panel")]
    [SerializeField] private GameObject storePanel;
    [SerializeField] private Button openStoreButton;
    [SerializeField] private Button closeStoreButton;
    
    [Header("Currency Display")]
    [SerializeField] private TextMeshProUGUI currencyText;
    [SerializeField] private TextMeshProUGUI storeCurrencyText;
    
    [Header("Store Item Template")]
    [SerializeField] private GameObject storeItemPrefab;
    [SerializeField] private Transform storeItemsContainer;
    
    [Header("Purchase Settings")]
    [SerializeField] private int purchaseQuantity = 5;
    [SerializeField] private Color insufficientFundsColor = Color.red;
    private Color defaultTextColor;
    
    [Header("References")]
    [SerializeField] private ProjectileSpawner projectileSpawner;
    
    private List<StoreItemUI> storeItems = new List<StoreItemUI>();

    private void Start()
    {
        // Ensure we have references
        if (projectileSpawner == null)
            projectileSpawner = FindObjectOfType<ProjectileSpawner>();
            
        // Set up button events
        if (openStoreButton != null)
            openStoreButton.onClick.AddListener(OpenStore);
            
        if (closeStoreButton != null)
            closeStoreButton.onClick.AddListener(CloseStore);
        
        // Initially close the store
        if (storePanel != null)
            storePanel.SetActive(false);
            
        // Subscribe to currency changes
        if (CurrencyManager.Instance != null)
            CurrencyManager.Instance.OnCurrencyChanged += UpdateCurrencyDisplay;
            
        // Initialize currency display
        UpdateCurrencyDisplay(CurrencyManager.Instance.GetCurrentCurrency());
        
        // Save default text color
        if (currencyText != null)
            defaultTextColor = currencyText.color;
            
        // Populate store items
        PopulateStoreItems();
    }
    
    private void OnDestroy()
    {
        // Unsubscribe when destroyed
        if (CurrencyManager.Instance != null)
            CurrencyManager.Instance.OnCurrencyChanged -= UpdateCurrencyDisplay;
            
        if (openStoreButton != null)
            openStoreButton.onClick.RemoveListener(OpenStore);
            
        if (closeStoreButton != null)
            closeStoreButton.onClick.RemoveListener(CloseStore);
            
        // Unsubscribe from store items
        foreach (StoreItemUI item in storeItems)
        {
            if (item != null)
                item.OnPurchaseClicked -= TryPurchaseItem;
        }
    }
    
    private void PopulateStoreItems()
    {
        // Clear existing items
        foreach (Transform child in storeItemsContainer)
        {
            Destroy(child.gameObject);
        }
        storeItems.Clear();
        
        // Get purchasable projectile types
        List<ProjectileType> purchasableTypes = ProjectileInventory.Instance.GetPurchasableProjectileTypes();
        
        foreach (ProjectileType type in purchasableTypes)
        {
            // Get projectile data
            ProjectileData data = null;
            foreach (var projectileData in projectileSpawner.GetProjectileDataList())
            {
                if (projectileData.type == type)
                {
                    data = projectileData;
                    break;
                }
            }
            
            if (data == null)
                continue;
                
            // Create store item
            GameObject itemObject = Instantiate(storeItemPrefab, storeItemsContainer);
            StoreItemUI itemUI = itemObject.GetComponent<StoreItemUI>();
            
            if (itemUI != null)
            {
                // Set up the item UI
                itemUI.Setup(type, data.displayName, data.price, purchaseQuantity, data.icon);
                itemUI.OnPurchaseClicked += TryPurchaseItem;
                
                storeItems.Add(itemUI);
            }
        }
    }
    
    private void UpdateCurrencyDisplay(int currentCurrency)
    {
        if (currencyText != null)
            currencyText.text = currentCurrency.ToString();
            
        if (storeCurrencyText != null)
            storeCurrencyText.text = currentCurrency.ToString();
            
        // Update store items purchase availability
        foreach (StoreItemUI item in storeItems)
        {
            if (item != null)
                item.UpdatePurchaseAvailability(currentCurrency);
        }
    }
    
    private void TryPurchaseItem(ProjectileType data, int price, int quantity)
    {
        if (CurrencyManager.Instance.SpendCurrency(price))
        {
            // Purchase successful, add projectiles
            ProjectileInventory.Instance.AddProjectile(data, quantity);
        }
        else
        {
            // Flash currency text to indicate insufficient funds
            StartCoroutine(FlashInsufficientFunds());
        }
    }
    
    private IEnumerator FlashInsufficientFunds()
    {
        if (currencyText != null && storeCurrencyText != null)
        {
            currencyText.color = insufficientFundsColor;
            storeCurrencyText.color = insufficientFundsColor;
            
            yield return new WaitForSeconds(0.5f);
            
            currencyText.color = defaultTextColor;
            storeCurrencyText.color = defaultTextColor;
        }
    }
    
    public void OpenStore()
    {
        if (storePanel != null)
            storePanel.SetActive(true);
    }
    
    public void CloseStore()
    {
        if (storePanel != null)
            storePanel.SetActive(false);
    }
}