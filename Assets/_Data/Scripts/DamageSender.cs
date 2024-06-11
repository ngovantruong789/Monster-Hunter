using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageSender : TruongMonoBehaviour
{
    [SerializeField] protected int damage;

    public virtual void SendDamage(Transform targetSendDamage)
    {
        DamageReceiver damageReceiver = targetSendDamage.GetComponent<DamageReceiver>();
        if (damageReceiver == null) return;

        damageReceiver.DetuctHp(damage);
    }

    protected abstract void LoadDamage();
}
