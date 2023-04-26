using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float maxHealth = 100;
    float currentHealth;

    [SerializeField] float respawnTime = 5f;

    [SerializeField] GameObject hitEffect;

    [SerializeField] BodyPart[] bodyParts = null;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Start()
    {
        for (int i = 0; i < bodyParts.Length; i++)
        {
            bodyParts[i].Init(TakeDamage);
        }
    }

    public void TakeDamage(float damage, Vector3 hitPos, Vector3 hitNormal)
    {
        //Creates the VFX for the shooting effect towards an object
        var hitParticleEffect = Instantiate(hitEffect, hitPos, Quaternion.LookRotation(hitNormal));

        //Makes the effect a child so if the object is destroyed the effect is destroyed as well
        hitParticleEffect.transform.parent = gameObject.transform;

        if (!(gameObject.tag == "Ground"))
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                Die();
            }
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
