using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateIdle : PlayerState
{
    public PlayerStateIdle(PlayerController playerController) : base(playerController)
    {

    }

    public override void OnStateEnter()
    {

    }

    public override void OnStateExit()
    {
    }

    public override void OnStateFixedUpdate()
    {
    }

    public override void OnStateUpdate()
    {
        // Idle -> Walking
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) playerController.ChangePlayerState(new PlayerStateWalking(playerController));
    }
}
