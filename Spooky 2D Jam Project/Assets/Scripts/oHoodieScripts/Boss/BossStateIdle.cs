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
            int nextMove = Random.Range(0, 2);

            // Spike Attack
            if (nextMove == 0)
            {
                bossController.ChangeBossState(new BossStateSpikeAttack(bossController));
            }
            // Invoke enemy
            else if (nextMove == 1)
            {
                bossController.ChangeBossState(new BossStateInvoking(bossController));
            }
        }
    }
}
