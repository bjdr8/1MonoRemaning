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


    public WeaponController(List<WeaponScript> weaponScripts)
    {
        this.weaponScripts = weaponScripts;
    }
    
    public void EquipWeapon(WeaponBase weapon)
    {
        if (currentWeapon != null)
        {
            UnequipCurrentWeapon();
        }
        currentWeapon = weapon;
        currentWeapon.Equip();
    }
    
    public void UnequipCurrentWeapon()
    {
        if (currentWeapon != null)
        {
            currentWeapon.Unequip();
            currentWeapon = null;
        }
    }

    public void Update()
    {
        if (currentWeapon == null) return;

        if (Input.GetMouseButton(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 playerPosition = playerObject.transform.position;
            Vector2 direction = mousePosition - playerPosition;
            currentWeapon.Shoot(direction, playerPosition);
        }

        foreach (Bullet bullet in currentWeapon.activeBulletPool)
        {
            bullet.lifeTimeUpdate();
            bullet.rb.velocity = bullet.direction.normalized * bullet.speed;
            
        }
    }
}
