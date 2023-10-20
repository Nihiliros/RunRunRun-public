using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateForm : BaseForm
{

    protected override void Start()
    {
        base.Start();
        canSideMove = true;
        canJump = true;
        canAction = false;
        canUltimate = false;
        canTakeDamage = false;
        hasUltimate = false;
    }

    public override void Ultimate()
    {
    }
}
