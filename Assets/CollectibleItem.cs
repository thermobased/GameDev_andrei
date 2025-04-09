using TMPro;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    private static int keyCount = 0;
    private TextMeshProUGUI cntComponent;

    void Start()
    {
        cntComponent = GameObject.Find("KeysAmount").GetComponent<TextMeshProUGUI>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            keyCount++;
            Debug.Log(keyCount);
            UpdateCounterText();
            Destroy(gameObject);
        }
    }    
    private void UpdateCounterText()
    {
                 cntComponent.text = $"x {keyCount.ToString()}";
    }


}
