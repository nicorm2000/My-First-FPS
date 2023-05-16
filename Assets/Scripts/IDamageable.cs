using UnityEngine;

public interface IDamageable
{
    public void TakeDamage(float damage, RaycastHit hit);
    public float DamageMultiplier { get; }
}
