using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostStateWalking : GhostState
{

    public GhostStateWalking(GhostController ghostController) : base(ghostController) { }

    public override void OnStateEnter()
    {
        ghostController.damageTriggerCollider.enabled = false;
        totalStateTime = Random.Range(ghostController.minWalkTime, ghostController.maxWalkTime);
        currentStateTime = 0;

        if (ghostController.hasWalkedBefore)
        {
            useFading = true;
            ghostController.transform.position = GetWalkingStartPosition();
        }

        ghostController.targetPosition = GetNewTargetPosition();
        ghostController.animator.SetTrigger("Walk");

        SoundBank.PlayAudioClip(SoundBank.GetInstance().ghostSpawnAudioClips, ghostController.audioSource);

    }

    public override void OnStateExit()
    {
        ghostController.hasWalkedBefore = true;
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

    private Vector3 GetNewTargetPosition()
    {
        //Vector3 targetPosition = ghostController.transform.position;
        //Vector3 ghostToPlayer = ghostController.playerController.transform.position - ghostController.transform.position;
        //targetPosition += new Vector3(ghostToPlayer.x * Random.Range(-0.5f, 1.5f), ghostToPlayer.y * Random.Range(-0.5f, 1.5f));
        //return targetPosition;

        int signX = Random.Range(0, 2) == 0 ? 1 : -1;
        int signY = Random.Range(0, 2) == 0 ? 1 : -1;

        return new Vector3(ghostController.playerController.transform.position.x, ghostController.playerController.transform.position.y) + new Vector3(Random.Range(3f, 5f) * signX, Random.Range(3f, 5f) * signY);
    }

    private Vector3 GetWalkingStartPosition()
    {
        int signX = Random.Range(0, 2) == 0 ? 1 : -1;
        int signY = Random.Range(0, 2) == 0 ? 1 : -1;

        return new Vector3(ghostController.playerController.transform.position.x, ghostController.playerController.transform.position.y) + new Vector3(Random.Range(3f, 5f) * signX, Random.Range(3f, 5f) * signY);
    }
}
