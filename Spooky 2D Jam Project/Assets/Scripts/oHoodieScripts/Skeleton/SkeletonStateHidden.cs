using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStateHidden : SkeletonState
{
    public SkeletonStateHidden(SkeletonController skeletonController) : base(skeletonController) { }

    public override void OnStateEnter()
    {
        skeletonController.collider.enabled = false;
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateFixedUpdate()
    {
    }

    public override void OnStateUpdate()
    {
        if (Vector3.Distance(skeletonController.transform.position, skeletonController.playerController.transform.position) <= skeletonController.distanceToSpawn)
        {
            skeletonController.ChangeSkeletonState(new SkeletonStateSpawning(skeletonController));
        }
    }
}
