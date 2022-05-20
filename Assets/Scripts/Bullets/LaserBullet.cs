using System;
using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class LaserBullet : Bullet
{
    private LineRenderer _lineRenderer;

    [SerializeField]
    private float _laserDistance;

    [SerializeField]
    private LayerMask _layerAvoid;

    [SerializeField]
    private GameObject _hitPointFx;
    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _hitPointFx.SetActive(false);
    }

    private void Update()
    {
        _lineRenderer.SetPosition(0, transform.position);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, _laserDistance, _layerAvoid);


        if (hit.collider)
        {
            var hitPos = new Vector3(hit.point.x, hit.point.y, transform.position.z);
            _lineRenderer.SetPosition(1, hitPos);
            _hitPointFx.SetActive(true);
            _hitPointFx.transform.position = hitPos;

            // _hitPointFx.transform.rotation = Quaternion.Euler(hit.normal);
            Debug.DrawLine(hit.point, hit.point + hit.normal);

            if (hit.collider.TryGetComponent(out IDamageable damageable))
            {
                damageable.Damage(_damageDetails.Damage * Time.deltaTime);
            }
        }
        else
        {
            _lineRenderer.SetPosition(1, transform.position + transform.right * _laserDistance);
            _hitPointFx.SetActive(false);
        }

        // var damage = _damageDetails.Damage * Time.deltaTime;
    }
}