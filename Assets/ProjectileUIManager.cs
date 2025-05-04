using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProjectileUIManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Button switchButton;
    [SerializeField] private Image projectileIcon;
    
    [Header("Icon Settings")]
    [SerializeField] private float maxIconSize = 64f;
    [SerializeField] private bool preserveAspectRatio = true;
    [SerializeField] private bool centerIcon = true;
    
    [Header("References")]
    [SerializeField] private ProjectileSpawner spawner;
    
    private void Start()
    {
        // Ensure we have references
        if (spawner == null)
            spawner = FindObjectOfType<ProjectileSpawner>();
            
        if (spawner != null)
        {
            spawner.OnProjectileTypeChanged += UpdateUI;
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