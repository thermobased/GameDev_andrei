using UnityEngine;
using UnityEngine.UI;
public class Pointer : MonoBehaviour
{
    private Image crosshairImage;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        Cursor.visible = false;
        if(crosshairImage == null) crosshairImage = GetComponent<Image>();
    }

    // Update is called once per frame
    private void Update()
    {
        Vector2 mousePosition = Input.mousePosition;
        crosshairImage.rectTransform.position = mousePosition;
    }
}
