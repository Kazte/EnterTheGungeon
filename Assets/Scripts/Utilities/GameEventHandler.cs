using System;
using System.Collections.Generic;
using UnityEngine;

public class GameEventHandler : MonoBehaviour
{
    [SerializeField] 
    private List<GameEventListener> _events;

    private void OnEnable()
    {
        foreach (var eventListener in _events)
        {
            eventListener.OnEnable();
        }
    }
    
    private void OnDisable()
    {
        foreach (var eventListener in _events)
        {
            eventListener.OnDisable();
        }
    }
}
