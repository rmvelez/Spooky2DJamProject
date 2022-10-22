using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageKeyController : ItemController
{
    public StorageKeyController(PlayerController playerController) : base(playerController) { }

    public override void Use()
    {
        base.Use();

        if (cooldown <= 0)
        {
            // Open Door to storage

        }
    }
}
