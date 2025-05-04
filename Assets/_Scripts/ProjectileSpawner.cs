using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ProjectileSpawner : MonoBehaviour
{
    [Header("Projectile Settings")]
    [SerializeField] private List<ProjectileData> projectilePrefabs = new List<ProjectileData>();
    [SerializeField] private ProjectileType currentProjectileType = ProjectileType.Bomb;
    
    [Header("References")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Button switchProjectileButton;
    [SerializeField] private Image currentProjectileIcon;
    
    [Header("UI Settings")]
    [SerializeField] private bool ignoreFullscreenCanvas = true;
    [SerializeField] private List<RectTransform> uiElementsToCheck = new List<RectTransform>();
    
    public System.Action<ProjectileType, Sprite> OnProjectileTypeChanged;
    
    private void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
        
        if (switchProjectileButton != null)
        {
            switchProjectileButton.onClick.AddListener(SwitchProjectileType);
            
            RectTransform buttonRect = switchProjectileButton.GetComponent<RectTransform>();
            if (buttonRect != null && !uiElementsToCheck.Contains(buttonRect))
            {
                uiElementsToCheck.Add(buttonRect);
            }
        }
            
        // Set initial icon
        UpdateProjectileIcon();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!IsPointerOverInteractiveUI())
            {
                SpawnProjectile();
            }
        }
    }

    private void SpawnProjectile()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePos);
        worldPosition.z = 0;
        
        GameObject prefabToSpawn = GetCurrentProjectilePrefab();
        if (prefabToSpawn != null)
        {
            Instantiate(prefabToSpawn, worldPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning($"No prefab found for projectile type: {currentProjectileType}");
        }
    }
    
    private GameObject GetCurrentProjectilePrefab()
    {
        foreach (var data in projectilePrefabs)
        {
            if (data.type == currentProjectileType)
                return data.prefab;
        }
        return null;
    }
    
    private void SwitchProjectileType()
    {
        ProjectileType[] projectileTypes = (ProjectileType[])System.Enum.GetValues(typeof(ProjectileType));
        
        int currentIndex = (int)currentProjectileType;
        int nextIndex = (currentIndex + 1) % projectileTypes.Length;
        
        currentProjectileType = projectileTypes[nextIndex];
        UpdateProjectileIcon();
        
        Debug.Log($"Switched to projectile type: {currentProjectileType}");
    }
    
    private void UpdateProjectileIcon()
    {
        if (currentProjectileIcon == null)
            return;
        
        Sprite currentIcon = null;
        foreach (var data in projectilePrefabs)
        {
            if (data.type == currentProjectileType)
            {
                currentIcon = data.icon;
                currentProjectileIcon.sprite = currentIcon;
                currentProjectileIcon.gameObject.SetActive(true);
                
                if (OnProjectileTypeChanged != null)
                    OnProjectileTypeChanged(currentProjectileType, currentIcon);
                    
                return;
            }
        }
        
        currentProjectileIcon.gameObject.SetActive(false);
    }
    
    private bool IsPointerOverInteractiveUI()
    {
        if (EventSystem.current == null)
            return false;
            
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        
        if (results.Count == 0)
            return false;
        
        if (ignoreFullscreenCanvas)
        {
            foreach (RaycastResult result in results)
            {
                if (uiElementsToCheck.Count > 0)
                {
                    RectTransform resultRect = result.gameObject.GetComponent<RectTransform>();

                    if (resultRect != null && IsInteractiveUIElement(result.gameObject))
                    {
                        foreach (RectTransform uiElement in uiElementsToCheck)
                        {
                            if (resultRect == uiElement || IsChildOf(resultRect, uiElement))
                            {
                                return true;
                            }
                        }
                    }
                }
                else if (IsInteractiveUIElement(result.gameObject))
                {
                    return true;
                }
            }

            return false;
        }
        
        return true;
    }
    
    private bool IsInteractiveUIElement(GameObject obj)
    {
        // Check for common interactive UI components
        if (obj.GetComponent<Button>() != null ||
            obj.GetComponent<Toggle>() != null ||
            obj.GetComponent<Slider>() != null ||
            obj.GetComponent<Dropdown>() != null ||
            obj.GetComponent<InputField>() != null ||
            obj.GetComponent<ScrollRect>() != null)
        {
            return true;
        }
        
        return false;
    }
    
    private bool IsChildOf(RectTransform child, RectTransform parent)
    {
        Transform current = child.parent;
        while (current != null)
        {
            if (current == parent)
                return true;
                
            current = current.parent;
        }
        
        return false;
    }
}