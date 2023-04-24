using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] float maxHealth = 100;
    float currentHealth;

    [SerializeField] GameObject hitEffect;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamge(float damage, Vector3 hitPos, Vector3 hitNormal)
    {
        //Creates the VFX for the shooting effect towards an object
        var hitParticleEffect = Instantiate(hitEffect, hitPos, Quaternion.LookRotation(hitNormal));

        //Makes the effect a child so if the object is destroyed the effect is destroyed as well
        hitParticleEffect.transform.parent = gameObject.transform;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log(name + " was destroyed");

        Destroy(gameObject);
    }
}
