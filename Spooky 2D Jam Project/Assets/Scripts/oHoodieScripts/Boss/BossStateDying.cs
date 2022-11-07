using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateDying : BossState
{
    public BossStateDying(BossController bossController) : base(bossController)
    {

    }

    public override void OnStateEnter()
    {
        bossController.animator.SetTrigger("Die");
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateFixedUpdate()
    {
    }

    public override void OnStateUpdate()
    {
    }
}
