using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetMovement : TruongMonoBehaviour
{
    [SerializeField] protected PetController controller;
    public PetController PetController => controller;

    [SerializeField] protected Transform target;
    [SerializeField] protected float speed = 0.05f;
    [SerializeField] protected float distance;
    [SerializeField] protected float distanceTarget = 2f;
    [SerializeField] protected float distanceTargetLimit = 10f;
    [SerializeField] protected bool isFollow = false;

    protected override void Start()
    {
        base.Start();
        Invoke(nameof(LoadTarget), 1f);
    }

    private void FixedUpdate()
    {
        Movement();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPetController();
    }

    protected void LoadPetController()
    {
        if (controller != null) return;
        controller = transform.parent.GetComponent<PetController>();
    }

    protected void LoadTarget()
    {
        target = FindObjectOfType<PlayerController>().transform;
    }

    protected void Movement()
    {
        if (target == null) return;
        distance = Vector3.Distance(transform.parent.position, target.position);
        if (distance <= distanceTargetLimit && isFollow) return;
        isFollow = false;

        controller.PetAnimator.PlayAnimRun(true);
        ChangeLook();
        transform.parent.position = Vector3.MoveTowards(transform.parent.position, target.position, speed);

        if(distance <= distanceTarget)
        {
            isFollow = true;
            Invoke(nameof(PlayAction), 0.2f);
        }
    }

    protected void ChangeLook()
    {
        Vector3 scale = transform.parent.localScale;
        if (target.position.x < transform.parent.position.x) scale.x = -1;
        else if (target.position.x > transform.parent.position.x) scale.x = 1;

        transform.parent.localScale = scale;
    }

    protected void PlayAction()
    {
        controller.PetAnimator.PlayAnimRun(false);
        controller.PetAnimator.RandomAction();
    }
}
