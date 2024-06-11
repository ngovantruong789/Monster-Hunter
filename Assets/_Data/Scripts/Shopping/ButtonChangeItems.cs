using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonChangeItems : BaseButton
{
    [SerializeField] protected ShoppingController shopping;

    [SerializeField] protected int indexItems;
    [SerializeField] protected int nextIndex;

    protected override void Start()
    {
        base.Start();
        LoadIndexItems();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadShopping();
    }
    
    protected void LoadShopping()
    {
        if (shopping != null) return;
        shopping = GetComponentInParent<ShoppingController>();
    }


    protected void LoadIndexItems()
    {
        for (int i = 0; i < shopping.ItemsToSell.Count; i++)
            if (shopping.ItemsToSell[i].gameObject.activeSelf)
                indexItems = i;
    }

    protected override void OnClick()
    {
        LoadIndexItems();
        indexItems += nextIndex;
        if(indexItems > shopping.ItemsToSell.Count - 1) indexItems = 0;
        if (indexItems < 0) indexItems = shopping.ItemsToSell.Count - 1;

        ChangeItems(indexItems);
        //shopping.SaveShoppingItems.SaveFirstItemSelected();
    }

    public void ChangeItems(int index)
    {
        for (int i = 0; i < shopping.ItemsToSell.Count; i++)
        {
            ItemInfor itemInfor = shopping.ItemsToSell[i].GetComponent<ItemInfor>();
            if (i == index)
            {
                
                shopping.ItemName.text = itemInfor.name;
                shopping.ItemPrice.text = itemInfor.Price.ToString();
                shopping.ItemsToSell[i].gameObject.SetActive(true);
                SaveItemChange(itemInfor);
            }
            else
                shopping.ItemsToSell[i].gameObject.SetActive(false);
        }

        shopping.ButtonUnlockItem.ChangeStatusItem();
        if (shopping.ItemStats != null) shopping.ItemStats.LoadStatsItem();
        shopping.ActivePrice();
    }

    protected void SaveItemChange(ItemInfor itemInfor)
    {
        if (!itemInfor.GetIsOpen()) return;
        if (PlayerPrefs.GetString("DeviceID") == PlayFabController.Instance.MyID)
        {
            PlayerPrefs.SetString(shopping.itemType.ToString() + "Device", itemInfor.ItemID.ToString());
        }
        else if (PlayerPrefs.GetString("EmailID") == PlayFabController.Instance.MyID)
        {
            PlayerPrefs.SetString(shopping.itemType.ToString() + "Email", itemInfor.ItemID.ToString());
        }
    }
}
