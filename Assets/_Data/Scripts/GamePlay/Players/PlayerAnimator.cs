using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : BaseAnimator
{
    public void SetBoolParametersWithValue(string value)
    {
        foreach (AnimatorControllerParameter parameter in animator.parameters)
        {
            if (parameter.type != AnimatorControllerParameterType.Bool) continue;
            if (parameter.name == value) animator.SetBool(parameter.name, true);
            else animator.SetBool(parameter.name, false);
        }
    }

    public void SetBoolParameters(bool value)
    {
        foreach (AnimatorControllerParameter parameter in animator.parameters)
        {
            if (parameter.type != AnimatorControllerParameterType.Bool) continue;
            animator.SetBool(parameter.name, value);
        }
    }

    public void SetTriggerParameters(string triggerName)
    {
        animator.SetTrigger(triggerName);
        SetBoolParameters(false);
    }
}
