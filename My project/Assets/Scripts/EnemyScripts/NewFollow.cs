using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewFollow : MonoBehaviour
{
    private Transform target;
    public float speed = 3;
    public float followRange = 5f;

    // Start is called before the first frame update
    void Start()
    {// find player tag
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //movements and speed
        float distance = Vector3.Distance(transform.position, target.position);

        if (Vector2.Distance(transform.position, target.transform.position) < followRange)
        {
            if (target.position.x > transform.position.x)
            {
                //move right
                transform.Translate(transform.right * speed * Time.deltaTime);
            }
            else
            {
                // move left
                transform.Translate(-transform.right * speed * Time.deltaTime);
            }
        }

    }
}


    
    
       
        
