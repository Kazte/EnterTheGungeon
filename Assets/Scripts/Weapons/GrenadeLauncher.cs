using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncher : Weapon
{
    protected override void DownShoot(float rotation)
    {
        var bullet = Instantiate(_bullet);
        bullet.transform.localPosition = _muzzlePoint.position;
        bullet.transform.rotation = Quaternion.Euler(0f, 0f, rotation);
            
            
        // _muzzleObject.SetActive(true);
        _muzzleObjectAnimator.SetTrigger("Shoot");
        // Invoke(nameof(HideMuzzle), _muzzleObjectAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length);
        CameraEffects.Instance.ShakeCamera(_cameraShakeIntensity, _cameraShakeTime);

        _currentAmmo--;
        _onPlayerUpdateUI.Raise();
        
        
    }

    protected override IEnumerator Reload(bool forceReload = false)
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
        _rateFire = 0f;

        _reloading = false;
        _currentAmmo = _weaponData.MAXAmmo;
        _reloadTimeBeforeChange = 0f;
        _onPlayerUpdateUI.Raise();
    }
}
