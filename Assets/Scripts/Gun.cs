using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    Transform cam;

    [SerializeField] bool rapidFire = false;

    [SerializeField] float range = 50f;
    [SerializeField] float damage = 10f;

    [SerializeField] float fireRate = 5f;

    WaitForSeconds rapidFireWait;

    [SerializeField] int maxAmmo;
    int currentAmmo;

    [SerializeField] float reloadTime;
    WaitForSeconds reloadWait;

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

        RaycastHit hit;

        if (Physics.Raycast(cam.position, cam.forward, out hit, range))
        {
            if (hit.collider.GetComponent<Damageable>() != null)
            {
                hit.collider.GetComponent<Damageable>().TakeDamge(damage, hit.point, hit.normal);
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
