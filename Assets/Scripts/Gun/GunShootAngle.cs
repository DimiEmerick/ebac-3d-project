using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShootAngle : GunBase
{
    public int amountPerShot = 4;
    public float angle = 15f;

    public override void Shoot()
    {
        for(int i = 0; i < amountPerShot; i++)
        {
            var projectile = Instantiate(prefabProjectile);
            projectile.transform.position = positionToShoot.position;
            projectile.transform.eulerAngles = Vector3.zero + Vector3.up * (i % 2 == 0 ? angle : -angle);
        }
        
    }
}
