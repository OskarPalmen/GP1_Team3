using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float life = 1;

    private void Awake()
    {
        Destroy(gameObject, life);
    }

    void OnTriggerEnter(Collider other)
    {
        //if the projectile collides with a player tag destroy the projectile
        if (other.CompareTag("Player"))
        {
            DestroyProjectile();
        }
        else if (other.CompareTag("Wall"))
        {
            DestroyProjectile();
        }
    }

    //funktion that destroys the projectile
    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
    /*private void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);
        Destroy(gameObject);
    }*/
}
