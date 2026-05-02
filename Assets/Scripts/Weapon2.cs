using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static WeaponController;

public class WeaponBase
{
    public WeaponName Name;
    public float fireRate;
    private float fireRateTimer = 0;
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
        if (fireRateTimer > 0) return;
        Bullet bulletToShoot;
        if (bulletPool.Count == 0)
        {
            bulletToShoot = new Bullet(bulletPrefab, origin);
            activeBulletPool.Add(bulletToShoot);
        } else
        {
            bulletToShoot = bulletPool[0];
            bulletPool.RemoveAt(0);
        }
        bulletToShoot.ReuseBullet(origin, direction);
        activeBulletPool.Add(bulletToShoot);
        fireRateTimer = fireRate;
    }

    public virtual void ChangeFireRate(float fireRateChangeAmount, bool positive)
    {
        if (!positive)
        {
            fireRateChangeAmount = -fireRateChangeAmount;
        }
        fireRate += fireRateChangeAmount;
    }

    public void Update()
    {
        if (fireRateTimer <= 0) return;
        fireRateTimer -= Time.deltaTime;
    }
}
