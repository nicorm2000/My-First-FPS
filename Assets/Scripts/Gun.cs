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

    private void Awake()
    {
        cam = Camera.main.transform;
        rapidFireWait = new WaitForSeconds(1 / fireRate);
    }

    public void Shoot()
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

    public IEnumerator RapidFire()
    {
        bool rapidFireBool = true;

        if (rapidFire)
        {
            while (rapidFireBool)
            {
                Shoot();

                yield return rapidFireWait;
            }
        }
        else
        {
            Shoot();

            yield return null;
        }
    }
}
