using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.Serialization;

public class ProjectileSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private LayerMask spawnLayerMask = -1;
    
    public event Action<ProjectileType, Sprite> OnProjectileTypeChanged;

    private void Start()
    {
        if (playerCamera == null)
            playerCamera = Camera.main;
        
        Inventory.OnCurrentProjectileChanged += HandleCurrentProjectileChanged;
        
        if (Inventory.Instance != null)
        {
            var currentType = Inventory.Instance.GetCurrentProjectileType();
            HandleCurrentProjectileChanged(currentType);
        }
    }

    private void OnDestroy()
    {
        Inventory.OnCurrentProjectileChanged -= HandleCurrentProjectileChanged;
    }

    private void HandleCurrentProjectileChanged(ProjectileType newType)
    {
        var projectileData = Inventory.Instance.GetProjectileData(newType);
        if (projectileData != null)
        {
            OnProjectileTypeChanged?.Invoke(newType, projectileData.icon);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleMouseClick();
        }
    }

    private void HandleMouseClick()
    {
        if (Inventory.Instance == null || playerCamera == null)
            return;
        
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        Vector3 mousePosition = Input.mousePosition;
        Ray ray = playerCamera.ScreenPointToRay(mousePosition);
    
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, spawnLayerMask))
        {
            SpawnProjectileAtPosition(hit.point);
        }
        else
        {
            Vector3 spawnPoint = ray.origin + ray.direction * 10f;
            SpawnProjectileAtPosition(spawnPoint);
        }
    }

    private void SpawnProjectileAtPosition(Vector3 position)
    {
        var currentType = Inventory.Instance.GetCurrentProjectileType();
        
        if (!Inventory.Instance.CanUseProjectile(currentType))
        {
            Debug.Log($"Cannot use projectile {currentType} - insufficient quantity or not available");
            return;
        }

        var projectileData = Inventory.Instance.GetProjectileData(currentType);
        if (projectileData?.prefab == null)
        {
            Debug.LogError($"No prefab found for projectile type {currentType}");
            return;
        }
        
        if (Inventory.Instance.UseProjectile(currentType))
        {
            GameObject spawnedProjectile = Instantiate(projectileData.prefab, position, Quaternion.identity);
            Debug.Log($"Spawned {currentType} at {position}");
        }
    }

    public ProjectileData GetProjectileData(ProjectileType type)
    {
        return Inventory.Instance?.GetProjectileData(type);
    }
}
