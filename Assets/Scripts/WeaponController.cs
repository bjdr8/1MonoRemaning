using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController
{
    private GameObject playerObject;
    private PlayerProfile2 playerProfile;
    private WeaponBase currentWeapon;
    public List<WeaponScript> weaponScripts = new List<WeaponScript>();
    public List<WeaponBase> weaponList = new List<WeaponBase>();

    public enum WeaponName
    {
        Pistol,
        Rifle,
        Minigun
    }

    public WeaponController(GameObject player,List<WeaponScript> weaponScripts)
    {
        this.weaponScripts = weaponScripts;
        playerObject = player;
        InitializeWeapons();
    }

    private void InitializeWeapons()
    {
        foreach (WeaponScript script in weaponScripts)
        {
            WeaponBase weapon = new WeaponBase(script);
            weaponList.Add(weapon);
        }
    }

    public WeaponBase SearchWeapon(WeaponName weaponName)
    {
        foreach (WeaponBase weapon in weaponList)
        {
            if (weapon.Name == weaponName)
            {
                return weapon;
            }
        }
        return null;
    }

    public void EquipWeapon(WeaponName weaponName)
    {
        if (currentWeapon != null)
        {
            UnequipCurrentWeapon();
        }
        if (SearchWeapon(weaponName) == null)
        {
            Debug.LogError("Weapon not found: " + weaponName);
            return;
        }
        WeaponBase weapon = SearchWeapon(weaponName);
        currentWeapon = weapon;
    }
    
    public void UnequipCurrentWeapon()
    {
        if (currentWeapon != null)
        {
            currentWeapon = null;
        }
    }

    public void Update()
    {
        if (currentWeapon == null) return;
        currentWeapon.Update();
        if (Input.GetMouseButton(0))
        {
            //Debug.Log("Shooting");
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 playerPosition = playerObject.transform.position;
            Vector2 direction = mousePosition - playerPosition;
            currentWeapon.Shoot(direction, playerPosition);
        }

        for (int i = currentWeapon.activeBulletPool.Count - 1; i >= 0; i--)
        {
            Bullet bullet = currentWeapon.activeBulletPool[i];

            if (!bullet.lifeTimeUpdate())
            {
                currentWeapon.activeBulletPool.RemoveAt(i);
                currentWeapon.bulletPool.Add(bullet);
                continue;
            }

            bullet.rb.velocity = bullet.direction.normalized * bullet.speed;
        }
    }
}
