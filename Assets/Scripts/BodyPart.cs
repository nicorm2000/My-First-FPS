using System;
using UnityEngine;

public class BodyPart : MonoBehaviour, IDamageable
{
    [SerializeField] new Collider collider;
    [SerializeField] float damageMultiplier = 0f;

    public float DamageMultiplier => damageMultiplier;

    //Stores methods and choose which parameters to use
    private Action<float, RaycastHit> takeDamageEffects = null;
    private Action<float> takeDamage = null;

    public void Init(Action<float, RaycastHit> takeDamageEffects, Action<float> takeDamage)
    {
        this.takeDamageEffects = takeDamageEffects;
        this.takeDamage = takeDamage;
    }

    public void TakeDamage(float damage, RaycastHit hit)
    {
        takeDamageEffects.Invoke(damage * damageMultiplier, hit);
    }

    public void TakeDamage(float damage)
    {
        takeDamage.Invoke(damage * damageMultiplier);
    }
}
