using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageDoorController : InteractableController
{
    [SerializeField] private Collider2D doorCollider;

    [SerializeField] 
    public override void Interact(ItemController usedItem)
    {
        Debug.Log($"Opened Storage Door with Key!");
        PlayerController.GetInstance().RemoveItemFromInventory(usedItem.baseItem);

        SoundBank.PlayAudioClip(SoundBank.GetInstance().DoorUnlockAudioClips, audioSource);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        doorCollider.enabled = false;
    }
}
