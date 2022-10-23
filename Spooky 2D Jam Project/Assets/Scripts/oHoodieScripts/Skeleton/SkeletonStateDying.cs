using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStateDying : SkeletonState
{
    public SkeletonStateDying(SkeletonController skeletonController) : base(skeletonController) { }

    public override void OnStateEnter()
    {
        skeletonController.animator.SetTrigger("Die");
        SoundBank.PlayAudioClip(SoundBank.GetInstance().skeletonDieAudioClips, skeletonController.audioSource);


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
