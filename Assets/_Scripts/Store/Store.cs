using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Store : MonoBehaviour
{
   [SerializeField] private Transform contentRoot;
   [SerializeField] private GameObject itemPrefab;
   [SerializeField] private List<ProjectileData> storeItems;
   [SerializeField] private Animator _animator;
    
   private void Awake()
   {
      _animator = GetComponent<Animator>();
      foreach (var item in storeItems)
      {
         Debug.Log($"Shop creates a button for {item.type}");
         var newItemGO = Instantiate(itemPrefab, contentRoot);
         var newItem = newItemGO.GetComponent<StoreItem>();
         newItem.Init(item);
         newItem.OnBuyClicked += HandleBuyClicked;
         CloseShop();
      }
   }
   
   private void HandleBuyClicked(ProjectileData item)
   {
      if (Inventory.Instance.TryBuyItem(item))
      {
         Debug.Log($"Bought an item - {item.type}.");
      }
      else
      {
         Debug.Log($"Failed to buy item {item.type}, not enough coins.");
      }
   }
   
      private void Update()
      {
         if (Input.GetKeyDown(KeyCode.Tab))
         {
            _animator.SetTrigger("close");
         }
      }
      
      public void CloseShop()
      {
         gameObject.SetActive(false);
      }
      
   
   // public void ResetShop()
   // {
   //    Debug.Log("Shop reset requested");
   //    Inventory.Instance.ResetShop();
   //  
   //    foreach (Transform child in contentRoot)
   //    {
   //       var shopItem = child.GetComponent<ShopItem>();
   //       if (shopItem != null)
   //       {
   //          shopItem.SetUnpurchased();
   //       }
   //    }
   // }
}