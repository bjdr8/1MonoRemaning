using UnityEngine;

public class Bullet
{
    public GameObject bulletObject;
    public float bulletLifetime = 10f;
    public float speed = 30f;
    public Rigidbody2D rb;
    public Vector2 direction;

    public Bullet()
    {
        rb = bulletObject.GetComponent<Rigidbody2D>();
    }

    public virtual bool lifeTimeUpdate()
    {
        bulletLifetime -= Time.deltaTime;
        if (bulletLifetime <= 0)
        {
            return false;
        }
        return true;
    }
}
