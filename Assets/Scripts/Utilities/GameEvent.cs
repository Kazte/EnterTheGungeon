
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEvent : ScriptableObject
{
    [SerializeField] private List<GameEventListener> _listeners = new List<GameEventListener>();

    

    public void Raise()
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
        {
            _listeners[i].OnEventRaised();
            // Debug.Log($"Lister raised: {_listeners[i].name} to {name}");
        }
    }

    public void RegisterListener(GameEventListener listener)
    {
        _listeners.Add(listener);
        // Debug.Log($"Lister suscribe: {listener.name} to {name}");
    }
    
    public void UnregisterListener(GameEventListener listener)
    {
        _listeners.Remove(listener);
        // Debug.Log($"Lister unsuscribe: {listener.name} to {name}");
    }
}
