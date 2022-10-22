using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostStateWalking : GhostState
{

    private bool isPausing = false;
    private float pauseTime;

    public GhostStateWalking(GhostController ghostController) : base(ghostController) { }

    public override void OnStateEnter()
    {
        pauseTime = Random.Range(1f, 2f);
        moveSpeed = ghostController.MoveSpeedInWalkState;
        useFading = true;
        ghostController.animator.SetTrigger("Walk");

        ghostController.targetPosition = GetNewTargetPosition();

        if (ghostController.fadeState == GhostController.FadeState.Invisible)
        {
            ghostController.fadeState = GhostController.FadeState.FadingIn;
        }
    }

    public override void OnStateExit()
    {
        ghostController.fadeState = GhostController.FadeState.Invisible;
    }

    public override void OnStateFixedUpdate()
    {
    }

    public override void OnStateUpdate()
    {
        if (!isPausing)
        {
            // Transition to new State 
            if (Vector3.Distance(ghostController.transform.position, ghostController.targetPosition) == 0)
            {
                isPausing = true;
                moveSpeed = 0;
            }
        }
        else
        {
            ghostController.fadeState = GhostController.FadeState.Invisible;
            pauseTime -= Time.deltaTime;

            if(pauseTime <= 0)
            {
                // 33% Chance to walk again
                if (Random.Range(0, 3) > 1)
                {
                    ghostController.ChangeGhostState(new GhostStateWalking(ghostController));
                }
                // 66% chance to attack
                else
                {
                    ghostController.ChangeGhostState(new GhostStateAttacking(ghostController));
                }
            }
        }

    }

    private Vector3 GetNewTargetPosition()
    {
        Vector3 targetPosition = ghostController.transform.position;
        Vector3 ghostToPlayer = ghostController.playerController.transform.position - ghostController.transform.position;
        targetPosition += new Vector3(ghostToPlayer.x * Random.Range(-0.5f, 1.5f), ghostToPlayer.y * Random.Range(-0.5f, 1.5f));
        return targetPosition;
    }
}
