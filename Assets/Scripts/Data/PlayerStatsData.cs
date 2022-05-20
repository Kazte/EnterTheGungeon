
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerStatsData : ScriptableObject, ISerializationCallbackReceiver
{
    public GameObject PlayerGameObject;
    public float HP_Max = 10;
    public float HP_Current = 10;
    public Weapon CurrentWeapon = null;
    public List<Weapon> WeaponsList = new List<Weapon>();
    
    public void OnBeforeSerialize()
    {
        
    }

    public void OnAfterDeserialize()
    {
        HP_Max = 10;
        HP_Current = 10;

        CurrentWeapon = null;
        WeaponsList = new List<Weapon>();
    }
}
