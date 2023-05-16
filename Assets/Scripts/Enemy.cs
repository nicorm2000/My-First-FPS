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
            bodyParts[i].Init(TakeDamage, TakeDamage);
        }
    }

    public void TakeDamage(float damage, RaycastHit hit)
    {
        //Creates the VFX for the shooting effect towards an object
        ParticleSystem hitParticleEffect = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal), hit.collider.transform);

        hitParticleEffect.transform.parent = null;

        hitParticleEffect.transform.localScale = Vector3.one;

        hitParticleEffect.transform.parent = hit.collider.transform;
        
        effects.Add(hitParticleEffect);

        //Makes the effect a child so if the object is destroyed the effect is destroyed as well

        if (!(gameObject.tag == "Ground"))
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                hitEffectParent.Stop();

                Die();
            }
        }
    }

    public void TakeDamage(float damage)
    {
        if (!(gameObject.tag == "Ground"))
        {
            currentHealth -= damage;

            Debug.Log(damage);

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
