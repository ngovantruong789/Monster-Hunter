using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShoppingController : TruongMonoBehaviour
{
    [SerializeField] protected SaveShoppingItems saveShoppingItems;
    public SaveShoppingItems SaveShoppingItems => saveShoppingItems;

    [SerializeField] protected ButtonUnlockItem buttonUnlockItem;
    public ButtonUnlockItem ButtonUnlockItem => buttonUnlockItem;

    [SerializeField] protected ButtonChangeItems buttonChangeItems;
    public ButtonChangeItems ButtonChangeItems => buttonChangeItems;

    [SerializeField] protected ItemStats itemStats;
    public ItemStats ItemStats => itemStats;

    [SerializeField] protected List<Transform> itemsToSell;
    public List<Transform> ItemsToSell => itemsToSell;

    [SerializeField] public ItemType itemType;

    [SerializeField] protected TextMeshProUGUI itemName;
    public TextMeshProUGUI ItemName => itemName;

    [SerializeField] protected TextMeshProUGUI itemPrice;
    public TextMeshProUGUI ItemPrice => itemPrice;

    protected override void Start()
    {
        base.Start();
        ActivePrice();
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadItemsToSell();
        LoadSaveShoppingItems();
        LoadButtonUnlockItem();
        LoadItemStats();
        LoadItemsText();
        LoadButtonChangeItems();
    }

    protected void LoadSaveShoppingItems()
    {
        if (saveShoppingItems != null) return;
        saveShoppingItems = GetComponent<SaveShoppingItems>();
    }

    protected void LoadButtonUnlockItem()
    {
        if (buttonUnlockItem != null) return;
        buttonUnlockItem = GetComponentInChildren<ButtonUnlockItem>();
    }

    protected void LoadButtonChangeItems()
    {
        if (buttonChangeItems != null) return;
        buttonChangeItems = GetComponentInChildren<ButtonChangeItems>();
    }

    protected void LoadItemStats()
    {
        if (itemStats != null) return;
        itemStats = GetComponentInChildren<ItemStats>();
    }

    protected void LoadItemsToSell()
    {
        if (itemsToSell.Count < 0) return;
        Transform items = transform.Find("Items");
        foreach(Transform item in items)
            itemsToSell.Add(item);
    }

    protected void LoadItemsText()
    {
        itemName = transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
        Transform price = transform.Find("Price");
        itemPrice = price.GetComponentInChildren<TextMeshProUGUI>();

        foreach (Transform item in itemsToSell)
        {
            ItemInfor itemInfor = item.GetComponent<ItemInfor>();
            if (item.gameObject.activeSelf)
            {
                itemName.text = itemInfor.ItemName;
                itemPrice.text = itemInfor.Price.ToString();
            }
        }
    }

    public ItemInfor GetItemSelected()
    {
        foreach(Transform transform in itemsToSell)
        {
            if (!transform.gameObject.activeSelf) continue;
            ItemInfor itemInfor = transform.GetComponent<ItemInfor>();

            return itemInfor;
        }

        return null;
    }

    public void ActivePrice()
    {
        ItemInfor itemInfor = GetItemSelected();
        Transform price = transform.Find("Price");
        if (itemInfor.GetIsOpen())
            price.gameObject.SetActive(false);
        else price.gameObject.SetActive(true);
    }
}
