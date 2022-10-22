using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]

public abstract class ItemController : MonoBehaviour
{
    public BaseItem baseItem;
    public float cooldown;
    
    public SpriteRenderer spriteRenderer;


    private PlayerController playerController;
    protected PlayerController PlayerController
    {
        get
        {
            if(playerController == null) playerController = PlayerController.GetInstance();
            return playerController;
        }
    }

    protected float currentCooldown;

    public ItemController(PlayerController playerController)
    {
        this.playerController = playerController;
    }


    void Start()
    {
        if (baseItem != null && !IsEquipped()) spriteRenderer.sprite = baseItem.inventoryIcon;
    }

    void Update()
    {
        currentCooldown -= Time.deltaTime;
    }

    /// <summary>
    /// Use the item (in direction of mouse or right joystick)
    /// What happens is determined by the class implementing the method
    /// </summary>
    public virtual void Use()
    {
        if(currentCooldown > 0)
        {
            //Debug.Log($"{baseItem.name} is on cooldown for {currentCooldown} more seconds.");
            return;
        }
    }


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
            PlayerController.PickUpItem(baseItem);
            Destroy(this.gameObject);
        }

    }

    protected bool IsEquipped()
    {
        if (PlayerController == null) return false;
        return PlayerController.equippedItem != null && PlayerController.equippedItem == this;
    }

    /// <summary>
    /// Change to the rotatable ingame icon
    /// </summary>
    public void Equip()
    {
        spriteRenderer.sprite = baseItem.ingameIcon;
        GetComponent<Collider2D>().enabled = false;
    }
}


