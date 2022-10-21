using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateDashing : PlayerState
{
    private Vector2 dashVector;
    private float dashTimeLeft;

    public PlayerStateDashing(PlayerController playerController) : base(playerController)
    {

    }

    public override void OnStateEnter()
    {
        allowDashing = false;
        allowShooting = false;

        dashVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * playerController.dashSpeed;
        dashTimeLeft = playerController.dashDuration;
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
        dashTimeLeft -= Time.deltaTime;
        playerController.rb.velocity = dashVector;

        if(dashTimeLeft <= 0)
        {
            if((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)){
                playerController.ChangePlayerState(new PlayerStateWalking(playerController));
            }
            else{
                playerController.ChangePlayerState(new PlayerStateIdle(playerController));
            }
        }
    }

}
