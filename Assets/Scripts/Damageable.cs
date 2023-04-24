using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] float maxHealth = 100;
    float currentHealth;

    [SerializeField] float respawnTime = 5f;

    [SerializeField] GameObject hitEffect;

    [Header("Hitboxes")]
    public Collider headHitbox;
    public Collider neckHitbox;
    public Collider chestHitbox;
    public Collider bodyHitbox;

    public enum HitboxType
    {
        Head,
        Neck,
        Chest,
        Body
    }

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamge(float damage, Vector3 hitPos, Vector3 hitNormal)
    {
        float actualDamage = damage;

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

        gameObject.SetActive(false);

        Invoke("Respawn", respawnTime);
    }

    private void Respawn()
    {
        currentHealth = maxHealth;

        gameObject.SetActive(true);
    }
}
