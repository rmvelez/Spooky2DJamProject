using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStateBoneThrow : SkeletonState
{
    private int nrOfBonesThrown = 0;
    private int nrOfBonesToThrow;

    public SkeletonStateBoneThrow(SkeletonController skeletonController) : base(skeletonController) { }

    public override void OnStateEnter()
    {
        skeletonController.animator.SetTrigger("Throw");
        nrOfBonesToThrow = Random.Range(skeletonController.minBoneThrows, skeletonController.maxBoneThrows + 1);

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

    public void IncreaseBoneCounter()
    {
        nrOfBonesThrown++;

        if(nrOfBonesThrown >= nrOfBonesToThrow)
        {
            SkeletonState newState = null;
            int nr = Random.Range(0, 2);
            if (nr == 0) newState = new SkeletonStateWalking(skeletonController);
            if (nr == 1) newState = new SkeletonStateTeleOut(skeletonController);

            skeletonController.ChangeSkeletonState(newState);
        }
    }
}
