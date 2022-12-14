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
        allowItemUse = false;
        mirrorLeftRight = true;

        dashVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * playerController.dashSpeed;
        dashTimeLeft = playerController.dashDuration;

        SoundBank.PlayAudioClip(SoundBank.GetInstance().dashAudioClips, playerController.audioSource);
    }

    public override void OnStateExit()
    {
        playerController.rb.velocity = Vector2.zero;
        playerController.currentDashCooldown = playerController.dashCooldown;
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
