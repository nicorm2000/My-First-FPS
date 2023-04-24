using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    protected Transform cam;

    [Header("General Stats")]
    [SerializeField] float range = 50f;
    [SerializeField] float damage = 10f;
    [SerializeField] int maxAmmo;
    protected int currentAmmo;

    [SerializeField] protected float fireRate = 5f;
    [SerializeField] float reloadTime;
    WaitForSeconds reloadWait;

    [SerializeField] float inaccuracyDistance = 5f;

    [Header("Debugger")]
    [SerializeField] bool displayLaser = true;
    [SerializeField] GameObject laser;
    [SerializeField] Transform muzzle;
    [SerializeField] float fadeDuration = 0.3f;

    protected virtual void Awake()
    {
        cam = Camera.main.transform;

        reloadWait = new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
    }

    public virtual void Shoot()//Lets the gun shoot and checks if it is or not a shotgun so the bullets are different
    {

        if (TryToShoot())
        {
            UpdateAmmo();
        }
    }

    protected void UpdateAmmo()
    {
        currentAmmo--;
    }

    public bool TryToShoot()//The logic behind the shooting, it is made into a function because it is used more than once
    {
        RaycastHit hit;

        Vector3 shootingDir = GetShootingDirection();

        if (Physics.Raycast(cam.position, shootingDir, out hit, range))
        {
            if (hit.collider.GetComponent<Damageable>() != null)
            {
                hit.collider.GetComponent<Damageable>().TakeDamge(damage, hit.point, hit.normal);
            }
            if (displayLaser)
            {
                CreateLaser(hit.point);
            }
            return true;
        }
        else
        {
            if (displayLaser)
            {
                CreateLaser(cam.position + shootingDir * range);
            }
            return false;
        }
    }
    
    public virtual IEnumerator ShootCoroutine()//Coroutine that checks which fire type is being used
    {
        if (CanShoot())
        {
            Shoot();
        }
        else
        {
            StartCoroutine(Reload());
        }
        yield break;
    }

    protected IEnumerator Reload()//Coroutine that reloads the weapon
    {
        if (currentAmmo == maxAmmo)
        {
            yield break;
        }

        Debug.Log("Reloading...");

        yield return reloadWait;

        currentAmmo = maxAmmo;

        Debug.Log("Weapon Reloaded");
    }

    protected bool CanShoot()//Checks if the weapon can shoot based on the bullets remaining
    {
        bool enoughAmmo = currentAmmo > 0;

        return enoughAmmo;
    }

    Vector3 GetShootingDirection()//Gets the direction of where we are shooting, it also calculate how accurate the shooting will be
    {
        Vector3 targetPos = cam.position + cam.forward * range;

        targetPos = new Vector3(
            targetPos.x + Random.Range(-inaccuracyDistance, inaccuracyDistance), 
            targetPos.y + Random.Range(-inaccuracyDistance, inaccuracyDistance), 
            targetPos.z + Random.Range(-inaccuracyDistance, inaccuracyDistance)
            );

        Vector3 direction = targetPos - cam.position;

        return direction.normalized;
    }

    void CreateLaser(Vector3 end)//Creates the laser
    {
        LineRenderer lr = Instantiate(laser).GetComponent<LineRenderer>();

        lr.SetPositions(new Vector3[2] { muzzle.position, end });

        StartCoroutine(FadeLaser(lr));
    }

    // <summary>
    // Makes the laser trail fade away
    IEnumerator FadeLaser(LineRenderer lr)
    {
        float alpha = 1f;

        while (alpha > 0)
        {
            alpha -= Time.deltaTime / fadeDuration;

            lr.startColor = new Color(lr.startColor.r, lr.startColor.g, lr.startColor.b, alpha);

            lr.endColor = new Color(lr.endColor.r, lr.endColor.g, lr.endColor.b, alpha);

            yield return null;
        }
    }
}
