using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WeaponController;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class WeaponScript : ScriptableObject
{
    public WeaponName weaponName;
    public float fireRate;
    public float damage;
    public GameObject bulletPrefab;
}
