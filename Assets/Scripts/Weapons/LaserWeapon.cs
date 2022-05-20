
using UnityEngine;

public class LaserWeapon : Weapon
{
    private Bullet _currentBullet;
    private float _damageTime;

    protected override void DownShoot(float rotation)
    {
        _damageTime = 0;
        _currentBullet = InstantiateBullet(rotation);
        _muzzleObject.SetActive(true);
    }

    protected override void HoldShoot(float rotation)
    {
        _currentBullet.transform.rotation = Quaternion.Euler(0f, 0f, rotation);
        _currentBullet.transform.position = _muzzlePoint.position;
        
        CameraEffects.Instance.ShakeCamera(_cameraShakeIntensity, _cameraShakeTime);
    }

    protected override void ReleaseShoot(float rotation)
    {
        DestroyBullet(_currentBullet);
        DestroyAllBullets();

        _muzzleObject.SetActive(false);        
    }
}
