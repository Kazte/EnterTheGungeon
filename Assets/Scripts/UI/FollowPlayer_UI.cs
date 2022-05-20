using System;
using UnityEngine;

public class FollowPlayer_UI : MonoBehaviour
{
    private GameObject _player;

    private Camera _currentCamera;

    [SerializeField]
    private Vector2 _offset;


    private void Awake()
    {
        _currentCamera = Camera.main;
        _player = GameObject.FindWithTag("Player");
    }

    private void FixedUpdate()
    {
        var playerPosition = _player.transform.position + (Vector3) _offset;
        transform.position = _currentCamera.WorldToScreenPoint(playerPosition);
    }
}