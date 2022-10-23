using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostStateSpawning : GhostState
{
    public GhostStateSpawning(GhostController ghostController) : base(ghostController) { }

    public override void OnStateEnter()
    {
        moveSpeed = 0;
        useFading = false;
        SoundBank.PlayAudioClip(SoundBank.GetInstance().ghostSpawnAudioClips, ghostController.audioSource);
        ghostController.light2D.enabled = true;
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateFixedUpdate()
    {
    }

    public override void OnStateUpdate()
    {
        ghostController.animator.SetTrigger("Spawn");
    }
}
