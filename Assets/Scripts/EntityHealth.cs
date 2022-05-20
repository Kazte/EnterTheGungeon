
using System;
using UnityEngine;

public class EntityHealth : MonoBehaviour, IDamageable
{
    [SerializeField]
    private float _maxHP;
    
    private float _currentHP;
    
    private void Awake()
    {
        _currentHP = _maxHP;
    }

    public void Damage(float damage)
    {

        _currentHP -= damage;
        
        if (_currentHP <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        gameObject.SetActive(false);
    }
}
