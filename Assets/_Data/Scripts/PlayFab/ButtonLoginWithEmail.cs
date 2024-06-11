using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLoginWithEmail : ButtonLogin
{
    protected override void LoginPlayFab()
    {
        PlayFabController.Instance.LoginWithEmail();
    }
}
