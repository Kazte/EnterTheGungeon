
using UnityEngine;

public class HurtZone : HurtComponent
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            damageable.Damage(_damageDetails.Damage);
        }
    }
}
