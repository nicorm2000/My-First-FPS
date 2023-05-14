using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] float explosionRadius;
    [SerializeField] float hitEffectDuration;
    private Transform effectsHolder;
    private float damage;

    public void Init(Transform effectsHolder, float damage)
    {
        this.effectsHolder = effectsHolder;
        this.damage = damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject effect = Instantiate(hitEffect.gameObject, effectsHolder);

        effect.transform.position = transform.position;
        
        Collider[] impactedColliders = Physics.OverlapSphere(effect.transform.position, explosionRadius);

        for (int i = 0; i < impactedColliders.Length; i++)
        {
            BodyPart part = impactedColliders[i].gameObject.transform.GetComponent<BodyPart>();

            if (part != null)
            {
                part.TakeDamage(damage);
            }
        }

        Destroy(effect, hitEffectDuration);

        Destroy(gameObject);
    }
}
