using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStateTeleIn : SkeletonState
{
    
    public SkeletonStateTeleIn(SkeletonController skeletonController) : base(skeletonController) { }

    public override void OnStateEnter()
    {

        Vector3 targetPos = skeletonController.playerController.transform.position;
        float teleDist = Random.Range(skeletonController.minTeleDistanceFromPlayer, skeletonController.maxTeleDistanceFromPlayer);

        Vector2 randomDirection;
        RaycastHit2D hit;
        int tries = 0;

        do
        {
            randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            hit = Physics2D.Raycast(targetPos, randomDirection, teleDist, 10); // 10 = Player layer
            tries++;

            if (hit.collider == null) targetPos = targetPos + new Vector3(randomDirection.x, randomDirection.y, 0) * teleDist;
        } while (hit.collider != null && tries < 10);

        skeletonController.transform.position = targetPos;

        skeletonController.animator.SetTrigger("TeleIn");
        SoundBank.PlayAudioClip(SoundBank.GetInstance().skeletonSpawnAudioClips, skeletonController.audioSource);

    }

    public override void OnStateExit()
    {
    }

    public override void OnStateFixedUpdate()
    {
    }

    public override void OnStateUpdate()
    {

    }
}
