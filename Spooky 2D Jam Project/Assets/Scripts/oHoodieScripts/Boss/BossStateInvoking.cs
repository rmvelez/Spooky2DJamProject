using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateInvoking : BossState
{
    public BossStateInvoking(BossController bossController) : base(bossController)
    {

    }

    public override void OnStateEnter()
    {
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateFixedUpdate()
    {
    }

    public override void OnStateUpdate()
    {
        bossController.animator.SetTrigger("Invoke");

    }
}
