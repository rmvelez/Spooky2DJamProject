using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateAboutToDie : BossState
{
    public BossStateAboutToDie(BossController bossController) : base(bossController)
    {

    }

    public override void OnStateEnter()
    {
        bossController.animator.SetTrigger("AboutToDie");
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
