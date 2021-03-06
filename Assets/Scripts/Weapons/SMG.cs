using UnityEngine;

public class SMG : Weapon
{
    
    private float _initSoundPitch;

    protected override void Start()
    {
        base.Start();

        _initSoundPitch = _audioSource.pitch;
    }
    
    protected override void DownShoot(float rotation)
    {
        HoldShoot(rotation);
    }

    protected override void HoldShoot(float rotation)
    {


        var bullet = InstantiateBullet(rotation);
        bullet.transform.localPosition = _muzzlePoint.position;
        bullet.transform.rotation = Quaternion.Euler(0f, 0f, rotation);


        // _muzzleObject.SetActive(true);
        _muzzleObjectAnimator.SetTrigger("Shoot");
        // Invoke(nameof(HideMuzzle), 0.05f);
        CameraEffects.Instance.ShakeCamera(_cameraShakeIntensity, _cameraShakeTime);
        
        _audioSource.pitch = Random.Range(_initSoundPitch - 0.05f, _initSoundPitch + 0.05f);
        _audioSource.PlayOneShot(_shotSound);
        
        _currentAmmo--;
        _onPlayerUpdateUI.Raise();
        
        _rateFire = _weaponData.FireRate;
    }
}