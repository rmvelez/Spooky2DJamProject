using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GhostState
{
    protected GhostController ghostController;

    public float totalStateTime = 0;
    public float currentStateTime = 0;

    public float moveSpeed;
    public bool useFading;

    public GhostState(GhostController ghostController)
    {
        this.ghostController = ghostController;
    }

    public abstract void OnStateEnter();
    public abstract void OnStateExit();
    public abstract void OnStateUpdate();
    public abstract void OnStateFixedUpdate();
}
