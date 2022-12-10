using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateAboutToFly : BossState
{
    public BossStateAboutToFly(BossController bossController) : base(bossController)
    {

    }

    public override void OnStateEnter()
    {
        bossController.animator.SetTrigger("AboutToFly");
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateFixedUpdate()
    {
    }

    public override void OnStateUpdate()
    {
        bossController.animator.SetTrigger("AboutToFly");

    }
}
