using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : TruongMonoBehaviour
{
    [SerializeField] protected List<Transform> itemsSpawn;
    [SerializeField] protected ItemType itemType;

    protected override void Start()
    {
        base.Start();
        Spawn();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadItemsSpawn();
    }

    protected void LoadItemsSpawn()
    {
        foreach(Transform transform in transform) 
            itemsSpawn.Add(transform);
    }

    protected void Spawn()
    {
        if (!PlayerPrefs.HasKey(itemType.ToString() + "Device") && !PlayerPrefs.HasKey(itemType.ToString() + "Email")) return;

        if (PlayerPrefs.GetString("DeviceID") == PlayFabController.Instance.MyID)
            SpawnWithID("Device");
        else if (PlayerPrefs.GetString("EmailID") == PlayFabController.Instance.MyID)
            SpawnWithID("Email");
    }
    protected void SpawnWithID(string idName)
    {
        foreach (Transform item in itemsSpawn)
        {
            ItemInfor itemInfor = item.GetComponent<ItemInfor>();
            if (itemInfor.ItemID.ToString() == PlayerPrefs.GetString(itemType.ToString() + idName))
                itemInfor.gameObject.SetActive(true);
            else Destroy(itemInfor.gameObject);
        }
    }
}
