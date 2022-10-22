using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    protected PlayerController playerController;
    
    public bool allowItemUse;
    public bool allowDashing;
    public bool mirrorLeftRight;

    public PlayerState(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    public abstract void OnStateEnter();
    public abstract void OnStateExit();
    public abstract void OnStateUpdate();
    public abstract void OnStateFixedUpdate();
}
