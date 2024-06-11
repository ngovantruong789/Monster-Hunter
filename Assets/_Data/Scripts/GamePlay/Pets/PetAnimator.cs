using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetAnimator : BaseAnimator
{
    protected override void Start()
    {
        base.Start();
        RandomAction();
    }

    public void PlayAnimRun(bool value)
    {
        animator.SetBool("IsRun", value);
    }

    public void RandomAction()
    {
        int rand = Random.Range(1, 4);
        animator.SetTrigger("Action_" + rand);
    }
}
