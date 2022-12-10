using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateLanding : BossState
{
    public BossStateLanding(BossController bossController) : base(bossController)
    {

    }

    public override void OnStateEnter()
    {
        bossController.animator.SetTrigger("Landing");

    }

    public override void OnStateExit()
    {
    }

    public override void OnStateFixedUpdate()
    {
    }

    public override void OnStateUpdate()
    {
        bossController.animator.SetTrigger("Landing");

    }

}
