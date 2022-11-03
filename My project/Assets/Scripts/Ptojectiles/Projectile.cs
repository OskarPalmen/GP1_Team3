using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed;

    private Transform player;
    private Vector2 target;


    // Start is called before the first frame update
    void Start()
    {
        //the projectile goes after the tag "Player"
        player = GameObject.FindGameObjectWithTag("Player").transform;

        FindObjectOfType<AudioManager>().Play("Pew");

        //the projectile goes after the player's position
        target = new Vector2(player.position.x, player.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        //the projektile moves twords the set potision
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        //when it has reached its end destination destroy the projectile
        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            DestroyProjectile();
        }

    }

    void OnTriggerEnter(Collider other)
    {
        //if the projectile collides with a player tag destroy the projectile
        if (other.CompareTag("Player"))
        {
            DestroyProjectile();
        } else if (other.CompareTag("Wall"))
        {
            DestroyProjectile();
        }
    }

    //funktion that destroys the projectile
    void DestroyProjectile()
    {
        Destroy(gameObject);
    }

}
