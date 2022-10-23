using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalPulleyController : ItemController
{
    public Collider2D unlockCollider;
    private bool justUsed = false;

    public MetalPulleyController(PlayerController playerController) : base(playerController) { }

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
            Debug.Log("Using the metal pulley on the bathtub!");

            justUsed = true;
            unlockCollider.isTrigger = false;
            unlockCollider.enabled = true;

            currentCooldown = cooldown;
        }
    }
}






