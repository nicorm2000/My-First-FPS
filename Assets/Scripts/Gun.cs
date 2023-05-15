using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    protected Transform cam;

    [Header("General Stats")]
    [SerializeField] bool instantiatesBullets = true;
    [SerializeField] float range = 50f;
    [SerializeField] protected float damage = 10f;
    [SerializeField] int maxAmmo;
    protected int currentAmmo;

    [SerializeField] protected float fireRate = 5f;
    [SerializeField] float reloadTime;
    WaitForSeconds reloadWait;

    [SerializeField] float inaccuracyDistance = 5f;

    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] float hitEffectDuration;

    [Header("UI Settings")]
    [SerializeField] private TextMeshProUGUI ammoText;

    [Header("Debugger")]
    [SerializeField] bool displayLaser = true;
    [SerializeField] GameObject laser;
    [SerializeField] protected Transform muzzle;
    [SerializeField] float fadeDuration = 0.3f;
    [SerializeField] float weaponOffset;
    private bool isDropped;
    private bool isCurrentWeapon;
    private bool isReloading;
    private GameObject newParent;
    [SerializeField] GameObject positionGun;
    [SerializeField] Transform originParent;
    [SerializeField] protected Transform effectsHolder;

    protected virtual void Awake()
    {
        cam = Camera.main.transform;

        reloadWait = new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
    }

    private void UpdateAmmoText()
    {
        if (!isDropped)
        {
            ammoText.text = "" + currentAmmo;
        }
        else
        {
            ammoText.text = "";
        }
    }

    public virtual void Shoot()//Lets the gun shoot and checks if it is or not a shotgun so the bullets are different
    {
        if (instantiatesBullets)
        {
            if (BulletShoot())
            {
                UpdateAmmo();
            }
        }
        else if (!instantiatesBullets)
        {
            if (RayCastShoot()) 
            { 
                UpdateAmmo();
            }
        }
    }

    protected void UpdateAmmo()
    {
        currentAmmo--;
    }

    public virtual bool BulletShoot()
    {
        return true;
    }

    public bool RayCastShoot()//The logic behind the shooting, it is made into a function because it is used more than once
    {
        GameObject effect = Instantiate(hitEffect.gameObject, effectsHolder.position, Quaternion.identity, muzzle);

        effect.transform.position = muzzle.transform.position;

        RaycastHit hit;

        Vector3 shootingDir = GetShootingDirection();

        if (Physics.Raycast(cam.position, shootingDir, out hit, range))
        {
            IDamageable auxDamageable = hit.collider.GetComponent<IDamageable>();

            if (auxDamageable != null)
            {
                auxDamageable.TakeDamage(damage, hit.point, hit.normal);
            }
            if (displayLaser)
            {
                CreateLaser(hit.point);
            }

            Destroy(effect, hitEffectDuration);

            return true;
        }
        else
        {
            if (displayLaser)
            {
                CreateLaser(cam.position + shootingDir * range);
            }

            Destroy(effect, hitEffectDuration);

            return false;
        }
    }
    
    public virtual IEnumerator ShootCoroutine()//Coroutine that checks which fire type is being used
    {
        if (!isReloading)
        {
            if (CanShoot())
            {
                Shoot();
            }
            else
            {
                OnReload();
            }
            yield break;
        }

        UpdateAmmoText();
    }

    protected IEnumerator OnReload()//Coroutine that reloads the weapon
    {
        isReloading = true;

        if (currentAmmo == maxAmmo)
        {
            yield break;
        }

        Debug.Log("Reloading...");

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;

        Debug.Log("Weapon Reloaded");

        isReloading = false;
    }

    protected bool CanShoot()//Checks if the weapon can shoot based on the bullets remaining
    {
        return currentAmmo > 0;
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

    public void OnDropWeapon()
    {
        UpdateAmmoText();

        if (isDropped)
        {
            return;
        }

        isDropped = true;

        if (!isCurrentWeapon) return;
      
        if (newParent == null)
        {
            newParent = new GameObject("Weapon Place Holder");
        }

        transform.parent = null;

        transform.parent = newParent.transform;

        Rigidbody weaponRB = gameObject.AddComponent<Rigidbody>();
        weaponRB.useGravity = true;
        weaponRB.isKinematic = false;

        GetComponent<BoxCollider>().enabled = true;
        FindObjectOfType<InputManager>().gun = null;
        isCurrentWeapon = false;
    }

    public void OnPickWeapon()
    {
        UpdateAmmoText();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject existingWeapon = FindWeaponInChildren(player.transform);

        if (existingWeapon != null)
        {
            return;
        }

        float pickUpRadius = 2f;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, pickUpRadius);

        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.tag == "Player")
            {
                isDropped = false;

                if (newParent != null)
                {
                    Destroy(newParent);
                }

                transform.SetParent(originParent);

                Rigidbody weaponRB = gameObject.GetComponent<Rigidbody>();

                if (weaponRB != null)
                {
                    Destroy(weaponRB);
                }

                GetComponent<PlayerInput>().enabled = true;
                GetComponent<BoxCollider>().enabled = false;

                transform.rotation = positionGun.transform.rotation;
                transform.localPosition = positionGun.transform.localPosition + new Vector3(0f, 0f, weaponOffset);

                FindObjectOfType<InputManager>().gun = this;
                isCurrentWeapon = true;
            }
        }
    }

    private GameObject FindWeaponInChildren(Transform parent)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);

            if (child.CompareTag("Weapon"))
            {
                return child.gameObject;
            }
            else
            {
                GameObject found = FindWeaponInChildren(child);

                if (found != null)
                {
                    return found;
                }
            }
        }

        return null;
    }
}
