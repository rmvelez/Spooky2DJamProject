using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostStateDying : GhostState
{
    public GhostStateDying(GhostController ghostController) : base(ghostController) { }

    public override void OnStateEnter()
    {
        moveSpeed = 0;
        useFading = false;
        SoundBank.PlayAudioClip(SoundBank.GetInstance().ghostDieAudioClips, ghostController.audioSource);
        ghostController.animator.SetTrigger("Die");

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
