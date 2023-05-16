using System;
using UnityEngine;

public class BodyPart : MonoBehaviour, IDamageable
{
    [SerializeField] new Collider collider;
    [SerializeField] float damageMultiplier = 0f;
    [SerializeField] int scoreMultiplier = 0;
    [SerializeField] int score = 1;

    public float DamageMultiplier => damageMultiplier;
    public int ScoreMultiplier => scoreMultiplier;

    //Stores methods and choose which parameters to use
    private Action<float, RaycastHit, int> takeDamageEffects = null;
    private Action<float, int> takeDamage = null;

    public void Init(Action<float, RaycastHit, int> takeDamageEffects, Action<float, int> takeDamage)
    {
        this.takeDamageEffects = takeDamageEffects;
        this.takeDamage = takeDamage;
    }

    public void TakeDamage(float damage, RaycastHit hit)
    {
        takeDamageEffects?.Invoke(damage * damageMultiplier, hit, score * scoreMultiplier);
    }

    public void TakeDamage(float damage)
    {
        takeDamage?.Invoke(damage * damageMultiplier, score * scoreMultiplier);
    }
}
