using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform teleportTarget;
    public GameObject Player;

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {

            var cc = Player.GetComponent<CharacterController>();

            Debug.Log("Before"+ Player.transform.position);
            cc.enabled = false;
            Player.transform.position = teleportTarget.transform.position;
            cc.enabled = true;
            Debug.Log("After" + Player.transform.position);
        }
    }


}
