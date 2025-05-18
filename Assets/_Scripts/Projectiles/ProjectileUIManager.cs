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
    
    [Header("References")]
    [SerializeField] private ProjectileSpawner spawner;
    
    private void Start()
    {
        if (spawner == null)
            spawner = FindObjectOfType<ProjectileSpawner>();
            
        if (spawner != null)
        {
            spawner.OnProjectileTypeChanged += UpdateUI;
            
            var type = ProjectileInventory.Instance.GetProjectileType();
            var data = spawner.GetProjectileData(type);
            UpdateUI(type, data.icon);
        }
        
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
        
        if (ProjectileInventory.Instance != null)
        {
            ProjectileInventory.Instance.OnProjectileQuantityChanged += OnProjectileQuantityChanged;

            UpdateProjectileQuantity(
                ProjectileInventory.Instance.GetProjectileType(),
                ProjectileInventory.Instance.GetProjectileQuantity(ProjectileInventory.Instance.GetProjectileType())
            );
        }
        
        UpdateProjectileQuantity(ProjectileInventory.Instance.GetProjectileType(), 
                                ProjectileInventory.Instance.GetProjectileQuantity(ProjectileInventory.Instance.GetProjectileType()));
    }
    
    private void OnDestroy()
    {
        if (ProjectileInventory.Instance != null)
            ProjectileInventory.Instance.OnProjectileQuantityChanged -= OnProjectileQuantityChanged;
    }
    
    private void OnProjectileQuantityChanged(ProjectileType data, int quantity)
    {
        if (data == ProjectileInventory.Instance.GetCurrentProjectileType())
        {
            UpdateProjectileQuantity(data, quantity);
        }
    }
    
    private void UpdateProjectileQuantity(ProjectileType data, int quantity)
    {
        if (quantityText != null)
        {
            if (data == ProjectileType.Bomb)
            {
                quantityText.text = "∞";
            }
            else
            {
                quantityText.text = quantity.ToString();
            }
        }
    }
    
    public void UpdateUI(ProjectileType data, Sprite icon)
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
        
        UpdateProjectileQuantity(data, ProjectileInventory.Instance.GetProjectileQuantity(data));
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