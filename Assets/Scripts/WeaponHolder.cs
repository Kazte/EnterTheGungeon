using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour, ISerializationCallbackReceiver, IInteractable
{
    [SerializeField]
    private Weapon _weapon;

    private SpriteRenderer _renderer;
    
    private Animator _animator;

    private bool _picked;

    private void Awake()
    {
        _renderer = GetComponentsInChildren<SpriteRenderer>()[1];
        
        _animator = GetComponent<Animator>();
        Initialize();
    }

    public void Initialize(Weapon weapon = null)
    {
        _picked = false;
        if (weapon != null)
        {
            _weapon = weapon;
        }

        var weaponData = _weapon.GetComponent<Weapon>().GetData();
        _renderer.sprite = weaponData.WeaponIcon;
    }

    public Weapon GetWeapon() => _weapon;

    public Weapon PickWeapon()
    {
        if (_picked == true) return null;

        _animator.SetTrigger("Picked");
        
        _picked = true;
        return _weapon;
    }

    public void OnBeforeSerialize()
    {
        _renderer = GetComponentsInChildren<SpriteRenderer>()[1];

        if (_weapon != null)
        {
            var weaponData = _weapon.GetComponent<Weapon>().GetData();
            _renderer.sprite = weaponData.WeaponIcon;
            gameObject.name = $"Weapon Holder ({weaponData.name})";
        }
        else
        {
            gameObject.name = $"Weapon Holder";
        }
    }

    public void OnAfterDeserialize()
    {
    }

    public void Interact()
    {
        PickWeapon();
    }
}