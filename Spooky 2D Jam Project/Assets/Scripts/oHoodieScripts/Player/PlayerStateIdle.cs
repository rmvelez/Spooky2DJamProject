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
        allowDashing = false;
        allowItemUse = true;
        mirrorLeftRight = false;
        
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateFixedUpdate()
    {
    }

    public override void OnStateUpdate()
    {
        playerController.animator.SetTrigger("IdleFront");

        // Idle -> Walking
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) playerController.ChangePlayerState(new PlayerStateWalking(playerController));
    }
}
