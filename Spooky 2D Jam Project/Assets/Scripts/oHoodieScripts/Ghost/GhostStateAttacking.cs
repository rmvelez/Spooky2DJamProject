using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostStateAttacking : GhostState
{

    private bool isPausing = false;
    private float pauseTime;


    public GhostStateAttacking(GhostController ghostController) : base(ghostController) { }

    public override void OnStateEnter()
    {
        pauseTime = Random.Range(1f, 2f);
        moveSpeed = ghostController.moveSpeedInAttackState;
        useFading = true;

        ghostController.transform.position = GetAttackStartPosition();
        ghostController.targetPosition = GetTargetPosition();

        ghostController.fadeState = GhostController.FadeState.FadingIn;
        ghostController.animator.SetTrigger("Attack");

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
            pauseTime -= Time.deltaTime;

            if (pauseTime <= 0)
            {
                // 33% chance to Walk
                if (Random.Range(0, 3) > 1)
                {
                    ghostController.ChangeGhostState(new GhostStateWalking(ghostController));
                }
                // 66% Chance to attack again
                else
                {
                    ghostController.ChangeGhostState(new GhostStateAttacking(ghostController));
                }
            }
        }
    }


    private Vector3 GetAttackStartPosition()
    {
        return new Vector3(ghostController.playerController.transform.position.x, ghostController.playerController.transform.position.y) + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * ghostController.appearDistance;
    }

    private Vector3 GetTargetPosition()
    {
        return ghostController.transform.position + (ghostController.playerController.transform.position - ghostController.transform.position) * 1.5f;
    }
}
