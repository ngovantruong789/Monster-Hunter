using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAbility : TruongMonoBehaviour
{
    [Header("BaseAbility")]
    [SerializeField] protected float timerAbility;
    [SerializeField] protected float timeAbility;
    [SerializeField] protected bool isReady = false;

    protected override void Start()
    {
        base.Start();
        Reborn();
    }

    protected virtual void FixedUpdate()
    {
        Timing();
    }


    protected virtual void Timing()
    {
        if (isReady) return;

        timerAbility -= Time.fixedDeltaTime;
        if (timerAbility > 0) return;
        Reborn();

        isReady = true;
    }
    
    protected virtual void Reborn()
    {
        timerAbility = timeAbility;
    }
}
