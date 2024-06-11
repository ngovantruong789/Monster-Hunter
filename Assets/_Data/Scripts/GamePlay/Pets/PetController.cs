using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetController : TruongMonoBehaviour
{
    [SerializeField] protected PetAnimator petAnimator;
    public PetAnimator PetAnimator => petAnimator;

    [SerializeField] protected PetMovement petMovement;
    public PetMovement PetMovement => petMovement;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPetAnimator();
        LoadPetMovement();
    }

    protected void LoadPetAnimator()
    {
        if (petAnimator != null) return;
        petAnimator = transform.GetComponentInChildren<PetAnimator>();
    }

    protected void LoadPetMovement()
    {
        if (petMovement != null) return;
        petMovement = transform.GetComponentInChildren<PetMovement>();
    }
}
