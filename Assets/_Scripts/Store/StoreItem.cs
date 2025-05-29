using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

//[RequireComponent(typeof(Animator))]
public class StoreItem : MonoBehaviour
{
    public event Action<ProjectileData> OnBuyClicked;

    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Button buyButton;

    private ProjectileData _itemInfo;
    public string ItemId => _itemInfo.type.ToString();

    public void Init(ProjectileData itemInfo)
    {
        Debug.Log($"Initializing item {itemInfo.type} with cost {itemInfo.price}");
        _itemInfo = itemInfo;
        icon.sprite = itemInfo.icon;

        icon.type = Image.Type.Simple;
        icon.preserveAspect = true;
    
        priceText.text = itemInfo.price.ToString();
        buyButton.onClick.AddListener(HandleClick);
    }
   
    void HandleClick()
    {
        Debug.Log($"Buy {_itemInfo.type} clicked");
        OnBuyClicked?.Invoke(_itemInfo);
    }
}