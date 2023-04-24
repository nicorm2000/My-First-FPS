using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    Transform cam;

    [Header("General Stats")]
    [SerializeField] float range = 50f;
    [SerializeField] float damage = 10f;
    [SerializeField] int maxAmmo;
    int currentAmmo;

    [SerializeField] float fireRate = 5f;
    [SerializeField] float reloadTime;
    WaitForSeconds reloadWait;

    [Header("Rapid Fire")]
    [SerializeField] bool rapidFire = false;
    WaitForSeconds rapidFireWait;

    [Header("ShotGun")]
    [SerializeField] bool shotgun = false;
    [SerializeField] int bulletsPerShot = 6;

    private void Awake()
    {
        cam = Camera.main.transform;
        rapidFireWait = new WaitForSeconds(1 / fireRate);
        reloadWait = new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
    }

    public void Shoot()
    {
        currentAmmo--;

        if (shotgun)
        {
            for (int i = 0; i < bulletsPerShot; i++)
            {
                RaycastHit hit;

                if (Physics.Raycast(cam.position, cam.forward, out hit, range))
                {
                    if (hit.collider.GetComponent<Damageable>() != null)
                    {
                        hit.collider.GetComponent<Damageable>().TakeDamge(damage, hit.point, hit.normal);
                    }
                }
            }
        }
        else
        {
            RaycastHit hit;

            if (Physics.Raycast(cam.position, cam.forward, out hit, range))
            {
                if (hit.collider.GetComponent<Damageable>() != null)
                {
                    hit.collider.GetComponent<Damageable>().TakeDamge(damage, hit.point, hit.normal);
                }
            }
        }
    }

    public IEnumerator RapidFire()
    {
        if (CanShoot())
        {
            Shoot();

            if (rapidFire)
            {
                while (CanShoot())
                {
                    yield return rapidFireWait;
                    
                    Shoot();
                }
                StartCoroutine(Reload());
            }
        }
        else
        {
            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    {
        if (currentAmmo == maxAmmo)
        {
            yield return null;
        }

        Debug.Log("Reloading...");

        yield return reloadWait;

        currentAmmo = maxAmmo;

        Debug.Log("Reloaded");
    }

    bool CanShoot()
    {
        bool enoughAmmo = currentAmmo > 0;
        return enoughAmmo;
    }
}
