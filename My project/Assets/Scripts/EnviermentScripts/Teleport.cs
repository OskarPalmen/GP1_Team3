using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform teleportTarget;
    public GameObject Player;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == Player.tag)
        {
            Player.transform.position = teleportTarget.transform.position;
        }
        
    }


}
