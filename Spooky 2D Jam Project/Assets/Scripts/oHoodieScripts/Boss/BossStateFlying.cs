using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateFlying : BossState
{
    private Vector3 direction;
    private Vector3 targetPosition;

    public BossStateFlying(BossController bossController) : base(bossController)
    {

    }

    public override void OnStateEnter()
    {
        bossController.animator.SetTrigger("Fly");
        targetPosition = new Vector3(Random.Range(bossController.minRoomX, bossController.maxRoomX), Random.Range(bossController.minRoomY, bossController.maxRoomY));
        direction = (targetPosition - new Vector3(bossController.transform.position.x, bossController.transform.position.y)).normalized ;
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateFixedUpdate()
    {
    }

    public override void OnStateUpdate()
    {
        bossController.animator.SetTrigger("Fly");


        Vector3 moveVector = direction * Time.deltaTime * bossController.flyingSpeed;
        // Fly
        if (moveVector.magnitude < Vector3.Distance(bossController.transform.position, targetPosition))
        {
            bossController.transform.position = bossController.transform.position + moveVector;

        }
        // Arrive at destination
        else
        {
            bossController.transform.position = targetPosition;
            bossController.ChangeBossState(new BossStateLanding(bossController));
        }
    }
}
