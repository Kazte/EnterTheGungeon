using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ExplosiveBullet : Bullet
{
    [SerializeField]
    private Rigidbody2D _rb;

    [SerializeField]
    private float _bulletSpeed;

    [SerializeField]
    private GameObject _explodeFX;

    [SerializeField]
    private int _initialBounce;

    private int _bounceLeft;

    [SerializeField]
    private float _decayForceTime;

    [SerializeField]
    private AnimationCurve _curve;

    [SerializeField]
    private float _timer;


    private void Start()
    {
        _rb.AddForce(transform.right * _bulletSpeed, ForceMode2D.Impulse);
        _bounceLeft = _initialBounce;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        // _rb.velocity = Vector2.Lerp(_rb.velocity, Vector2.zero, Time.deltaTime * _decayForceTime);
        // var newX = 0f;
        // var newY = 0f;
        //
        // if (Mathf.Abs(_rb.velocity.x) > 0f)
        // {
        //     newX = _rb.velocity.x - Mathf.Sign(_rb.velocity.x) * Time.fixedDeltaTime * _decayForceTime;
        // }
        //
        // if (Mathf.Abs(_rb.velocity.y) > 0f)
        // {
        //     newY = _rb.velocity.y - Mathf.Sign(_rb.velocity.y) * Time.fixedDeltaTime * _decayForceTime;
        // }
        //
        // _rb.velocity = new Vector2(newX, newY);

        Debug.Log($"Direction: {transform.right}\nVelocity.Magnitude: {_rb.velocity.magnitude}");

        Debug.DrawLine(transform.position, transform.position + transform.right * _rb.velocity.magnitude);

        // _rb.velocity = _rb.velocity * 0.99f;

        if (_rb.velocity.magnitude <= 0.1f)
        {
            Explode();
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) return;

        if (other.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            Explode();
            Debug.Log("Damageable");
        }
        else
        {
            if (_bounceLeft > 0)
            {
                _bounceLeft--;
            }
            else
            {
                Explode();
            }
            Debug.Log("Not Damageable");
        }
    }


    private void Explode()
    {
        Debug.Log("Explode");
        Instantiate(_explodeFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}