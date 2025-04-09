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

    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private AudioClip allPickedup;
    [SerializeField] [Range(0f, 1f)] private float volume = 1f;
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
            if (pickupSound != null)
            {
                if(hoeCount+keyCount+shroomCount == 12){AudioSource.PlayClipAtPoint(allPickedup, transform.position, volume);}
                else {AudioSource.PlayClipAtPoint(pickupSound, transform.position, volume);}
            }
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
