using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

[Serializable]
public class MinMaxFloat
{
    public float max;
    public float min;
}

public class WeaponController : MonoBehaviour
{
    // Aim
    private Camera _camera;

    [SerializeField]
    private MinMaxFloat _minmaxtest;

    [SerializeField]
    private PlayerStatsData _currentPlayerStatsData;

    [SerializeField]
    private Transform _weaponPivot;

    [SerializeField]
    private Transform _body;

    [SerializeField]
    private Transform _cameraPoint;

    [SerializeField]
    private GameObject _initWeapon;

    [SerializeField]
    private GameObject _weaponSocket;

    [Header("Events")]
    [SerializeField]
    private GameEvent _onPlayerUpdateUI;

    private float _holdTimeCurrent;

    private int _currentWeaponIndex = 0;

    private bool _scrollingMouse;

    // private bool _hold;

    private Vector2 _initPos;


    private void Awake()
    {
        _camera = Camera.main;

        _initPos = _cameraPoint.position;

        AddWeapon(_initWeapon.GetComponent<Weapon>());
    }

    private void Update()
    {
        var aimPoint = (Vector2) _camera.ScreenToWorldPoint(Input.mousePosition);


        // _cameraPoint.position = Vector2.ClampMagnitude((aimPoint) - (_initPos + (Vector2)transform.position), 1f);

        var diff = aimPoint - (Vector2) transform.position;
        var rotationZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        if (rotationZ > 90f || rotationZ < -90f)
        {
            _weaponPivot.transform.localScale = new Vector3(1, -1, 1);
            _weaponPivot.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);

            _body.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            _weaponPivot.transform.localScale = new Vector3(1, 1, 1);
            _weaponPivot.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);

            _body.transform.localScale = new Vector3(1, 1, 1);
        }

        // TODO: Extract to InputController singleton
        if (Input.GetMouseButtonDown(0))
        {
            _currentPlayerStatsData.CurrentWeapon.TryShoot(true, false, false, rotationZ);
        }
        else if (Input.GetMouseButton(0))
        {
            _currentPlayerStatsData.CurrentWeapon.TryShoot(false, true, false, rotationZ);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _currentPlayerStatsData.CurrentWeapon.TryShoot(false, false, true, rotationZ);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            // _currentPlayerStatsData.CurrentWeapon.StopCoroutine("Reload");
            _currentPlayerStatsData.CurrentWeapon.StartCoroutine("Reload", false);
        }

        // -----
        if (Input.mouseScrollDelta.y > 0f && !_scrollingMouse)
        {
            _scrollingMouse = true;
            _currentWeaponIndex++;

            if (_currentWeaponIndex > _currentPlayerStatsData.WeaponsList.Count - 1)
            {
                _currentWeaponIndex = 0;
            }

            SwitchWeapon(_currentWeaponIndex);
        }
        else if (Input.mouseScrollDelta.y < 0f && !_scrollingMouse)
        {
            _scrollingMouse = true;
            _currentWeaponIndex--;

            if (_currentWeaponIndex < 0)
            {
                _currentWeaponIndex = _currentPlayerStatsData.WeaponsList.Count - 1;
            }

            SwitchWeapon(_currentWeaponIndex);
        }
        if (Input.mouseScrollDelta.y == 0 && _scrollingMouse)
        {
            _scrollingMouse = false;
        }
    }

    public void SetWeapon(Weapon newWeapon)
    {
        _currentPlayerStatsData.CurrentWeapon = newWeapon;
    }

    public Weapon AddWeapon(Weapon newWeapon)
    {
        var weaponGO = Instantiate(newWeapon, transform.position, quaternion.identity);
        weaponGO.transform.SetParent(_weaponSocket.transform);
        weaponGO.transform.localPosition = Vector3.zero;
        weaponGO.transform.localRotation = Quaternion.identity;
        weaponGO.transform.localScale = Vector3.one;

        var w = weaponGO.GetComponent<Weapon>();
        w.SetOnPlayerUpdateUIEvent(_onPlayerUpdateUI);
        if (_currentPlayerStatsData.WeaponsList.Count < 1)
        {
            _currentPlayerStatsData.WeaponsList.Add(w);
            _currentWeaponIndex = 0;
            SwitchWeapon(_currentWeaponIndex);
        }
        else
        {
            _currentPlayerStatsData.WeaponsList.Add(w);
        }

        return w;
    }

    public void SwitchWeapon(int position)
    {
        if (_currentPlayerStatsData.CurrentWeapon != null)
        {
            _currentPlayerStatsData.CurrentWeapon.gameObject.SetActive(false);
        }
        
        SetWeapon(_currentPlayerStatsData.WeaponsList[position]);
        _currentPlayerStatsData.CurrentWeapon.gameObject.SetActive(true);
        _currentWeaponIndex = position;
        _onPlayerUpdateUI.Raise();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out WeaponHolder weaponHolder))
        {
            var weapon = weaponHolder.PickWeapon();

            if (weapon == null) return;

            var a = AddWeapon(weapon);
            a.gameObject.SetActive(false);
            _currentWeaponIndex = _currentPlayerStatsData.WeaponsList.IndexOf(a);
            SwitchWeapon(_currentWeaponIndex);
        }
    }
}