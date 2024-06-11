using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAnimator : TruongMonoBehaviour
{
    [SerializeField] protected Animator animator;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadAnimator();
    }

    protected void LoadAnimator()
    {
        if (animator != null) return;
        animator = transform.GetComponent<Animator>();
    }
}
