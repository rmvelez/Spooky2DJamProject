using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStateWalking : SkeletonState
{
    private Vector3 moveDirection;
    private float moveTime;
    private float currentMoveTime = 0;

    public SkeletonStateWalking(SkeletonController skeletonController) : base(skeletonController) { }

    public override void OnStateEnter()
    {
        skeletonController.animator.SetTrigger("Walk");
        moveTime = Random.Range(skeletonController.minMoveTime, skeletonController.maxMoveTime);
        moveDirection = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0).normalized;
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateFixedUpdate()
    {
    }

    public override void OnStateUpdate()
    {
        currentMoveTime += Time.deltaTime;
        skeletonController.rb.velocity = moveDirection * skeletonController.moveSpeed;

        if(currentMoveTime >= moveTime)
        {
            skeletonController.rb.velocity = Vector2.zero;
            skeletonController.ChangeSkeletonState(new SkeletonStateBoneThrow(skeletonController));
        }
    }
}
