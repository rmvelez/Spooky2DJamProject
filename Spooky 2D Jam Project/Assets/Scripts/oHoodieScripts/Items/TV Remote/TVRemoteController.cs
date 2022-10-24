using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVRemoteController : ItemController
{
    public Collider2D unlockCollider;
    private bool justUsed = false;


    public TVRemoteController(PlayerController playerController) : base(playerController) { }


    protected override void Update()
    {
        base.Update();

        if (currentCooldown <= 0.1f && justUsed)
        {
            unlockCollider.isTrigger = true;
            unlockCollider.enabled = false;
            justUsed = false;
        }
    }


    public override void Use()
    {
        base.Use();

        if (currentCooldown <= 0)
        {
            Debug.Log("Using the TV Remote!");

            // trigger TV
            justUsed = true;
            unlockCollider.isTrigger = false;
            unlockCollider.enabled = true;

            currentCooldown = cooldown;

            SoundBank.PlayAudioClip(SoundBank.GetInstance().skeletonAttackAudioClips, audioSource);

        }
    }
}
