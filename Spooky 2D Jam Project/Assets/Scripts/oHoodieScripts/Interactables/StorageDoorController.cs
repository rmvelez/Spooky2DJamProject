using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageDoorController : InteractableController
{
    Collider2D doorCollider;

    [SerializeField] 
    public override void Interact(ItemController usedItem)
    {
        Debug.Log($"Opened Storage Door with Key!");
        PlayerController.GetInstance().RemoveItemFromInventory(usedItem.baseItem);

        Destroy(this.gameObject);
    }
}
