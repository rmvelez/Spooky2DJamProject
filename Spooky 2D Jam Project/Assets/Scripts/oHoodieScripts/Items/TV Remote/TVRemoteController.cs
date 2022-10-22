using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVRemoteController : ItemController
{
    public TVRemoteController(PlayerController playerController) : base(playerController) { }

    public override void Use()
    {
        Debug.Log("Used TV Remote!");
    }
}