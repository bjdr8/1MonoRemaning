using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class WeaponScript : ScriptableObject
{
    public string weaponName;
    public float fireRate;
    public float damage;
    public GameObject bulletPrefab;
}
