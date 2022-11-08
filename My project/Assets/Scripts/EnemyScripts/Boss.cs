using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    public float speed;
    public float stoppingDistance;
    public float retreatDistance;

    private float timeBtwShots;
    public float startTimeBtwShots;

    public GameObject projectile;
    private Transform player;
    public float shootingRange = 5f;


    // Start is called before the first frame update
    // Finds the player tag
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;


        timeBtwShots = startTimeBtwShots;

    }

    // Update is called once per frame
    // This function sets the shooting speed and that the enemy should follow the player at a set distance
    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        }
        else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
        {

            transform.position = this.transform.position;
        }
        else if (Vector2.Distance(transform.position, player.position) < retreatDistance)

        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        }

        if (Vector2.Distance(transform.position, player.transform.position) < shootingRange)
        {
            if (timeBtwShots <= 0)
            {

                Instantiate(projectile, transform.position, Quaternion.identity);
                timeBtwShots = startTimeBtwShots;
                
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }

    }
}

