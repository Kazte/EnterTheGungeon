using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField]
    private Image _playerHealthbar;
    
    [SerializeField]
    private Image _playerHealthbarBGEffect;

    [SerializeField]
    private Text _playerHealthbarNumber;

    [SerializeField]
    private Image _playerCurrentWeapon;

    [SerializeField]
    private Text _currentWeaponAmmo;

    


    [Header("Variables")]
    [SerializeField]
    private PlayerStatsData _playerStatsData;

    [SerializeField]
    private FloatVariable _playerHP;

    [SerializeField]
    private FloatVariable _playerHP_MAX;

    private float _targetHPValue;
    private bool _healthbarEffectActivated;

    public void UpdateUI()
    {
        // Update Healthbar
        _targetHPValue = _playerHP.RuntimeValue / _playerHP_MAX.RuntimeValue;
        _playerHealthbar.fillAmount = _targetHPValue;
        StartCoroutine(HealthBarEffect());

        // Current Weapon Update
        if (_playerStatsData.CurrentWeapon != null)
        {
            _playerCurrentWeapon.sprite = _playerStatsData.CurrentWeapon.GetData().WeaponIcon;

            _currentWeaponAmmo.text =
                (_playerStatsData.CurrentWeapon.GetData().MagazineSize == 0
                    ? "\u221E"
                    : _playerStatsData.CurrentWeapon.GetCurrentAmmo().ToString()) + " / " +
                _playerStatsData.CurrentWeapon.GetData().MAXAmmo;
        }
    }

    private IEnumerator HealthBarEffect()
    {
        if (!_healthbarEffectActivated) yield return new WaitForSeconds(0.5f);
        
        _healthbarEffectActivated = true;
        
        while (_playerHealthbarBGEffect.fillAmount > _targetHPValue)
        {
            _playerHealthbarBGEffect.fillAmount -= Time.deltaTime;

            
            
            yield return null;
        }

        _playerHealthbarBGEffect.fillAmount = _targetHPValue;
        
        _healthbarEffectActivated = false;

    }
}