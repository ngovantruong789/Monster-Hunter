using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLoginWithDevice : ButtonLogin
{
    protected override void LoginPlayFab()
    {
        PlayFabController.Instance.LoginWithDeviceID();
    }
}
