using UnityEngine;

public interface IDamageable
{
    public void TakeDamage(float damage, Vector3 hitPos, Vector3 hitNormal);
    public float DamageMultiplier { get; }
}
