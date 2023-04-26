using System;
using UnityEngine;

public class BodyPart : MonoBehaviour, IDamageable
{
    [SerializeField] Collider collider = null;
    [SerializeField] float damageMultiplier = 0f;

    public float DamageMultiplier => damageMultiplier;

    //Stores methods and choose which parameters to use
    private Action<float, Vector3, Vector3> takeDamage = null;

    public void Init(Action<float, Vector3, Vector3> takeDamage)
    {
        this.takeDamage = takeDamage;
    }

    public void TakeDamage(float damage, Vector3 hitPos, Vector3 hitNormal)
    {
        takeDamage.Invoke(damage * damageMultiplier, hitPos, hitNormal);
    }
}
