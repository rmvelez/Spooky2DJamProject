using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public abstract class InteractableController : MonoBehaviour
{
    public BaseItem itemNeededForInteraction;
    public Collider2D interactTriggerCollider;

    protected AudioSource audioSource;

    public abstract void Interact(ItemController itemController);

    void OnTriggerEnter2D(Collider2D collision)
    {
        ItemController usedItem = collision.gameObject.GetComponent<ItemController>();
        if(usedItem != null && usedItem.baseItem == itemNeededForInteraction)
        {
            Interact(usedItem);
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
}
