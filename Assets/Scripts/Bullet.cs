using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class Bullet
{
    public GameObject bulletObject;
    public float bulletLifetime = 10f;
    private float bulletLifeTimeTimer = 0;
    public float speed = 30f;
    public Rigidbody2D rb;
    public Vector2 direction;

    public Bullet(GameObject bulletPrefab, Vector2 spawnPosition)
    {
        bulletObject = GameObject.Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
        rb = bulletObject.GetComponent<Rigidbody2D>();
    }

    public void ReuseBullet(Vector2 origin, Vector2 direction)
    {
        bulletLifeTimeTimer = bulletLifetime;
        bulletObject.SetActive(true);
        rb.velocity = Vector2.zero;
        this.direction = direction;
        bulletObject.transform.position = origin;

    }

    public virtual bool lifeTimeUpdate()
    {
        bulletLifeTimeTimer -= Time.deltaTime;
        if (bulletLifeTimeTimer <= 0)
        {
            if (bulletObject == null) return false;
            if (bulletObject.activeSelf)
            {
                bulletObject.SetActive(false);
            }
            return false;
        }
        return true;
    }
}
