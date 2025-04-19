using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI amountText;
    [SerializeField] private GameObject background;
    
    private InventorySlotData currentSlot;
    
    public void UpdateSlot(InventorySlotData slot)
    {
        currentSlot = slot;
        background.SetActive(slot.itemData != null);
        
        if (slot.itemData != null)
        {
            itemIcon.sprite = slot.itemData.icon;
            itemIcon.enabled = true;
            
            if (slot.amount > 1)
            {
                amountText.text = slot.amount.ToString();
                amountText.gameObject.SetActive(true);
            }
            else
            {
                amountText.gameObject.SetActive(false);
            }
        }
        else
        {
            // Empty slot
            itemIcon.sprite = null;
            itemIcon.enabled = false;
            amountText.gameObject.SetActive(false);
        }
    }
    
}