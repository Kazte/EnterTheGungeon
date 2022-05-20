using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    
    [SerializeField]
    private Animator _anim;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private PlayerStatsData _currentStats;

    [Header("Events")]
    [SerializeField]
    private GameEvent _onInteractEnter;
    
    [SerializeField]
    private GameEvent _onInteractExit;

    private IInteractable _interactable;
    
    private Vector2 _movement;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _currentStats.PlayerGameObject = gameObject;
    }

    private void Update()
    {
        GetInput();
        AnimationController();
    }

    private void GetInput()
    {
        _movement.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        _movement.Normalize();

        if (Input.GetKeyDown(KeyCode.E))
        {
            _interactable.Interact();
        }
    }

    private void AnimationController()
    {
        _anim.SetBool("IsMoving", Mathf.Abs(_movement.magnitude) > 0.1f);
    }

    private void FixedUpdate()
    {
        _rb.velocity = _movement * _speed;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Interact"))
        {
            _onInteractEnter.Raise();
            _interactable = other.GetComponent<IInteractable>();
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Interact"))
        {
            _onInteractExit.Raise();
            _interactable = null;
        }
    }
}