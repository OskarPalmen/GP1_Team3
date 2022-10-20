using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    //this transforms the projektives position
    void Update()
    {
        Vector3 position = this.transform.position;
        position.y = position.y - 0.002f;
        this.transform.position = position;
    }
}
