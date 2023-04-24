using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapidFire : Gun
{
    [Header("Rapid Fire")]
    WaitForSeconds rapidFireWait;

    private void Awake()
    {
        rapidFireWait = new WaitForSeconds(1 / fireRate);
    }

    public override IEnumerator ShootCoroutine()
    {
        while (CanShoot())
        {
            yield return rapidFireWait;

            Shoot();
        }

        yield return Reload();
    }
}
