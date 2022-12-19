using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateIdle : BossState
{
    public BossStateIdle(BossController bossController) : base(bossController)
    {

    }

    public override void OnStateEnter()
    {
        bossController.animator.SetTrigger("Idle");
        
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateFixedUpdate()
    {
    }

    public override void OnStateUpdate()
    {
        if (bossController.isSpawned)
        {
            int nextMove = bossController.justLanded ? Random.Range(0, 4) : Random.Range(0, 6);

            // Invoke enemy 
            if (nextMove == 0)
            {
                bossController.ChangeBossState(new BossStateInvoking(bossController));
            }
            // Spike Attack 
            else if (nextMove <= 3)
            {
                bossController.ChangeBossState(new BossStateSpikeAttack(bossController));
            }
            // Move
            else
            {
                bossController.ChangeBossState(new BossStateAboutToFly(bossController));
            }

        }
    }
}
