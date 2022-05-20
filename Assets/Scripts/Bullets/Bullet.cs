using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    protected DamageDetails _damageDetails;

    public void Initialize(DamageDetails damageDetails)
    {
        // _damageDetails = damageDetails;
    }

    public void Initialize(float damage)
    {
        _damageDetails.Damage = damage;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            damageable.Damage(_damageDetails.Damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            damageable.Damage(_damageDetails.Damage);
        }
    }
}