using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{

    Animator _doorAnim;

    private void OnTriggerEnter(Collider other)

    {

        if (other.tag == "Player")
        {

            _doorAnim.SetBool("isOpening", true);
        }
            

    }

    private void OnTriggerExit(Collider other)




    {
        if (other.tag == "Player")

        {
            _doorAnim.SetBool("isOpening", false);
        }
       
    }
    // Start is called before the first frame update
    void Start()
    {

        _doorAnim = this.transform.parent.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
