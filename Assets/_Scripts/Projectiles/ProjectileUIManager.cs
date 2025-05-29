using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProjectileUIManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Button switchButton;
    [SerializeField] private Image projectileIcon;
    [SerializeField] private TextMeshProUGUI quantityText;
    
    [Header("Icon Settings")]
    [SerializeField] private float maxIconSize = 64f;
    [SerializeField] private bool preserveAspectRatio = true;
    [SerializeField] private bool centerIcon = true;
    
    private List<ProjectileType> availableProjectiles = new List<ProjectileType>();
    private int currentProjectileIndex = 0;
    
    private void Start()
    {
        SetupUI();
        SubscribeToEvents();
        InitializeAvailableProjectiles();
        UpdateUIForCurrentProjectile();
    }
    
    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    private void SetupUI()
    {
        if (projectileIcon != null)
        {
            projectileIcon.preserveAspect = preserveAspectRatio;
            
            RectTransform rectTransform = projectileIcon.GetComponent<RectTransform>();
            if (rectTransform != null && centerIcon)
            {
                rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                rectTransform.pivot = new Vector2(0.5f, 0.5f);
            }
        }

        if (switchButton != null)
        {
            switchButton.onClick.AddListener(SwitchToNextProjectile);
        }
    }

    private void SubscribeToEvents()
    {
        Inventory.OnProjectileQuantityChanged += OnProjectileQuantityChanged;
        Inventory.OnCurrentProjectileChanged += OnCurrentProjectileChanged;
    }

    private void UnsubscribeFromEvents()
    {
        Inventory.OnProjectileQuantityChanged -= OnProjectileQuantityChanged;
        Inventory.OnCurrentProjectileChanged -= OnCurrentProjectileChanged;
    }

    private void InitializeAvailableProjectiles()
    {
        if (Inventory.Instance == null) return;
        
        var allProjectiles = Inventory.Instance.GetAllProjectileData();
        availableProjectiles.Clear();
        
        foreach (var projectile in allProjectiles)
        {
            availableProjectiles.Add(projectile.type);
        }
        
        var currentType = Inventory.Instance.GetCurrentProjectileType();
        currentProjectileIndex = availableProjectiles.IndexOf(currentType);
        if (currentProjectileIndex == -1) currentProjectileIndex = 0;
    }

    private void SwitchToNextProjectile()
    {
        if (availableProjectiles.Count == 0 || Inventory.Instance == null) return;
        
        currentProjectileIndex = (currentProjectileIndex + 1) % availableProjectiles.Count;
        var newProjectileType = availableProjectiles[currentProjectileIndex];
        
        Inventory.Instance.SetCurrentProjectileType(newProjectileType);
    }

    private void OnCurrentProjectileChanged(ProjectileType newType)
    {
        UpdateUIForCurrentProjectile();
    }

    private void OnProjectileQuantityChanged(ProjectileType type, int quantity)
    {
        if (type == Inventory.Instance.GetCurrentProjectileType())
        {
            UpdateProjectileQuantity(type, quantity);
        }
    }
    
    private void UpdateUIForCurrentProjectile()
    {
        if (Inventory.Instance == null) return;
        
        var currentType = Inventory.Instance.GetCurrentProjectileType();
        var projectileData = Inventory.Instance.GetProjectileData(currentType);
        
        if (projectileData != null)
        {
            UpdateUI(currentType, projectileData.icon);
        }
    }
    
    public void UpdateUI(ProjectileType type, Sprite icon)
    {
        if (projectileIcon != null && icon != null)
        {
            projectileIcon.sprite = icon;
            projectileIcon.gameObject.SetActive(true);
            
            if (preserveAspectRatio)
            {
                ResizeIconToFit(icon);
            }
        }
        
        var quantity = Inventory.Instance.GetProjectileQuantity(type);
        UpdateProjectileQuantity(type, quantity);
    }
    
    private void UpdateProjectileQuantity(ProjectileType type, int quantity)
    {
        if (quantityText != null)
        {
            var projectileData = Inventory.Instance.GetProjectileData(type);
            if (projectileData != null && projectileData.isUnlimited)
            {
                quantityText.text = "∞";
            }
            else
            {
                quantityText.text = quantity.ToString();
            }
        }
    }
    
    private void ResizeIconToFit(Sprite icon)
    {
        if (icon == null || projectileIcon == null)
            return;
            
        RectTransform rectTransform = projectileIcon.GetComponent<RectTransform>();
        if (rectTransform == null)
            return;
        
        float spriteWidth = icon.rect.width;
        float spriteHeight = icon.rect.height;
        
        float aspectRatio = spriteWidth / spriteHeight;
        
        float width, height;
        
        if (aspectRatio >= 1f)
        {
            width = maxIconSize;
            height = width / aspectRatio;
        }
        else
        {
            height = maxIconSize;
            width = height * aspectRatio;
        }
        
        rectTransform.sizeDelta = new Vector2(width, height);
    }
}