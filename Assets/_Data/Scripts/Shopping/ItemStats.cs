using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemStats : TruongMonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI attack;
    [SerializeField] protected TextMeshProUGUI speed;
    [SerializeField] protected TextMeshProUGUI health;

    [SerializeField] protected ShoppingController shopping;
    public ShoppingController ShoppingController => shopping;

    [SerializeField] protected PlayerSO playerSO;
    public PlayerSO PlayerSO => playerSO;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadShoppingController();
        LoadStatsText();
    }

    protected void LoadShoppingController()
    {
        if (shopping != null) return;
        shopping = transform.parent.GetComponent<ShoppingController>();
    }

    protected void LoadPlayerSO()
    {
        string resPath = "Players/" + shopping.GetItemSelected().ItemName;
        playerSO = Resources.Load<PlayerSO>(resPath);
    }

    protected void LoadStatsText()
    {
        attack = transform.Find("Attack").GetComponentInChildren<TextMeshProUGUI>();
        speed = transform.Find("Speed").GetComponentInChildren<TextMeshProUGUI>();
        health = transform.Find("Health").GetComponentInChildren<TextMeshProUGUI>();
    }

    public void LoadStatsItem()
    {
        LoadPlayerSO();
        attack.text = playerSO.damage.ToString();
        speed.text = playerSO.speed.ToString();
        health.text = playerSO.hpMax.ToString();
    }
}
