using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapidFire : Gun
{
    [Header("Rapid Fire")]
    [SerializeField] int bulletsPerBurst = 3;
    private float rapidFireWait;
    private bool isInRapidFire = false;

    protected override void Awake()
    {
        base.Awake();
        rapidFireWait = 1 / fireRate;
    }
    
    public override void CheckBeforeShoot()
    {
        if (FindObjectOfType<InputManager>().gun == null)
        {
            return;
        }

        if (isInRapidFire)
        {
            return;
        }

        StartCoroutine(FireRateCoroutine());
    }

    public IEnumerator FireRateCoroutine()
    {
        isInRapidFire = true;

        int actualShoot = 0;

        while (CanShoot() && actualShoot < bulletsPerBurst)
        {
            Shoot();

            actualShoot++;

            yield return new WaitForSeconds(rapidFireWait);
        }

        isInRapidFire = false;

        OnReload();
    }
}
