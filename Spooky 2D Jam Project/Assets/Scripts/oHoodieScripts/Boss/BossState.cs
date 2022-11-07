using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossState
{
    protected BossController bossController;

    public BossState(BossController bossController)
    {
        this.bossController = bossController;
    }

    public abstract void OnStateEnter();
    public abstract void OnStateExit();
    public abstract void OnStateUpdate();
    public abstract void OnStateFixedUpdate();
}
