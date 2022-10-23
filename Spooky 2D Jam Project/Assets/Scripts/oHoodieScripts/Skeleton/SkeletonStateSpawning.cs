using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStateSpawning : SkeletonState
{
    public SkeletonStateSpawning(SkeletonController skeletonController) : base(skeletonController) { }

    public override void OnStateEnter()
    {
        skeletonController.collider.enabled = true;
        skeletonController.animator.SetTrigger("Spawn");


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
