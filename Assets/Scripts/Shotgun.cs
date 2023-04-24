using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
    [Header("ShotGun")]
    [SerializeField] int bulletsPerShot = 6;

    public override void Shoot()
    {
        UpdateAmmo();

        for (int i = 0; i < bulletsPerShot; i++)
        {
            TryToShoot();
        }
    }
}
