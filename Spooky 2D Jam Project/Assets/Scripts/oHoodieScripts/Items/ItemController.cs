using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]

public abstract class ItemController : MonoBehaviour
{
    public BaseItem baseItem;
    public SpriteRenderer spriteRenderer;


    private PlayerController playerController;

    public ItemController(PlayerController playerController)
    {
        this.playerController = playerController;
    }


    private void Start()
    {
        playerController = PlayerController.GetInstance();
        if (baseItem != null && !IsEquipped()) spriteRenderer.sprite = baseItem.inventoryIcon;
    }


    /// <summary>
    /// Use the item (in direction of mouse or right joystick)
    /// What happens is determined by the class implementing the method
    /// </summary>
    public abstract void Use();


    /// <summary>
    /// Pick the item up when walking over it
    /// </summary>
    /// <param name="collision"></param>
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsEquipped())
        {

        }
        else
        {
            playerController.PickUpItem(baseItem);
            Destroy(this.gameObject);
        }

    }

    protected bool IsEquipped()
    {
        if (playerController == null) return false;
        return playerController.equippedItem != null && playerController.equippedItem == this;
    }

    /// <summary>
    /// Change to the rotatable ingame icon
    /// </summary>
    public void Equip()
    {
        spriteRenderer.sprite = baseItem.ingameIcon;
    }
}


