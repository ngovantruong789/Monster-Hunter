using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDamage : DamageSender
{
    protected override void LoadDamage()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SendDamage(collision.transform);
    }
}
