using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkeletonState
{
    protected SkeletonController skeletonController;

    public float totalStateTime = 0;
    public float currentStateTime = 0;

    public SkeletonState(SkeletonController skeletonController)
    {
        this.skeletonController = skeletonController;
    }

    public abstract void OnStateEnter();
    public abstract void OnStateExit();
    public abstract void OnStateUpdate();
    public abstract void OnStateFixedUpdate();
}
