using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageReceiver : TruongMonoBehaviour
{
    [SerializeField] protected Collider2D colliderImpact;
    [SerializeField] protected Rigidbody2D rigidbodyImpact;

    [SerializeField] protected int hp;
    [SerializeField] protected int hpMax;

    [SerializeField] protected bool isDeath;
    public bool IsDeath => isDeath;

    [SerializeField] protected bool isTakeHit;
    public bool IsTakeHit => isTakeHit;

    protected override void Start()
    {
        base.Start();
        Reborn();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadCollider();
        LoadRigidbody();
    }

    protected void LoadCollider()
    {
        if (colliderImpact != null) return;
        colliderImpact = GetComponent<Collider2D>();
        colliderImpact.isTrigger = true;
    }

    protected void LoadRigidbody()
    {
        if (rigidbodyImpact != null) return;
        rigidbodyImpact = GetComponent<Rigidbody2D>();
        rigidbodyImpact.isKinematic = true;
    }

    protected void Reborn()
    {
        hp = hpMax;
    }

    public virtual void AddHp(int amount)
    {
        hp += amount;
        if (hp >= hpMax) hp = hpMax;
    }

    public virtual void DetuctHp(int amount)
    {
        hp -= amount;
        if (hp <= 0)
        {
            hp = 0;
            isDeath = true;
            OnDead();
        }
    }

    protected abstract void LoadStats();
    protected abstract void OnDead();
}
