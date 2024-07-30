using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonUnlockItem : BaseButton
{
    [SerializeField] protected ShoppingController shopping;
    [SerializeField] protected Transform lockItemImage;
    [SerializeField] protected Color colorLock = Color.black;
    [SerializeField] protected Color colorUnlock = Color.white;

    protected override void Start()
    {
        base.Start();
        ChangeStatusItem();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadShopping();
        LoadLockItemImage();
    }

    protected void LoadShopping()
    {
        if (shopping != null) return;
        shopping = GetComponentInParent<ShoppingController>();
    }

    protected void LoadLockItemImage()
    {
        if(lockItemImage != null) return;
        lockItemImage = shopping.transform.Find("LockItem");
    }

    //Tìm itemInfor bằng cái item đang chọn
    //Sau đó trừ tiền, nếu không trừ được(false) thì không unclock
    //Sau đó mình SetIsOpen rồi gọi ChangeStatusItem, lưu dữ liệu lên playfab, lưu item vào PlayerPrefs để lần sau vào game nó sẽ tự động chuyển sang

    protected void UnLockItems()
    {
        ItemInfor itemInfor = shopping.GetItemSelected();
        if (itemInfor == null) return;
        //if (itemInfor.GetIsOpen()) return;
        bool isCoin = CoinBank.Instance.DetuctCoin(itemInfor.Price);
        if (!isCoin) return;    

        itemInfor.SetIsOpen(true);
        //lockItemImage.gameObject.SetActive(false);
        ChangeStatusItem();

        shopping.SaveShoppingItems.SaveItemsUnlockedForPlayFab();
        SaveItemUnlock(itemInfor);
        //shopping.SaveShoppingItems.SaveFirstItemUnlocked();
        shopping.ActivePrice();
    }

    protected void SaveItemUnlock(ItemInfor itemInfor)
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

    //Lọc qua các item và lấy isOpen 
    //Nếu true thì bật hình ảnh và tắt ổ khoá, false thì ngược lại
    public void ChangeStatusItem()
    {
        ItemInfor itemInfor = shopping.GetItemSelected();
        if(itemInfor == null) return;
        SpriteRenderer spriteRenderer = itemInfor.GetComponentInChildren<SpriteRenderer>();
        Image image = GetComponent<Image>();
        if (itemInfor.GetIsOpen())
        {
            spriteRenderer.color = colorUnlock;
            image.enabled = false;
            button.interactable = false;
            lockItemImage.gameObject.SetActive(false);
        }
        else
        {
            spriteRenderer.color = colorLock;
            image.enabled = true;
            button.interactable = true;
            lockItemImage.gameObject.SetActive(true);
        }
    }

    protected override void OnClick()
    {
        UnLockItems();
    }
}
