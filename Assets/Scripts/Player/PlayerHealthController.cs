
using System;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour, IDamageable
{
    [Header("Events")]
    [SerializeField]
    private GameEvent _onPlayerTakeDamage;

    [Space]
    [SerializeField]
    private FloatVariable _playerHP;

    private void Start()
    {
        _onPlayerTakeDamage.Raise();
    }

    public void Damage(float damage)
    {
        _playerHP.RuntimeValue-= damage;
        _onPlayerTakeDamage.Raise();
    }
}
