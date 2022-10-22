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
        allowItemUse = true;
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


        // Play the right anim

        // Side (when aiming to left/right)
        if(Mathf.Abs(playerController.aimVector.x) > Mathf.Abs(playerController.aimVector.y))
        {
            playerController.animator.SetTrigger("WalkSide");
        }
        // Back (when aiming up)
        else if (playerController.aimVector.y > Mathf.Abs(playerController.aimVector.x))
        {
            playerController.animator.SetTrigger("WalkBack");
        }
        // Front (when aiming down)
        else
        {
            playerController.animator.SetTrigger("WalkFront");
        }




        // Transition to other states

        // Walking -> Idle
        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0) playerController.ChangePlayerState(new PlayerStateIdle(playerController));
    }
}
