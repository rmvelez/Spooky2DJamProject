using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateDying : PlayerState
{

    public PlayerStateDying(PlayerController playerController) : base(playerController)
    {

    }

    public override void OnStateEnter()
    {
        allowDashing = false;
        allowItemUse = false;
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
