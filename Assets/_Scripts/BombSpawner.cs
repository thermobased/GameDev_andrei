using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private Camera mainCamera;

    private void Start()
    {
        
    }

    private void Update()
    {
        // Check for left mouse button click
        if (Input.GetMouseButtonDown(0))
        {
            SpawnBomb();
        }
    }

    private void SpawnBomb()
    {
        // Get mouse position in screen space
        Vector3 mousePos = Input.mousePosition;
        
        // Convert screen position to world position
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePos);
        
        // Set z position to 0 since we're in 2D
        worldPosition.z = 0;
        
        // Instantiate the bomb at the world position
        Instantiate(bombPrefab, worldPosition, Quaternion.identity);
    }
}