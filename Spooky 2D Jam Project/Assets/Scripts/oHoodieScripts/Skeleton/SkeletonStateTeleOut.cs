using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStateTeleOut : SkeletonState
{

    public SkeletonStateTeleOut(SkeletonController skeletonController) : base(skeletonController) { }

    public override void OnStateEnter()
    {
        skeletonController.animator.SetTrigger("TeleOut");
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
