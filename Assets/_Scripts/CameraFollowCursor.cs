using UnityEngine;

public class CameraEdgeFollow : MonoBehaviour
{
    [Header("Camera Movement Settings")]
    [Tooltip("How close to the screen edge (in percentage) to trigger camera movement")]
    [Range(0.05f, 0.5f)]
    public float edgeThreshold = 0.1f;

    [Tooltip("Speed of camera movement")]
    public float cameraMoveSpeed = 5f;

    [Tooltip("Max horizontal distance the camera can move")]
    public float maxHorizontalMove = 60f;

    private Camera mainCamera;
    private Vector3 initialCameraPosition;
    private float currentCameraOffset = 0f;

    void Start()
    {
        mainCamera = Camera.main;
        initialCameraPosition = mainCamera.transform.position;
    }

    void Update()
    {
        // Convert mouse position to viewport space
        Vector3 mouseViewportPos = mainCamera.ScreenToViewportPoint(Input.mousePosition);

        // Determine camera movement direction
        float moveDirection = 0f;

        // Check right edge of screen
        if (mouseViewportPos.x > (1f - edgeThreshold))
        {
            moveDirection = 1f; // Move right
        }
        // Check left edge of screen
        else if (mouseViewportPos.x < edgeThreshold)
        {
            moveDirection = -1f; // Move left
        }

        // Calculate new camera position
        currentCameraOffset += moveDirection * cameraMoveSpeed * Time.deltaTime;
        
        // Clamp camera movement
        currentCameraOffset = Mathf.Clamp(currentCameraOffset, -maxHorizontalMove, maxHorizontalMove);

        // Update camera position
        Vector3 newCameraPosition = new Vector3(
            initialCameraPosition.x + currentCameraOffset, 
            mainCamera.transform.position.y, 
            mainCamera.transform.position.z
        );

        mainCamera.transform.position = newCameraPosition;
    }
}