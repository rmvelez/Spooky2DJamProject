using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalGunController : ItemController
{
    public BulletController bulletPrefab;
    public Transform bulletSpawnPoint;

    public NormalGunController(PlayerController playerController) : base(playerController){}


    public override void Use()
    {
        base.Use();

        if(currentCooldown <= 0)
        {
            // Shoot
            BulletController bulletController = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.Euler(0,0,0));
            bulletController.Shoot(PlayerController.aimVector);
            currentCooldown = cooldown;
        }
        

    }

}
