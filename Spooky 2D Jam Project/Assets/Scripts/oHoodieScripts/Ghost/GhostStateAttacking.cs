using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostStateAttacking : GhostState
{

    public GhostStateAttacking(GhostController ghostController) : base(ghostController) { }

    public override void OnStateEnter()
    {
        totalStateTime = Random.Range(ghostController.minAttackTime, ghostController.maxAttackTime);
        currentStateTime = 0;

        useFading = true;

        ghostController.transform.position = GetAttackStartPosition();
        ghostController.targetPosition = GetTargetPosition();

        ghostController.animator.SetTrigger("Attack");
        ghostController.damageTriggerCollider.enabled = true;

        SoundBank.PlayAudioClip(SoundBank.GetInstance().ghostAttackAudioClips, ghostController.audioSource);

    }

    public override void OnStateExit()
    {
        ghostController.damageTriggerCollider.enabled = false;

    }

    public override void OnStateFixedUpdate()
    {
    }

    public override void OnStateUpdate()
    {
        currentStateTime += Time.deltaTime;

        // Transition to new State 
        if (currentStateTime >= totalStateTime)
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


    private Vector3 GetAttackStartPosition()
    {
        return new Vector3(ghostController.playerController.transform.position.x, ghostController.playerController.transform.position.y) + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * ghostController.appearDistance;
    }

    private Vector3 GetTargetPosition()
    {
        return ghostController.transform.position + (ghostController.playerController.transform.position - ghostController.transform.position) * 2.5f;
    }
}
