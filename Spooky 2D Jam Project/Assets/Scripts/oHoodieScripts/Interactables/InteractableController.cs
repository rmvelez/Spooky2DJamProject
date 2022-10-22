using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractableController : MonoBehaviour
{
    public BaseItem itemNeededForInteraction;
    public Collider2D interactTriggerCollider;

    public virtual void Interact()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        ItemController usedItem = collision.gameObject.GetComponent<ItemController>();
        if(usedItem != null && usedItem.baseItem == itemNeededForInteraction)
        {
            Interact();
        }
    }
}
