using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowMouse : MonoBehaviour
{
    private Camera _camera;

    
    private void Awake()
    {
        Cursor.visible = false;
        _camera = Camera.main;
    }

    private void Update()
    {
        // var aimPoint = (Vector2) _camera.ScreenToViewportPoint(Input.mousePosition);
        var aimPoint = (Vector2)Input.mousePosition;
        transform.position = aimPoint;
    }
}
