using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyPlateController : ItemController
{
    public Collider2D unlockCollider;
    private bool justUsed = false;

    public EmptyPlateController(PlayerController playerController) : base(playerController) { }

    private void Update()
    {
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
            Debug.Log("Using the empty plate on the food!");

            justUsed = true;
            unlockCollider.isTrigger = false;
            unlockCollider.enabled = true;

            currentCooldown = cooldown;
        }
    }
}
