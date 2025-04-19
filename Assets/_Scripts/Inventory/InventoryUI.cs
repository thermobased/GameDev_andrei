using Unity.VisualScripting;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private InventorySlotUI[] slotUIs;
    [SerializeField] private GameObject inventoryPanel;
    
    
    private void Start()
    {
        playerInventory.OnInventoryChanged += UpdateUI;
        inventoryPanel.SetActive(false);
        UpdateUI();
    }

    /*private void OnEnable()
    {
        playerInventory.OnInventoryChanged += UpdateUI;
    }*/
    private void OnDestroy()
    {
        if (playerInventory != null)
        {
            playerInventory.OnInventoryChanged -= UpdateUI;
        }
    }
    
    private void Update()
    {

    }
    
    
    private void UpdateUI()
    {
        inventoryPanel.SetActive(true);
        Debug.Log("Updating UI...");
        InventorySlotData[] slots = playerInventory.GetAllSlots();
        
        for (int i = 0; i < slotUIs.Length; i++)
        {
            if (i < slots.Length)
            {
                slotUIs[i].UpdateSlot(slots[i]);
            }
        }
    }
}