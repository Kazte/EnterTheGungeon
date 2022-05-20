using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class GameEventListener
{
    public GameEvent Event;
    [Space]
    public UnityEvent Response;

    public void OnEnable()
    {
        // Debug.Log(this + " On Enable");
        Event.RegisterListener(this);
    }

    public void OnDisable()
    {
        // Debug.Log(this + " On Disable");
        Event.UnregisterListener(this);
    }

    public void OnEventRaised()
    {
        // Debug.Log(this + " Raised");
        Response.Invoke();
    }
}

/*
public class GameEventListener : MonoBehaviour
{
    public GameEvent Event;
    public UnityEvent Response;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised()
    {
        Response.Invoke();
    }
}
*/
