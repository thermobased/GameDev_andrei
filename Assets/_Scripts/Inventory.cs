using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI swordCountText;
    [SerializeField] private TextMeshProUGUI bowCountText;
    [SerializeField] private TextMeshProUGUI axeCountText;
    
    private int swordCount = 0;
    private int bowCount = 0;
    private int axeCount = 0;
    
    public void GetItem(string id)
    {
        switch (id)
        {
            case "11": swordCount++; UpdateCounter(swordCountText, swordCount); break;
            case "13": bowCount++; UpdateCounter(bowCountText, bowCount); break;
            case "12": axeCount++; UpdateCounter(axeCountText, axeCount); break;
        }
    }

    private void UpdateCounter(TextMeshProUGUI counter, int  count)
    {
        counter.text = $"x {count.ToString()}";
    }
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
}
