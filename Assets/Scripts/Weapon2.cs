using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponBase
{
    public string Name;
    public float fireRate;
    public float damage;
    public GameObject bulletPrefab;
    public List<Bullet> bulletPool = new List<Bullet>();
    public List<Bullet> activeBulletPool = new List<Bullet>();

    public WeaponBase(WeaponScript weaponScript)
    {
        Name = weaponScript.weaponName;
        fireRate = weaponScript.fireRate;
        damage = weaponScript.damage;
        bulletPrefab = weaponScript.bulletPrefab;
    }

    public virtual void Shoot(Vector2 direction, Vector2 origin)
    {
        if (bulletPool.Count == 0)
        {
            Bullet newBullet = new Bullet();
            newBullet.direction = direction;
            newBullet.bulletObject = Object.Instantiate(bulletPrefab, origin, Quaternion.identity.normalized);
            activeBulletPool.Add(newBullet);
        } else
        {
            Bullet bulletToShoot = bulletPool[0];
            bulletPool.RemoveAt(0);
            activeBulletPool.Add(bulletToShoot);
            bulletToShoot.bulletObject.transform.position = origin;
            bulletToShoot.direction = direction;
            bulletToShoot.bulletObject.SetActive(true);
        }
    }

    public virtual void ChangeFireRate(float fireRateChangeAmount, bool positive)
    {
        if (!positive)
        {
            fireRateChangeAmount = -fireRateChangeAmount;
        }
        fireRate += fireRateChangeAmount;
    }
    public virtual void Equip()
    {
    }

    public virtual void Unequip()
    {
    }
}
