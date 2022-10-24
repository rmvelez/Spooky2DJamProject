using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVController : InteractableController
{
    [SerializeField] private GameObject lightObjectToActivate;
    [SerializeField] private Animator animator;
    [SerializeField] private List<GhostController> disabledGhosts;

    private bool ghostsHaveSpawned = false;

    public override void Interact(ItemController itemController)
    {
        if (ghostsHaveSpawned) return;

        PlayerController.GetInstance().RemoveItemFromInventory(itemController.baseItem);

        foreach (GhostController ghost in disabledGhosts)
        {
            ghost.gameObject.SetActive(true);
            ghost.Spawn();
        }
        SoundBank.PlayAudioClip(SoundBank.GetInstance().TVTunOnAudioClips, audioSource);
        ghostsHaveSpawned = true;
        lightObjectToActivate.SetActive(true);
        animator.SetTrigger("turnOn");

    }

}
