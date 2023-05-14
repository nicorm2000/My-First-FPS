using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatedBullets : Gun
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletSpeed = 10f;

    public override bool BulletShoot()
    {
        //Turn off dummy and turn on when reloading
        //When the dummy is off replace with the instantiat and when reloading place the fake one

        GameObject bulletGO = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);

        Bullet bullet = bulletGO.GetComponent<Bullet>();

        bullet.Init(effectsHolder, damage);

        Rigidbody bulletRigidbody = bulletGO.GetComponent<Rigidbody>();

        bulletRigidbody.AddForce(muzzle.forward * bulletSpeed, ForceMode.Impulse);

        return true;
    }
}
