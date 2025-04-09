using TMPro;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    private static int keyCount = 0;
    private static int shroomCount = 0;
    private static int hoeCount = 0;
    private TextMeshProUGUI keyCountText;
    private TextMeshProUGUI hoeCountText;
    private TextMeshProUGUI shroomCountText;

    void Start()
    {
        keyCountText = GameObject.Find("KeysAmount").GetComponent<TextMeshProUGUI>();
        hoeCountText = GameObject.Find("HoesAmount").GetComponent<TextMeshProUGUI>();
        shroomCountText = GameObject.Find("ShroomsAmount").GetComponent<TextMeshProUGUI>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            switch (gameObject.name)
            {
                case "Hoe":
                    hoeCount++;
                    break;
                case "Shroom":
                    shroomCount++;
                    break;
                case "Key":
                    keyCount++;
                    break;
            }
            
            //Debug.Log($"{keyCount}, {hoeCount}, {shroomCount}");
            UpdateCounterText();
            Destroy(gameObject);
        }
    }    
    private void UpdateCounterText()
    {
        keyCountText.text = $"x {keyCount.ToString()}";
        shroomCountText.text = $"x {shroomCount.ToString()}";
        hoeCountText.text = $"x {hoeCount.ToString()}";
    }


}
