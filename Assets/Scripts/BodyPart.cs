using System;
using UnityEngine;

public class BodyPart : MonoBehaviour, IDamageable
{
    [SerializeField] new Collider collider;
    [SerializeField] float damageMultiplier = 0f;

    public float DamageMultiplier => damageMultiplier;

    //Stores methods and choose which parameters to use
    private Action<float, Vector3, Vector3> takeDamageEffects = null;
    private Action<float> takeDamage = null;

    public void Init(Action<float, Vector3, Vector3> takeDamageEffects, Action<float> takeDamage)
    {
        this.takeDamageEffects = takeDamageEffects;
        this.takeDamage = takeDamage;
    }

    public void TakeDamage(float damage, Vector3 hitPos, Vector3 hitNormal)
    {
        takeDamageEffects.Invoke(damage * damageMultiplier, hitPos, hitNormal);
    }

    public void TakeDamage(float damage)
    {
        takeDamage.Invoke(damage * damageMultiplier);
    }
}
