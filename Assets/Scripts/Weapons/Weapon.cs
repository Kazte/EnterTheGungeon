using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class Weapon : MonoBehaviour, IWeapon
{
    [SerializeField]
    protected Transform _muzzlePoint;

    [SerializeField]
    protected GameObject _muzzleObject;

    protected Animator _muzzleObjectAnimator;

    [SerializeField]
    protected float _cameraShakeIntensity = 1f;

    [SerializeField]
    protected float _cameraShakeTime = 0.1f;

    [SerializeField]
    protected ParticleSystem _shellParticle;

    [SerializeField]
    protected Bullet _bullet;

    [SerializeField]
    protected List<Bullet> _bullets;


    [SerializeField]
    protected WeaponData _weaponData;

    [SerializeField]
    protected float _currentAmmo;

    [SerializeField]
    protected AudioClip _shotSound;

    protected AudioSource _audioSource;

    protected GameEvent _onPlayerUpdateUI;

    protected bool _reloading;

    protected float _currentReloadTime;

    protected float _rateFire;

    protected float _reloadTimeBeforeChange;

    private void OnEnable()
    {
        if (_reloading) StartCoroutine("Reload", true);
        _muzzleObjectAnimator = _muzzleObject.GetComponent<Animator>();
    }

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    protected virtual void Start()
    {
        _currentAmmo = _weaponData.MAXAmmo;
        _onPlayerUpdateUI.Raise();
        _muzzleObject.SetActive(false);
    }

    protected void Update()
    {
        if (_rateFire > 0) _rateFire -= Time.deltaTime;
    }

    public void TryShoot(bool down, bool hold, bool release, float rotation)
    {
        if ((_currentAmmo <= 0 && _weaponData.MagazineSize > 0) || _reloading) return;

        if (_rateFire > 0) return;

        rotation += Random.Range(-_weaponData.Spread, _weaponData.Spread);

        if (down)
        {
            DownShoot(rotation);
        }
        else if (hold)
        {
            HoldShoot(rotation);
        }
        else if (release)
        {
            ReleaseShoot(rotation);
        }

        
    }

    protected virtual void DownShoot(float rotation)
    {
    }

    protected virtual void HoldShoot(float rotation)
    {
    }

    protected virtual void ReleaseShoot(float rotation)
    {
    }

    protected virtual IEnumerator Reload(bool forceReload = false)
    {
        if (_reloading && !forceReload) yield break;

        if (_reloadTimeBeforeChange > 0f)
        {
            _currentReloadTime = _reloadTimeBeforeChange;
            _reloadTimeBeforeChange = 0f;
        }
        else
        {
            // Default reload behaviour
            _reloading = true;
            _currentReloadTime = 0f;
            _reloadTimeBeforeChange = 0f;
        }

        while (_currentReloadTime < _weaponData.ReloadTime)
        {
            yield return null;

            // May change to different events
            _onPlayerUpdateUI.Raise();

            _currentReloadTime += Time.deltaTime;
            _reloadTimeBeforeChange = _currentReloadTime;
        }

        _reloading = false;
        _currentAmmo = _weaponData.MAXAmmo;
        _reloadTimeBeforeChange = 0f;
        _onPlayerUpdateUI.Raise();
    }

    protected void HideMuzzle()
    {
        _muzzleObject.SetActive(false);
    }

    protected Bullet InstantiateBullet(float rotation)
    {
        var bullet = Instantiate(_bullet, _muzzlePoint.position, Quaternion.Euler(0f, 0f, rotation));

        bullet.Initialize(_weaponData.GetWeaponDamage()); //Todo: change
        
        _bullets.Add(bullet);
        return bullet;
    }

    protected Bullet DestroyBullet(Bullet bullet)
    {
        var auxBullet = bullet;
        _bullets.Remove(bullet);
        
        Destroy(bullet.gameObject);
        
        return auxBullet;
    }

    protected void DestroyAllBullets()
    {
        foreach (var bullet in _bullets)
        {
            Destroy(bullet.gameObject);
        }

        _bullets.Clear();
    }

    public WeaponData GetData() => _weaponData;

    public float GetCurrentAmmo() => _currentAmmo;

    public float GetCurrentReloadTime() => _currentReloadTime;

    public void SetOnPlayerUpdateUIEvent(GameEvent onPlayerUpdateUI)
    {
        _onPlayerUpdateUI = onPlayerUpdateUI;
    }
    
    
}