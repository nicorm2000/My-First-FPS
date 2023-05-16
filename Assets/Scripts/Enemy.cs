using System;
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

    private Action<int> OnScoreAdded = null;

    public void Init(Action<int> updateScore)
    {
        OnScoreAdded = updateScore;

        for (int i = 0; i < bodyParts.Length; i++)
        {
            bodyParts[i].Init(TakeDamage, TakeDamage);
        }
    }

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage, RaycastHit hit, int score)
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
            OnScoreAdded?.Invoke(score);

            if (currentHealth <= 0)
            {
                hitEffectParent.Stop();

                Die();
            }
        }
    }

    public void TakeDamage(float damage, int score)
    {
        if (!(gameObject.tag == "Ground"))
        {
            currentHealth -= damage;
            OnScoreAdded?.Invoke(score);

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    public void Die()
    {
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
