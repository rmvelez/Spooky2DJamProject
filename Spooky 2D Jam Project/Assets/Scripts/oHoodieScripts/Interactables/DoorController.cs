using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DoorController : InteractableController
{
    [SerializeField] private Collider2D doorCollider;
    [SerializeField] private ShadowCaster2D shadowCaster;
    [SerializeField] private GameObject lockIcon;

    [SerializeField] 
    public override void Interact(ItemController usedItem)
    {
        Debug.Log($"Opened Storage Door with Key!");
        PlayerController.GetInstance().RemoveItemFromInventory(usedItem.baseItem);

        SoundBank.PlayAudioClip(SoundBank.GetInstance().DoorUnlockAudioClips, audioSource);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        shadowCaster.enabled = false;
        doorCollider.enabled = false;
        if(lockIcon != null) lockIcon.SetActive(false);
    }
}
