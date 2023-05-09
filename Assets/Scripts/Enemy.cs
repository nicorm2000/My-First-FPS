using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float maxHealth = 100;
    float currentHealth;

    [SerializeField] float respawnTime = 5f;

    [SerializeField] ParticleSystem hitEffectParent;
    [SerializeField] ParticleSystem hitEffect;

    [SerializeField] BodyPart[] bodyParts = null;

    private List<ParticleSystem> effects = new List<ParticleSystem>();

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
        ParticleSystem hitParticleEffect = Instantiate(hitEffect, hitPos, Quaternion.LookRotation(hitNormal), hitEffectParent.transform);

        effects.Add(hitParticleEffect);

        //Makes the effect a child so if the object is destroyed the effect is destroyed as well
        hitParticleEffect.transform.parent = hitEffectParent.transform;

        if (!(gameObject.tag == "Ground"))
        {
            currentHealth -= damage;

            Debug.Log(damage);

            if (currentHealth <= 0)
            {
                hitEffectParent.Stop();

                Die();
            }
        }
    }

    public void Die()
    {
        Debug.Log(name + " was destroyed");

        gameObject.SetActive(false);

        for (int i = 0; i < effects.Count; i++)
        {
            Destroy(effects[i].gameObject);
        }

        effects.Clear();

        Invoke("Respawn", respawnTime);
    }

    private void Respawn()
    {
        currentHealth = maxHealth;

        gameObject.SetActive(true);
    }
}
