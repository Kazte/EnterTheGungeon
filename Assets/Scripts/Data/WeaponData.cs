using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Objects/Weapon", order = 0)]
public class WeaponData : ScriptableObject
{
    [SerializeField]
    private string _weaponName;

    [SerializeField]
    private Sprite _weaponIcon;
    
    [SerializeField]
    private int _magazineSize;
    
    [SerializeField]
    private int _maxAmmo;

    [SerializeField]
    private float _damage;

    [SerializeField]
    private float _damageRandomRate;

    [SerializeField]
    private float _fireRate;

    [SerializeField, Tooltip("In Angles")]
    private float _spread;

    [SerializeField]
    private float _force;

    [SerializeField]
    private float _reloadTime;

    [SerializeField]
    private int _sellPrice;


    public string WeaponName { get => _weaponName; set => _weaponName = value; }
    public Sprite WeaponIcon { get => _weaponIcon; set => _weaponIcon = value; }
    public int MagazineSize { get => _magazineSize; set => _magazineSize = value; }
    public int MAXAmmo { get => _magazineSize; set => _magazineSize = value; }
    public float FireRate { get => _fireRate; set => _fireRate = value; }
    public float Spread { get => _spread; set => _spread = value; }
    public float Force { get => _force; set => _force = value; }
    public float ReloadTime { get => _reloadTime; set => _reloadTime = value; }
    public int SellPrice { get => _sellPrice; set => _sellPrice = value; }

    public float GetWeaponDamage()
    {
        return Random.Range(_damage - _damageRandomRate, _damage + _damageRandomRate);
    }
}