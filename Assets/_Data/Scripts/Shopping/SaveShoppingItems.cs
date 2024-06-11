using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SaveShoppingItems : TruongMonoBehaviour
{
    [SerializeField] protected ShoppingController shopping;
    [SerializeField] protected ItemType itemType;
    protected override void Start()
    {
        LoadItemType();
        GetItemsUnlockedFromPlayFab();
        Invoke(nameof(SaveFirstItemUnlocked), 0.5f);
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadShopping();
    }

    protected void LoadShopping()
    {
        if (shopping != null) return;
        shopping = GetComponent<ShoppingController>();
    }

    protected void LoadItemType()
    {
        itemType = shopping.itemType;
    }

    public void SaveItemsUnlockedForPlayFab()
    {
        string toString = "";
        foreach (Transform item in shopping.ItemsToSell)
        {
            ItemInfor itemInfor = item.GetComponent<ItemInfor>();
            if(itemInfor == null) continue;
            if (itemInfor.GetIsOpen())
                toString += itemInfor.ItemID;
        }
        Debug.Log(itemType.ToString());
        if (PlayerPrefs.GetString("DeviceID") == PlayFabController.Instance.MyID)
        {
            //PlayFabController.Instance.SetUserData(itemType.ToString() + "Device", toString);
            CoinBank.Instance.SendCoinToPlayFab(itemType.ToString() + "Device", toString);
        }
        else if (PlayerPrefs.GetString("EmailID") == PlayFabController.Instance.MyID)
        {
            //PlayFabController.Instance.SetUserData(itemType.ToString() + "Email", toString);
            CoinBank.Instance.SendCoinToPlayFab(itemType.ToString() + "Email", toString);
        }
    }

    protected void GetItemsUnlockedFromPlayFab()
    {
        if (PlayerPrefs.GetString("DeviceID") == PlayFabController.Instance.MyID)
            GetItemsDeviceID();
        else if (PlayerPrefs.GetString("EmailID") == PlayFabController.Instance.MyID)
            GetItemsEmailID();
    }

    protected void GetItemsDeviceID()
    {
        PlayFabController.Instance.GetUserDataFromPlayFab(() =>
        {
            Dictionary<string, UserDataRecord> data = PlayFabController.Instance.GetUserData();

            foreach (KeyValuePair<string, UserDataRecord> d in data)
            {
                if (itemType.ToString() + "Device" != d.Key) continue;

                UserDataRecord userDataRecord = d.Value;
                string value = userDataRecord.Value.ToString();

                foreach (Transform item in shopping.ItemsToSell)
                {
                    ItemInfor itemInfor = item.GetComponent<ItemInfor>();
                    if (itemInfor == null) continue;

                    for (int i = 0; i < value.Length; i++)
                    {
                        if (itemInfor.ItemID.ToString() == value[i].ToString())
                        {
                            itemInfor.SetIsOpen(true);
                            break;
                        }
                    }
                }

                shopping.ButtonUnlockItem.ChangeStatusItem();
                break;
            }
        });
    }

    protected void GetItemsEmailID()
    {
        PlayFabController.Instance.GetUserDataFromPlayFab(() =>
        {
            Dictionary<string, UserDataRecord> data = PlayFabController.Instance.GetUserData();

            foreach (KeyValuePair<string, UserDataRecord> d in data)
            {
                if (itemType.ToString() + "Email" != d.Key) continue;

                UserDataRecord userDataRecord = d.Value;
                string value = userDataRecord.Value.ToString();

                foreach (Transform item in shopping.ItemsToSell)
                {
                    ItemInfor itemInfor = item.GetComponent<ItemInfor>();
                    if (itemInfor == null) continue;

                    for (int i = 0; i < value.Length; i++)
                    {
                        if (itemInfor.ItemID.ToString() == value[i].ToString())
                        {
                            itemInfor.SetIsOpen(true);
                            break;
                        }
                    }
                }

                shopping.ButtonUnlockItem.ChangeStatusItem();
                break;
            }
        });
    }
    public void SaveFirstItemUnlocked()
    {
        int openCount = 0;
        foreach (Transform transform in shopping.ItemsToSell)
        {
            ItemInfor itemInfor = transform.GetComponent<ItemInfor>();
            if (itemInfor.GetIsOpen())
            {
                openCount++;
                
            }
            Debug.Log(itemInfor.ItemName + ": " + itemInfor.GetIsOpen());
        }
        
        if (openCount == 1) SaveFirstItemSelected();
        else LoadItemSelected();
        shopping.ActivePrice();
    }

    public void SaveFirstItemSelected()
    {
        //Con meoooooooo
        foreach (Transform transform in shopping.ItemsToSell)
        {
            ItemInfor itemInfor = transform.GetComponent<ItemInfor>();
            if (!itemInfor.IsOpen) continue;
            if (PlayerPrefs.GetString("DeviceID") == PlayFabController.Instance.MyID)
            {
                PlayerPrefs.SetString(itemType.ToString() + "Device", itemInfor.ItemID.ToString());
                Debug.Log("SaveItemSelected: " + itemType.ToString() + "Device" + ": " + PlayerPrefs.GetString(itemType.ToString() + "Device"));
            }
            else if (PlayerPrefs.GetString("EmailID") == PlayFabController.Instance.MyID)
            {
                PlayerPrefs.SetString(itemType.ToString() + "Email", itemInfor.ItemID.ToString());
                Debug.Log("SaveItemSelected: " + itemType.ToString() + "Email" + ": " + PlayerPrefs.GetString(itemType.ToString() + "Email"));
            }
        }
        LoadItemSelected();
    }

    protected void LoadItemSelected()
    {
        DeleteSavePlayerPrefs();
        //if (!PlayerPrefs.HasKey(itemType.ToString() + "Device") && !PlayerPrefs.HasKey(itemType.ToString() + "Email")) return;
        //Debug.Log(itemType.ToString() + ": " + PlayerPrefs.GetString(itemType.ToString()));
        if (PlayerPrefs.GetString("DeviceID") == PlayFabController.Instance.MyID) LoadItemSelectedDevice();
        else if (PlayerPrefs.GetString("EmailID") == PlayFabController.Instance.MyID) LoadItemSelectedEmail();
    }

    protected void LoadItemSelectedEmail()
    {
        foreach (Transform transform in shopping.ItemsToSell)
        {
            ItemInfor itemInfor = transform.GetComponent<ItemInfor>();
            if (itemInfor.ItemID.ToString() != PlayerPrefs.GetString(itemType.ToString() + "Email")) continue;
            for (int i = 0; i < shopping.ItemsToSell.Count; i++)
            {
                if (i == itemInfor.ItemID)
                {
                    shopping.ItemName.text = itemInfor.name;
                    shopping.ItemPrice.text = itemInfor.Price.ToString();
                    shopping.ItemsToSell[i].gameObject.SetActive(true);
                }
                else shopping.ItemsToSell[i].gameObject.SetActive(false);
            }
            shopping.ButtonUnlockItem.ChangeStatusItem();
        }

        if (shopping.ItemStats != null) shopping.ItemStats.LoadStatsItem();
    }

    protected void LoadItemSelectedDevice()
    {
        foreach (Transform transform in shopping.ItemsToSell)
        {
            ItemInfor itemInfor = transform.GetComponent<ItemInfor>();
            if (itemInfor.ItemID.ToString() != PlayerPrefs.GetString(itemType.ToString() + "Device")) continue;
            for (int i = 0; i < shopping.ItemsToSell.Count; i++)
            {
                if (i == itemInfor.ItemID)
                {
                    shopping.ItemName.text = itemInfor.name;
                    shopping.ItemPrice.text = itemInfor.Price.ToString();
                    shopping.ItemsToSell[i].gameObject.SetActive(true);
                }
                else shopping.ItemsToSell[i].gameObject.SetActive(false);
            }
            shopping.ButtonUnlockItem.ChangeStatusItem();
        }

        if (shopping.ItemStats != null) shopping.ItemStats.LoadStatsItem();
    }

    protected void DeleteSavePlayerPrefs()
    {
        bool isKey = false;
        foreach(Transform transform in shopping.ItemsToSell)
        {
            ItemInfor itemInfor = transform.GetComponent<ItemInfor>();
            if(itemInfor.IsOpen)
            {
                isKey = true;
                break;
            }
        }

        if (!isKey) PlayerPrefs.DeleteKey(itemType.ToString());
    }
}
