using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    Transform cam;

    [SerializeField] float range = 50f;
    [SerializeField] float damage = 10f;

    [SerializeField] float fireRate = 5f;

    private void Awake()
    {
        cam = Camera.main.transform;
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

        while (rapidFireBool)
        {
            Shoot();

            yield return new WaitForSeconds(1 / fireRate);
        }
    }
}
