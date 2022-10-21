using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalGunController : ItemController
{
    public NormalGunController(PlayerController playerController) : base(playerController){}


    public override void Use()
    {
        Debug.Log("Gun was used!!!");
    }
}
