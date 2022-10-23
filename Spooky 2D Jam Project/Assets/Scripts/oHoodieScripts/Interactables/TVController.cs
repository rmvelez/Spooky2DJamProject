using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVController : InteractableController
{
    [SerializeField] private List<GhostController> disabledGhosts;

    private bool ghostsHaveSpawned = false;

    public override void Interact(ItemController itemController)
    {
        if (ghostsHaveSpawned) return;

        foreach (GhostController ghost in disabledGhosts)
        {
            ghost.gameObject.SetActive(true);
        }
        SoundBank.PlayAudioClip(SoundBank.GetInstance().TVTunOnAudioClips, audioSource);
        ghostsHaveSpawned = true;
    }

}