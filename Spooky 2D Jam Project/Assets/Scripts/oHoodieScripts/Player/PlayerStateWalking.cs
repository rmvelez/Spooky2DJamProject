using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateWalking : PlayerState
{
    public PlayerStateWalking(PlayerController playerController) : base(playerController)
    {

    }

    public override void OnStateEnter()
    {
        allowDashing = true;
        allowShooting = true;
    }

    public override void OnStateExit()
    {
        playerController.rb.velocity = Vector2.zero;
    }

    public override void OnStateFixedUpdate()
    {
    }

    public override void OnStateUpdate()
    {
        // Move
        Vector2 moveVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (moveVector != Vector2.zero)
        {
            playerController.rb.velocity = moveVector * playerController.moveSpeed;
        }


        // Transition to other states

        // Walking -> Idle
        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0) playerController.ChangePlayerState(new PlayerStateIdle(playerController));
    }
}
