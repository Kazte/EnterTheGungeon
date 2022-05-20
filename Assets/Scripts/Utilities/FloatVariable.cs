
using System;
using UnityEngine;

[CreateAssetMenu]
public class FloatVariable : ScriptableObject, ISerializationCallbackReceiver
{
    public float Value;

    
    public float RuntimeValue;

    public void OnBeforeSerialize()
    {
        
    }

    public void OnAfterDeserialize()
    {
        RuntimeValue = Value;
    }
}
