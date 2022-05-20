
using UnityEngine;

public class HurtComponent : MonoBehaviour
{
    [SerializeField]
    protected DamageDetails _damageDetails;

    public DamageDetails DamageDetails { get => _damageDetails; set => _damageDetails = value; }
}
