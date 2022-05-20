using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : Bullet
{
    [SerializeField]
    private Rigidbody2D _rb;

    [SerializeField]
    private float _bulletSpeed;


    private void Awake()
    {
        Destroy(gameObject, 5f);
    }

    private void FixedUpdate()
    {
        _rb.velocity = transform.right * _bulletSpeed;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) return;
        if (other.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            damageable.Damage(_damageDetails.Damage);
        }
        
        Destroy(gameObject);
    }
}
