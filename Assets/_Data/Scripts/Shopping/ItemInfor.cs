using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfor : TruongMonoBehaviour
{
    [SerializeField] protected int itemID;
    public int ItemID => itemID;

    [SerializeField] protected string itemName;
    public string ItemName => itemName;

    [SerializeField] protected int price;
    public int Price => price;

    [SerializeField] protected bool isOpen;
    public bool IsOpen => isOpen;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        SetHunterName();
    }

    protected void SetHunterName()
    {
        itemName = transform.name;
    }

    public void SetIsOpen(bool isOpen)
    {
        this.isOpen = isOpen;
    }

    public bool GetIsOpen()
    {
        return isOpen;
    }


}
